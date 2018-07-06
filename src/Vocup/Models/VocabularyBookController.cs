using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup.Models
{
    public class VocabularyBookController
    {
        private readonly List<VocabularyWordController> wordControllers;
        private readonly ColumnHeader imageColumn;
        private readonly ColumnHeader motherTongueColumn;
        private readonly ColumnHeader foreignLangColumn;
        private readonly ColumnHeader praticeDateColumn;

        public VocabularyBookController(VocabularyBook vocabularyBook)
        {
            ImageList imageList = new ImageList();
            imageList.Images.Add(Icons.noch_nicht_geübt);
            imageList.Images.Add(Icons.falsch_geübt);
            imageList.Images.Add(Icons.richtig_geübt);
            imageList.Images.Add(Icons.übung_abgeschlossen);

            ListView = new ListView()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                GridLines = Settings.Default.GridLines,
                MultiSelect = false,
                Size = new System.Drawing.Size(497, 487),
                View = View.Details,
                SmallImageList = imageList
            };
            imageColumn = new ColumnHeader() { Text = "" };
            motherTongueColumn = new ColumnHeader();
            foreignLangColumn = new ColumnHeader();
            praticeDateColumn = new ColumnHeader() { Text = Words.LastPracticed };
            wordControllers = new List<VocabularyWordController>(
                vocabularyBook.Words.Select(x => new VocabularyWordController(x)));
            WordControllers = new ReadOnlyCollection<VocabularyWordController>(wordControllers);
            ListView.Columns.AddRange(new[] { imageColumn, motherTongueColumn, foreignLangColumn, praticeDateColumn });
            foreach (VocabularyWordController controller in wordControllers)
                ListView.Items.Add(controller.ListViewItem);
            VocabularyBook = vocabularyBook;
            VocabularyBook.PropertyChanged += (a0, a1) => UpdateUI();
            VocabularyBook.CollectionChanged += VocabularyBook_CollectionChanged;
            UpdateUI();
        }

        public ListView ListView { get; }
        public VocabularyBook VocabularyBook { get; }
        IReadOnlyCollection<VocabularyWordController> WordControllers { get; }

        public VocabularyWordController GetController(VocabularyWord word)
        {
            foreach (VocabularyWordController controler in WordControllers)
            {
                if (ReferenceEquals(controler.VocabularyWord, word))
                    return controler;
            }

            throw new KeyNotFoundException("No controller could be found for the specified VocabularyWord.");
        }

        private void UpdateUI()
        {
            // TODO: Set width of items like the last state. Saving in Settings required.
            motherTongueColumn.Text = VocabularyBook.MotherTongue;
            foreignLangColumn.Text = VocabularyBook.ForeignLang;
        }

        private void VocabularyBook_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (VocabularyWord word in e.NewItems)
                    {
                        word.Owner = VocabularyBook;
                        VocabularyWordController controller = new VocabularyWordController(word);
                        wordControllers.Add(controller);
                        ListView.Items.Add(controller.ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (VocabularyWord word in e.OldItems)
                    {
                        word.Owner = null;
                        VocabularyWordController controller = GetController(word);
                        wordControllers.Remove(controller);
                        ListView.Items.Remove(controller.ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (VocabularyWord word in e.OldItems)
                    {
                        word.Owner = null;
                        VocabularyWordController controller = GetController(word);
                        wordControllers.Remove(controller);
                        ListView.Items.Remove(controller.ListViewItem);
                    }
                    foreach (VocabularyWord word in e.NewItems)
                    {
                        word.Owner = VocabularyBook;
                        VocabularyWordController controller = new VocabularyWordController(word);
                        wordControllers.Add(controller);
                        ListView.Items.Add(controller.ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    break; // sort will be done on UI layer

                case NotifyCollectionChangedAction.Reset:
                    foreach (VocabularyWord word in e.OldItems)
                        word.Owner = null;
                    wordControllers.Clear();
                    ListView.Items.Clear();
                    break;
            }
        }
    }
}

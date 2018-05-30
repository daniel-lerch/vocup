using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            ListView = new ListView();
            imageColumn = new ColumnHeader();
            motherTongueColumn = new ColumnHeader();
            foreignLangColumn = new ColumnHeader();
            praticeDateColumn = new ColumnHeader();
            wordControllers = new List<VocabularyWordController>();
            WordControllers = new ReadOnlyCollection<VocabularyWordController>(wordControllers);
            ListView.Columns.AddRange(new[] { imageColumn, motherTongueColumn, foreignLangColumn, praticeDateColumn });
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
                        ListView.Items.Add(new VocabularyWordController(word).ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (VocabularyWord word in e.OldItems)
                    {
                        word.Owner = null;
                        ListView.Items.Remove(GetController(word).ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (VocabularyWord word in e.OldItems)
                    {
                        word.Owner = null;
                        ListView.Items.Remove(GetController(word).ListViewItem);
                    }
                    foreach (VocabularyWord word in e.NewItems)
                    {
                        word.Owner = VocabularyBook;
                        ListView.Items.Add(new VocabularyWordController(word).ListViewItem);
                    }
                    break;

                case NotifyCollectionChangedAction.Move:
                    break; // sort will be done on UI layer

                case NotifyCollectionChangedAction.Reset:
                    foreach (VocabularyWord word in e.OldItems)
                        word.Owner = null;
                    ListView.Items.Clear();
                    break;
            }
        }
    }
}

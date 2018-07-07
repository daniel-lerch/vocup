using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Controls;
using Vocup.Properties;

namespace Vocup.Models
{
    public class VocabularyBookController
    {
        private readonly List<VocabularyWordController> wordControllers;
        private StatisticsPanel _statisticsPanel;

        public VocabularyBookController(VocabularyBook vocabularyBook)
        {
            ListView = new VocabularyListView()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                GridLines = Settings.Default.GridLines,
            };
            wordControllers = new List<VocabularyWordController>();
            WordControllers = new ReadOnlyCollection<VocabularyWordController>(wordControllers);
            AddItems(vocabularyBook.Words);
            VocabularyBook = vocabularyBook;
            VocabularyBook.PropertyChanged += (a0, a1) => UpdateUI();
            VocabularyBook.CollectionChanged += VocabularyBook_CollectionChanged;
            VocabularyBook.CollectionChanged += OnStatisticsChanged;
            UpdateUI();
        }

        public VocabularyListView ListView { get; }
        public StatisticsPanel StatisticsPanel
        {
            get => _statisticsPanel;
            set { _statisticsPanel = value; OnStatisticsChanged(); }
        }
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
            ListView.MotherTongue = VocabularyBook.MotherTongue;
            ListView.ForeignLang = VocabularyBook.ForeignLang;
        }

        private void VocabularyBook_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddItems(e.NewItems);
                    break;

                case NotifyCollectionChangedAction.Remove:
                    RemoveItems(e.OldItems);
                    break;

                case NotifyCollectionChangedAction.Replace:
                    AddItems(e.NewItems);
                    RemoveItems(e.OldItems);
                    break;

                case NotifyCollectionChangedAction.Move:
                    break; // sort will be done on UI layer

                case NotifyCollectionChangedAction.Reset:
                    RemoveItems(e.OldItems);
                    break;
            }
        }

        private void OnStatisticsChanged(object sender, EventArgs e) => OnStatisticsChanged();
        private void OnStatisticsChanged()
        {
            if (StatisticsPanel == null)
                return;

            int un = 0, wrongly = 0, correctly = 0, fully = 0;

            foreach (VocabularyWord word in VocabularyBook.Words)
            {
                switch (word.PracticeState)
                {
                    case PracticeState.Unpracticed:
                        un++;
                        break;
                    case PracticeState.WronglyPracticed:
                        wrongly++;
                        break;
                    case PracticeState.CorrectlyPracticed:
                        correctly++;
                        break;
                    case PracticeState.FullyPracticed:
                        fully++;
                        break;
                    default:
                        break;
                }
            }

            StatisticsPanel.Unpracticed = un;
            StatisticsPanel.WronglyPracticed = wrongly;
            StatisticsPanel.CorrectlyPracticed = correctly;
            StatisticsPanel.FullyPracticed = fully;
        }

        private void AddItems(IEnumerable items)
        {
            foreach (VocabularyWord word in items)
            {
                word.Owner = VocabularyBook;
                VocabularyWordController controller = new VocabularyWordController(word);
                wordControllers.Add(controller);
                ListView.Items.Add(controller.ListViewItem);
                word.PropertyChanged += OnStatisticsChanged;
            }
        }

        private void RemoveItems(IEnumerable items)
        {
            foreach (VocabularyWord word in items)
            {
                word.Owner = null;
                VocabularyWordController controller = GetController(word);
                wordControllers.Remove(controller);
                ListView.Items.Remove(controller.ListViewItem);
                word.PropertyChanged -= OnStatisticsChanged;
            }
        }
    }
}

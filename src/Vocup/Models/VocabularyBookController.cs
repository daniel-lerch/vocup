using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Vocup.Controls;
using Vocup.Properties;

namespace Vocup.Models
{
    public class VocabularyBookController
    {
        private readonly List<VocabularyWordController> wordControllers;
        private StatisticsPanel _statisticsPanel;

        public VocabularyBookController(VocabularyBook book)
        {
            ListView = new VocabularyListView()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                GridLines = Settings.Default.GridLines,
            };
            wordControllers = new List<VocabularyWordController>();
            WordControllers = new ReadOnlyCollection<VocabularyWordController>(wordControllers);
            book.Words.OnAdd(AddItem);
            book.Words.OnRemove(RemoveItem);
            book.PropertyChanged += (a0, a1) => UpdateUI();
            book.Statistics.PropertyChanged += OnStatisticsChanged;
            VocabularyBook = book;
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

        private void OnStatisticsChanged(object sender, EventArgs e) => OnStatisticsChanged();
        private void OnStatisticsChanged()
        {
            if (StatisticsPanel == null)
                return;

            StatisticsPanel.Unpracticed = VocabularyBook.Statistics.Unpracticed;
            StatisticsPanel.WronglyPracticed = VocabularyBook.Statistics.WronglyPracticed;
            StatisticsPanel.CorrectlyPracticed = VocabularyBook.Statistics.CorrectlyPracticed;
            StatisticsPanel.FullyPracticed = VocabularyBook.Statistics.FullyPracticed;
        }

        private void AddItem(VocabularyWord item)
        {
            item.Owner = VocabularyBook;
            VocabularyWordController controller = new VocabularyWordController(item);
            wordControllers.Add(controller);
            ListView.Items.Add(controller.ListViewItem);
        }

        private void RemoveItem(VocabularyWord item)
        {
            item.Owner = null;
            VocabularyWordController controller = GetController(item);
            wordControllers.Remove(controller);
            ListView.Items.Remove(controller.ListViewItem);
        }
    }
}

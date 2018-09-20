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
        private IMainForm _parent;

        public VocabularyBookController(VocabularyBook book)
        {
            ListView = new VocabularyListView()
            {
                Dock = DockStyle.Fill,
                GridLines = Settings.Default.GridLines,
            };
            ListView.ItemSelectionChanged += OnSelectionChanged;
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
        public IMainForm Parent
        {
            get => _parent;
            set { _parent = value; OnStatisticsChanged(); OnSelectionChanged(); }
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
            ListView.MotherTongue = VocabularyBook.MotherTongue;
            ListView.ForeignLang = VocabularyBook.ForeignLang;
        }

        private void OnStatisticsChanged(object sender, EventArgs e) => OnStatisticsChanged();
        private void OnStatisticsChanged()
        {
            if (Parent == null)
                return;

            Parent.StatisticsPanel.Unpracticed = VocabularyBook.Statistics.Unpracticed;
            Parent.StatisticsPanel.WronglyPracticed = VocabularyBook.Statistics.WronglyPracticed;
            Parent.StatisticsPanel.CorrectlyPracticed = VocabularyBook.Statistics.CorrectlyPracticed;
            Parent.StatisticsPanel.FullyPracticed = VocabularyBook.Statistics.FullyPracticed;

            Parent.VocabularyBookPracticable(VocabularyBook.Statistics.NotFullyPracticed > 0);
        }

        private void OnSelectionChanged(object sender, EventArgs e) => OnSelectionChanged();
        private void OnSelectionChanged()
        {
            if (Parent == null)
                return;

            Parent.VocabularyWordSelected(ListView.SelectedItems.Count > 0);
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

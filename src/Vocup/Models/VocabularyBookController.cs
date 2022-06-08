using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Vocup.Controls;
using Vocup.IO;
using Vocup.Properties;

#nullable disable

namespace Vocup.Models;

public class VocabularyBookController : IDisposable
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
        ListView.Control.DoubleClick += OnDoubleClick;
        wordControllers = new List<VocabularyWordController>();
        WordControllers = new ReadOnlyCollection<VocabularyWordController>(wordControllers);
        book.Words.OnAdd(AddItem);
        book.Words.OnRemove(RemoveItem);
        book.PropertyChanged += OnPropertyChanged;
        book.Statistics.PropertyChanged += OnStatisticsChanged;
        VocabularyBook = book;
        OnPropertyChanged(this, new PropertyChangedEventArgs(null));
    }

    public VocabularyListView ListView { get; }
    public IMainForm Parent
    {
        get => _parent;
        set
        {
            if (_parent != null) _parent.SearchText.TextChanged -= OnSearchTextChanged;
            _parent = value;
            _parent.SearchText.TextChanged += OnSearchTextChanged;
            OnPropertyChanged(this, new PropertyChangedEventArgs(null));
            OnStatisticsChanged(this, new EventArgs());
            OnSelectionChanged(this, new EventArgs());
        }
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

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        ListView.MotherTongue = VocabularyBook.MotherTongue;
        ListView.ForeignLang = VocabularyBook.ForeignLang;
        Parent?.VocabularyBookHasFilePath(!string.IsNullOrWhiteSpace(VocabularyBook.FilePath));
        Parent?.VocabularyBookUnsavedChanges(VocabularyBook.UnsavedChanges);
        Parent?.VocabularyBookName(VocabularyBook.Name);

        if (VocabularyBook.UnsavedChanges && Settings.Default.AutoSave && !string.IsNullOrWhiteSpace(VocabularyBook.FilePath))
        {
            if (VocabularyFile.WriteVhfFile(VocabularyBook.FilePath, VocabularyBook) &&
                VocabularyFile.WriteVhrFile(VocabularyBook))
            {
                VocabularyBook.UnsavedChanges = false;
            }
        }
    }

    private void OnStatisticsChanged(object sender, EventArgs e)
    {
        if (Parent == null)
            return;

        Parent.StatisticsPanel.Unpracticed = VocabularyBook.Statistics.Unpracticed;
        Parent.StatisticsPanel.WronglyPracticed = VocabularyBook.Statistics.WronglyPracticed;
        Parent.StatisticsPanel.CorrectlyPracticed = VocabularyBook.Statistics.CorrectlyPracticed;
        Parent.StatisticsPanel.FullyPracticed = VocabularyBook.Statistics.FullyPracticed;

        Parent.VocabularyBookHasContent(VocabularyBook.Words.Count > 0);
        Parent.VocabularyBookPracticable(VocabularyBook.Statistics.NotFullyPracticed > 0);
    }

    private void OnSelectionChanged(object sender, EventArgs e)
    {
        Parent?.VocabularyWordSelected(ListView.SelectedItems.Count > 0);
    }

    private void OnDoubleClick(object sender, EventArgs e)
    {
        if (ListView.SelectedItem != null)
            Parent?.EditWord();
    }

    private void OnSearchTextChanged(object sender, EventArgs e)
    {
        foreach (VocabularyWordController controller in WordControllers)
        {
            controller.Highlight(Parent.SearchText.Text.ToUpper());
        }
    }

    private void AddItem(VocabularyWord item)
    {
        VocabularyWordController controller = new VocabularyWordController(item);
        wordControllers.Add(controller);
        ListView.Items.Add(controller.ListViewItem);
    }

    private void RemoveItem(VocabularyWord item)
    {
        VocabularyWordController controller = GetController(item);
        wordControllers.Remove(controller);
        ListView.Items.Remove(controller.ListViewItem);
    }

    public void Dispose()
    {
        ((IDisposable)ListView).Dispose();
    }
}

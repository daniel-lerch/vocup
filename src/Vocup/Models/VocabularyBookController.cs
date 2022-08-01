using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using Vocup.Controls;
using Vocup.IO;

#nullable disable

namespace Vocup.Models;

public class VocabularyBookController : IDisposable
{
    private readonly List<VocabularyWordController> wordControllers;
    private IMainForm _parent;

    public VocabularyBookController(BookContext bookContext)
    {
        ListView = new VocabularyListView { Dock = DockStyle.Fill, };
        ListView.ItemSelectionChanged += OnSelectionChanged;
        ListView.Control.DoubleClick += OnDoubleClick;
        wordControllers = new List<VocabularyWordController>();
        WordControllers = new ReadOnlyCollection<VocabularyWordController>(wordControllers);
        //book.Words.OnAdd(AddItem);
        //book.Words.OnRemove(RemoveItem);
        bookContext.Book.PropertyChanged += OnPropertyChanged;
        BookContext = bookContext;
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
    public BookContext BookContext { get; }
    IReadOnlyCollection<VocabularyWordController> WordControllers { get; }

    public VocabularyWordController GetController(Word word)
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
        ListView.MotherTongue = BookContext.Book.MotherTongue;
        ListView.ForeignLang = BookContext.Book.ForeignLanguage;
        Parent?.VocabularyBookHasFilePath(!string.IsNullOrWhiteSpace(BookContext.FilePath));
        Parent?.VocabularyBookUnsavedChanges(BookContext.UnsavedChanges);
        Parent?.VocabularyBookName(BookContext.Name);

        //if (VocabularyBook.UnsavedChanges && Program.Settings.AutoSave && !string.IsNullOrWhiteSpace(VocabularyBook.FilePath))
        //{
        //    if (VocabularyFile.WriteVhfFile(VocabularyBook.FilePath, VocabularyBook) &&
        //        VocabularyFile.WriteVhrFile(VocabularyBook))
        //    {
        //        VocabularyBook.UnsavedChanges = false;
        //    }
        //}

        OnStatisticsChanged(sender, EventArgs.Empty);
    }

    private void OnStatisticsChanged(object sender, EventArgs e)
    {
        if (Parent == null)
            return;

        Parent.StatisticsPanel.Unpracticed = BookContext.Book.Unpracticed;
        Parent.StatisticsPanel.WronglyPracticed = BookContext.Book.WronglyPracticed;
        Parent.StatisticsPanel.CorrectlyPracticed = BookContext.Book.CorrectlyPracticed;
        Parent.StatisticsPanel.FullyPracticed = BookContext.Book.FullyPracticed;

        Parent.VocabularyBookHasContent(BookContext.Book.Words.Count > 0);
        Parent.VocabularyBookPracticable(BookContext.Book.NotFullyPracticed > 0);
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

    private void AddItem(Word item)
    {
        VocabularyWordController controller = new VocabularyWordController(item);
        wordControllers.Add(controller);
        ListView.Items.Add(controller.ListViewItem);
    }

    private void RemoveItem(Word item)
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

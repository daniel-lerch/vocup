using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls;

public partial class VocabularyListView : UserControl
{
    private Size _imageBaseSize = new Size(16, 16);
    private readonly int initialWidthImage = 20;
    private readonly int initialWidthLastPracticed = 100;
    private readonly int initialWidthCreationTime = 100;

    private SizeF scalingFactor = new SizeF(1F, 1F);
    private int scaledWidthImage;
    private int scaledWidthLastPracticed;
    private int scaledWidthCreationTime;

    public VocabularyListView()
    {
        scaledWidthImage = initialWidthImage;
        scaledWidthLastPracticed = initialWidthLastPracticed;
        scaledWidthCreationTime = initialWidthCreationTime;

        InitializeComponent();

        MainListView.ListViewItemSorter = new Sorter() { Column = 1, SortOrder = SortOrder.Ascending };
        MainListView.Sorting = SortOrder.Ascending;
        MainListView.ItemSelectionChanged += MainListView_ItemSelectionChanged;
    }

    public ListView Control => MainListView;

    [DefaultValue(typeof(Size), "16,16")]
    public Size ImageBaseSize
    {
        get => _imageBaseSize;
        set
        {
            _imageBaseSize = value;
            ScaleImageList();
        }
    }

    public ListView.ListViewItemCollection Items => MainListView.Items;
    public ListView.SelectedListViewItemCollection SelectedItems => MainListView.SelectedItems;
    public ListViewItem? SelectedItem => SelectedItems.Count > 0 ? SelectedItems[0] : null;

    [DefaultValue("")]
    public string MotherTongue
    {
        get => motherTongueColumn.Text;
        set => motherTongueColumn.Text = value;
    }

    [DefaultValue("")]
    public string ForeignLang
    {
        get => foreignLangColumn.Text;
        set => foreignLangColumn.Text = value;
    }

    public event ListViewItemSelectionChangedEventHandler? ItemSelectionChanged;

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
        scalingFactor = scalingFactor.Multiply(factor);
        scaledWidthImage = (int)Math.Round(initialWidthImage * scalingFactor.Width);
        scaledWidthLastPracticed = (int)Math.Round(initialWidthLastPracticed * scalingFactor.Width);
        scaledWidthCreationTime = (int)Math.Round(initialWidthCreationTime * scalingFactor.Width);

        imageColumn.Width = scaledWidthImage;
        // Here we don't save defaults and therefore directly scale with factor.
        motherTongueColumn.Width = (int)Math.Round(motherTongueColumn.Width * factor.Width);
        foreignLangColumn.Width = (int)Math.Round(foreignLangColumn.Width * factor.Width);
        lastPracticedColumn.Width = scaledWidthLastPracticed;
        creationTimeColumn.Width = scaledWidthCreationTime;

        ScaleImageList();

        base.ScaleControl(factor, specified);
    }

    protected override void OnLayout(LayoutEventArgs e)
    {
        // Since .NET 6.0 ScaleControl is not called at 100% scaling anymore
        if (MainListView.SmallImageList is null && scalingFactor.Width == 1 && scalingFactor.Height == 1)
            ScaleImageList();
        base.OnLayout(e);
    }

    private void ScaleImageList()
    {
        ImageList? old = MainListView.SmallImageList;
        MainListView.SmallImageList = IconImageList.Scale(_imageBaseSize.Multiply(scalingFactor).Rectify().Round());
        old?.Dispose();
    }

    private void MainListView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        Sorter sorter = (Sorter)MainListView.ListViewItemSorter!;
        if (sorter.Column == e.Column)
        {
            if (sorter.SortOrder == SortOrder.Ascending)
                sorter.SortOrder = SortOrder.Descending;
            else
                sorter.SortOrder = SortOrder.Ascending;
            MainListView.Sorting = sorter.SortOrder;
        }
        else
        {
            sorter.Column = e.Column;
            sorter.SortOrder = SortOrder.Ascending;
            MainListView.Sorting = sorter.SortOrder;
            MainListView.Sort();
        }
    }

    // Prevents the user from changing these columns
    private void MainListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
    {
        if (e.ColumnIndex == 0)
        {
            e.Cancel = true;
            e.NewWidth = scaledWidthImage;
        }
        else if (e.ColumnIndex == 3)
        {
            e.Cancel = true;
            e.NewWidth = scaledWidthLastPracticed;
        }
        else if (e.ColumnIndex == 4)
        {
            e.Cancel = true;
            e.NewWidth = scaledWidthCreationTime;
        }
    }

    // Prevents the ListView component from changing these columns on resize
    private void MainListView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
        if (e.ColumnIndex == 0)
        {
            imageColumn.Width = scaledWidthImage;
        }
        else if (e.ColumnIndex == 3)
        {
            lastPracticedColumn.Width = scaledWidthLastPracticed;
        }
        else if (e.ColumnIndex == 4)
        {
            creationTimeColumn.Width = scaledWidthCreationTime;
        }
    }

    private void MainListView_Resize(object sender, EventArgs e)
    {
        if (Program.Settings.ColumnResize)
        {
            int include = SystemInformation.VerticalScrollBarWidth + MainListView.Columns.Count;
            int availableWidth = MainListView.Width - imageColumn.Width - include;
            
            float listViewWidth = MainListView.Width / scalingFactor.Width;
            if (listViewWidth > 400)
                availableWidth -= lastPracticedColumn.Width; // Keep last practiced column if enough space
            if (listViewWidth > 500)
                availableWidth -= creationTimeColumn.Width; // Keep creation time column if enough space

            int width = availableWidth / 2;
            motherTongueColumn.Width = width;
            foreignLangColumn.Width = width;
        }
    }

    private void MainListView_ItemSelectionChanged(object? sender, ListViewItemSelectionChangedEventArgs e)
    {
        ItemSelectionChanged?.Invoke(sender, e);
    }

    public class Sorter : IComparer
    {
        public SortOrder SortOrder { get; set; }

        public int Column { get; set; }

        public int Compare(object? x, object? y)
        {
            if (SortOrder == SortOrder.None || x == null || y == null)
                return 0;

            switch (Column)
            {
                case 0:
                    return Inv(((ListViewItem)x).ImageIndex.CompareTo(((ListViewItem)y).ImageIndex));
                case 1:
                case 2:
                    return Inv(((ListViewItem)x).SubItems[Column].Text.CompareTo(((ListViewItem)y).SubItems[Column].Text));
                case 3:
                    return Inv(CompareDates(x, y, 3));
                case 4:
                    return Inv(CompareDates(x, y, 4));
                default:
                    throw new NotImplementedException();
            }
        }

        private int Inv(int ascending)
        {
            if (SortOrder != SortOrder.Descending)
                return ascending;
            else
                return -ascending;
        }

        private int CompareDates(object x, object y, int index)
        {
            string left = ((ListViewItem)x).SubItems[index].Text;
            string right = ((ListViewItem)y).SubItems[index].Text;

            if (!DateTime.TryParse(left, out DateTime leftTime))
                leftTime = DateTime.MinValue;
            if (!DateTime.TryParse(right, out DateTime rightTime))
                rightTime = DateTime.MinValue;
            return leftTime.CompareTo(rightTime);
        }
    }
}

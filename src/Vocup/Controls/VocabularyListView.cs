using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls
{
    public partial class VocabularyListView : UserControl
    {
        private int initialWidthImage = 20;
        private int initialWidthLastPracticed = 100;

        private SizeF scalingFactor = new SizeF(1F, 1F);
        private int scaledWidthImage;
        private int scaledWidthLastPracticed;

        public VocabularyListView()
        {
            scaledWidthImage = initialWidthImage;
            scaledWidthLastPracticed = initialWidthLastPracticed;

            InitializeComponent();

            MainListView.ListViewItemSorter = new Sorter() { Column = 1, SortOrder = SortOrder.Ascending };
            MainListView.Sorting = SortOrder.Ascending;
            MainListView.ItemSelectionChanged += MainListView_ItemSelectionChanged;
        }

        public ListView Control => MainListView;

        public bool GridLines
        {
            get => MainListView.GridLines;
            set => MainListView.GridLines = value;
        }

        public ListView.ListViewItemCollection Items => MainListView.Items;
        public ListView.SelectedListViewItemCollection SelectedItems => MainListView.SelectedItems;

        public string MotherTongue
        {
            get => motherTongueColumn.Text;
            set => motherTongueColumn.Text = value;
        }

        public string ForeignLang
        {
            get => foreignLangColumn.Text;
            set => foreignLangColumn.Text = value;
        }

        public event ListViewItemSelectionChangedEventHandler ItemSelectionChanged;

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            scalingFactor = SizeMath.Multiply(scalingFactor, factor);
            scaledWidthImage = (int)Math.Round(initialWidthImage * scalingFactor.Width);
            scaledWidthLastPracticed = (int)Math.Round(initialWidthLastPracticed * scalingFactor.Width);

            imageColumn.Width = scaledWidthImage;
            motherTongueColumn.Width = (int)Math.Round(motherTongueColumn.Width * factor.Width); // Here we don't save defaults and
            foreignLangColumn.Width = (int)Math.Round(foreignLangColumn.Width * factor.Width);   // therefore directly scale with factor.
            lastPracticedColumn.Width = scaledWidthLastPracticed;

            base.ScaleControl(factor, specified);
        }

        private void MainListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            Sorter sorter = (Sorter)MainListView.ListViewItemSorter;
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
        }

        private void MainListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ItemSelectionChanged?.Invoke(sender, e);
        }

        public class Sorter : IComparer
        {
            public SortOrder SortOrder { get; set; }

            public int Column { get; set; }

            public int Compare(object x, object y)
            {
                if (SortOrder == SortOrder.None)
                    return 0;

                switch (Column)
                {
                    case 0:
                        return Inv(((ListViewItem)x).ImageIndex.CompareTo(((ListViewItem)y).ImageIndex));
                    case 1:
                    case 2:
                        return Inv(((ListViewItem)x).SubItems[Column].Text.CompareTo(((ListViewItem)y).SubItems[Column].Text));
                    case 3:
                        string left = ((ListViewItem)x).SubItems[3].Text;
                        string right = ((ListViewItem)y).SubItems[3].Text;
                        if (!DateTime.TryParse(left, out DateTime leftTime))
                            leftTime = DateTime.MinValue;
                        if (!DateTime.TryParse(right, out DateTime rightTime))
                            rightTime = DateTime.MinValue;
                        return Inv(leftTime.CompareTo(rightTime));
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
        }
    }
}
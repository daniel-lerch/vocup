using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Controls
{
    public partial class VocabularyListView : UserControl
    {
        // TODO: Prevent column width resizing on form resize

        private int initialWidthImage;
        private int initialWidthLastPraticed;

        public VocabularyListView()
        {
            InitializeComponent();
            initialWidthImage = imageColumn.Width;
            initialWidthLastPraticed = lastPracticedColumn.Width;
            MainListView.ListViewItemSorter = new Sorter() { Column = 1, SortOrder = SortOrder.Ascending };
            MainListView.Sorting = SortOrder.Ascending;
        }

        public ListView Control => MainListView;

        public bool GridLines
        {
            get => MainListView.GridLines;
            set => MainListView.GridLines = value;
        }

        public ListView.ListViewItemCollection Items => MainListView.Items;

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

        private void MainListView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                e.Cancel = true;
                e.NewWidth = initialWidthImage;
            }
            else if (e.ColumnIndex == 3)
            {
                e.Cancel = true;
                e.NewWidth = initialWidthLastPraticed;
            }
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
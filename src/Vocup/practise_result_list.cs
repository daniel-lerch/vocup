using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup
{
    public partial class practise_result_list : Form
    {
        public string[,] result_list;
        public int anzahl;

        public string own_language;
        public string foreign_language;

        public practise_result_list()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.statistics.GetHicon());
        }

        //ListView Spaltenbreite festlegen

        private void listView_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            //Verhindert, dass die Spaltenbreite von column 0 geändert werden kann

            if (listView.Enabled && e.ColumnIndex == 0)
            {
                e.Cancel = true;
                e.NewWidth = 20;
            }
        }

        //OK Button

        private void ok_button_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void practise_result_list_Load(object sender, EventArgs e)
        {
            //Listview vorbereiten
            listView.BeginUpdate();

            listView.Columns.Add("", 20);
            listView.Columns.Add(own_language, 153);
            listView.Columns.Add(foreign_language, 153);
            listView.Columns.Add(Properties.Words.Misentry, 153);

            //Gitternetz
            listView.GridLines = Properties.Settings.Default.GridLines;

            listView.EndUpdate();

            listView.BeginUpdate();

            for (int i = 0; i < anzahl; i++)
            {

                if (result_list[i, 4] != "" && result_list[i, 4] != null)
                {
                    listView.Items.Add(new ListViewItem(new string[] { "", result_list[i, 2], result_list[i, 3] + "=" + result_list[i, 4], result_list[i, 7] }));
                }
                else
                {
                    listView.Items.Add(new ListViewItem(new string[] { "", result_list[i, 2], result_list[i, 3], result_list[i, 7] }));
                }


                if (result_list[i, 6] == null || result_list[i, 6] == "")
                {
                    listView.Items[i].ImageIndex = 0;
                }
                else if (result_list[i, 6] == "false")
                {
                    listView.Items[i].ImageIndex = 1;
                }
                else if (result_list[i, 6] == "nearly right")
                {
                    listView.Items[i].ImageIndex = 2;
                }
                else if (result_list[i, 6] == "right")
                {
                    listView.Items[i].ImageIndex = 3;
                }
            }

            listView.EndUpdate();
            listView.Enabled = true;
        }
    }
}
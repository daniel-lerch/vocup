using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Vocup.Models;
using Vocup.Properties;
using Vocup.Util;

namespace Vocup.Forms
{
    public partial class PrintWordSelection : Form
    {
        VocabularyBook book;

        public PrintWordSelection(VocabularyBook book)
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.Print.GetHicon());

            this.book = book;
        }

        //Array, das die Status-Informationen der Vokabeln enthält
        public int[] vocable_state;

        private void BtnCheckAll_Click(object sender, EventArgs e) //Falls auf alle Auswählen geklickt wurde
        {
            ListBox.BeginUpdate();

            for (int i = 0; i < ListBox.Items.Count; i++)
            {
                ListBox.SetItemChecked(i, true);
            }

            ListBox.EndUpdate();

            BtnContinue.Enabled = true;
            BtnContinue.Focus(); //Fokus auf weiter-Button
        }

        private void BtnUncheckAll_Click(object sender, EventArgs e) //Falls auf alle Abwählen geklickt wurde
        {
            ListBox.BeginUpdate();

            for (int i = 0; i < ListBox.Items.Count; i++)
            {
                ListBox.SetItemChecked(i, false);
            }

            ListBox.EndUpdate();

            BtnContinue.Enabled = false;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (ListBox.CheckedItems.Count == 0)
            {
                BtnContinue.Enabled = false;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void RbList_CheckedChanged(object sender, EventArgs e)
        {
            GroupPracticeMode.Enabled = RbList.Checked;
        }

        private void ListBox_SelectedValueChanged(object sender, EventArgs e)
        {
            BtnContinue.Enabled = ListBox.CheckedItems.Count > 0;
        }

        //Nur Vokabeln üben die, noch nie geübt wurden
        private void CbUnpracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCheckAll.Enabled)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = ListBox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    ListBox.SetItemChecked(i, false);
                }

                BtnCheckAll.Enabled = false;
                BtnUncheckAll.Enabled = false;
            }

            if (CbUnpracticed.Checked)
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] == 0)
                    {
                        ListBox.SetItemChecked(i, true);
                    }
                }

                BtnContinue.Enabled = true;
            }
            else
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] == 0)
                    {
                        ListBox.SetItemChecked(i, false);
                    }
                }
            }

            if (CbWronglyPracticed.Checked == false && CbCorrectlyPracticed.Checked == false && CbUnpracticed.Checked == false && CbFullyPracticed.Checked == false)
            {
                BtnCheckAll.Enabled = true;
                BtnUncheckAll.Enabled = true;

                BtnCheckAll_Click(sender, e);
            }
        }

        //Falsch übersetzt wurden
        private void CbWronglyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCheckAll.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = ListBox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    ListBox.SetItemChecked(i, false);
                }

                BtnCheckAll.Enabled = false;
                BtnUncheckAll.Enabled = false;
            }

            if (CbWronglyPracticed.Checked == true)
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] == 1)
                    {
                        ListBox.SetItemChecked(i, true);
                    }
                }

                BtnContinue.Enabled = true;
            }
            else
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] == 1)
                    {
                        ListBox.SetItemChecked(i, false);
                    }
                }
            }

            if (CbWronglyPracticed.Checked == false && CbCorrectlyPracticed.Checked == false && CbUnpracticed.Checked == false && CbFullyPracticed.Checked == false)
            {
                BtnCheckAll.Enabled = true;
                BtnUncheckAll.Enabled = true;

                BtnCheckAll_Click(sender, e);
            }
        }

        //mindestens einmal richtig geübt wurden
        private void CbCorrectlyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCheckAll.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                int anzahl = ListBox.Items.Count;

                for (int i = 0; i < anzahl; i++)
                {
                    ListBox.SetItemChecked(i, false);
                }

                BtnCheckAll.Enabled = false;
                BtnUncheckAll.Enabled = false;
            }

            if (CbCorrectlyPracticed.Checked == true)
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] > 1 && vocable_state[i] <= Properties.Settings.Default.MaxPracticeCount)
                    {
                        ListBox.SetItemChecked(i, true);
                    }
                }
                BtnContinue.Enabled = true;
            }
            else
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] > 1 && vocable_state[i] <= Properties.Settings.Default.MaxPracticeCount)
                    {
                        ListBox.SetItemChecked(i, false);
                    }
                }
            }


            if (CbWronglyPracticed.Checked == false && CbCorrectlyPracticed.Checked == false && CbUnpracticed.Checked == false && CbFullyPracticed.Checked == false)
            {
                BtnCheckAll.Enabled = true;
                BtnUncheckAll.Enabled = true;

                BtnCheckAll_Click(sender, e);
            }
        }

        //als gelernt markiert wurden
        private void CbFullyPracticed_CheckedChanged(object sender, EventArgs e)
        {
            if (BtnCheckAll.Enabled == true)
            {
                //Falls auf alle Abwählen geklickt wurde

                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    ListBox.SetItemChecked(i, false);
                }

                BtnCheckAll.Enabled = false;
                BtnUncheckAll.Enabled = false;
            }

            if (CbFullyPracticed.Checked == true)
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] > Settings.Default.MaxPracticeCount)
                    {
                        ListBox.SetItemChecked(i, true);
                    }
                }

                BtnContinue.Enabled = true;
            }
            else
            {
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    if (vocable_state[i] > Settings.Default.MaxPracticeCount)
                    {
                        ListBox.SetItemChecked(i, false);
                    }
                }
            }

            if (CbWronglyPracticed.Checked == false && CbCorrectlyPracticed.Checked == false && CbUnpracticed.Checked == false && CbFullyPracticed.Checked == false)
            {
                BtnCheckAll.Enabled = true;
                BtnUncheckAll.Enabled = true;

                BtnCheckAll_Click(sender, e);
            }
        }

        private void PrintList_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //Anzahl Seiten festlegen
            anzahl_seiten = (int)Math.Ceiling(anz_vok / 42d);
            aktuelle_seite = 1;
        }

        private List<VocabularyWord> printList;
        private int wordNumber = 0;
        private int pageNumber = 1;

        private void PrintList_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;

            using (Font siteFont = new Font("Arial", 11))
            using (StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center })
            {
                g.DrawString($"{Words.Site} {pageNumber}", siteFont, Brushes.Black, e.MarginBounds.MarginTop(25).SetHeight(15));
            }

            using (Font titleFont = new Font("Arial", 12, FontStyle.Bold))
            using (StringFormat centerFormat = new StringFormat() { Alignment = StringAlignment.Center })
            {
                string name = "";
                if (!string.IsNullOrWhiteSpace(book.FilePath))
                    name = $"{Path.GetFileNameWithoutExtension(book.FilePath)}: ";

                string title;
                if (book.PracticeMode == PracticeMode.AskForForeignLang)
                    title = $"{book.MotherTongue} - {book.ForeignLang}";
                else
                    title = $"{book.ForeignLang} - {book.MotherTongue}";

                g.DrawString(name + title, titleFont, Brushes.Black, e.MarginBounds.MarginTop(45).SetHeight(20));
            }

            int hoffset = e.MarginBounds.Top + 85;

            using (Font font = new Font("Arial", 10))
            using (StringFormat nearFormat = new StringFormat() { Alignment = StringAlignment.Near })
            using (Pen pen = new Pen(Brushes.Black, 1F))
            {
                do // loop through printList
                {
                    g.DrawLine(pen, )
                    VocabularyWord word = printList[wordNumber];

                } while (true);
            }

            //Schrift
            Font font_vocable = new Font("Arial", 8);

            //stift
            Pen pen = new Pen(Brushes.Black, 1);

            //Rechtsbündig
            StringFormat format_near = new StringFormat();
            format_near.Alignment = StringAlignment.Near;

            //Ränder
            int left = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Left, 1, MidpointRounding.AwayFromZero));
            int right = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Right, 1, MidpointRounding.AwayFromZero));
            int top = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Top, 1, MidpointRounding.AwayFromZero));
            int bottom = Convert.ToInt32(Math.Round(e.PageSettings.PrintableArea.Bottom, 1, MidpointRounding.AwayFromZero));



            //Linien und Wörter einfügen

            int noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 42;
            int vok_beginnen = (aktuelle_seite - 1) * 42 + 1;

            //Falls volle Seiten gedruckt werden können
            if (noch_nicht_gedruckt >= 42)
            {
                //Oberste Linie
                g.DrawLine(pen, 60 - left, 65 - top, 767 - left, 65 - top);
                //Mittellinie
                g.DrawLine(pen, 413 - left, 65 - top, 413 - left, 1115 - top);
                //Seitenlinien
                g.DrawLine(pen, 60 - left, 65 - top, 60 - left, 1115 - top);
                g.DrawLine(pen, 767 - left, 65 - top, 767 - left, 1115 - top);
                //unterste Linie
                //g.DrawLine(pen, 60 - left, 1095 - top, 767 - left, 1120 - top);

                for (int i = 0; i < 42; i++)
                {

                    SizeF size_own = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable);
                    SizeF size_foreign = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable);

                    //Falls der Text zu gross ist
                    if (size_own.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    else //Falls Text nicht zu gross
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                    }
                    //Falls Text zu gross || Synonym
                    if (size_foreign.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);

                                }
                            }

                        } while (is_good == false);

                    }
                    else //Falls Text nicht zu gross
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                    }


                    //Untere Linie zeichnen
                    g.DrawLine(pen, 60 - left, 90 + i * 25 - top, 767 - left, 90 + i * 25 - top);
                }
            }
            else //Falls letzte Seite, und nicht voll
            {
                //Oberste Linie
                g.DrawLine(pen, 60 - left, 65 - top, 767 - left, 65 - top);
                //Mittellinie
                g.DrawLine(pen, 413 - left, 65 - top, 413 - left, 65 + 25 * noch_nicht_gedruckt - top);
                //Seitenlinien
                g.DrawLine(pen, 60 - left, 65 - top, 60 - left, 65 + 25 * noch_nicht_gedruckt - top);
                g.DrawLine(pen, 767 - left, 65 - top, 767 - left, 65 + 25 * noch_nicht_gedruckt - top);


                for (int i = 0; i < noch_nicht_gedruckt; i++)
                {

                    SizeF size_own = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable);
                    SizeF size_foreign = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable);

                    //Falls der Text zu gross ist
                    if (size_own.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    //Falls Text nicht zu gross
                    else
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                    }
                    //Falls Text zu gross || Synonym
                    if (size_foreign.Width > 413 - 62 - left)
                    {
                        bool is_good;
                        int font_size = 8;
                        do
                        {
                            font_size--;
                            Font font_new = new Font("Arial", font_size);

                            SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new);

                            if (string_size.Width > 413 - 62 - left && font_size > 1)
                            {
                                is_good = false;
                            }
                            else
                            {
                                is_good = true;

                                //kleinerer Text schreiben
                                if (if_own_to_foreign == true)
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                                }
                                else
                                {
                                    g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_new, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                                }
                            }

                        } while (is_good == false);

                    }
                    //Falls Text nicht zu gross
                    else
                    {
                        if (if_own_to_foreign == true)
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(415 - left, 70 + i * 25 - top), format_near);
                        }
                        else
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font_vocable, Brushes.Black, new Point(62 - left, 70 + i * 25 - top), format_near);
                        }
                    }

                    //Untere Linie zeichnen
                    g.DrawLine(pen, 60 - left, 90 + i * 25 - top, 767 - left, 90 + i * 25 - top);
                }
            }


            //Schauen, ob noch mehr Seiten gedruckt werden müssen

            if (aktuelle_seite != anzahl_seiten)
            {
                e.HasMorePages = true;
                aktuelle_seite++;
            }
            else
            {
                e.HasMorePages = false;
            }
        }
    }
}
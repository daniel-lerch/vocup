using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup.Forms
{
    public partial class PrintCardsDialog : Form
    {
        ListView listView_vokabeln;

        //Liste der Vokabeln die ausgedruckt werden soll
        int[] vokabelliste;

        //Anzahl Vokabeln beim Drucken
        int anz_vok;

        //Anzahl zu druckende Seiten
        int anzahl_seiten;

        //Aktuelle zu druckende Seite
        int aktuelle_seite;

        //Vorder- oder Rückseite bei den Kärtchen
        bool if_foreside;

        //Papiereinzug
        bool is_front;

        public PrintCardsDialog()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.Print.GetHicon());
        }

        private void PrintCards_BeginPrint(object sender, PrintEventArgs e)
        {
            //Anzahl Seiten festlegen
            anzahl_seiten = (int)Math.Ceiling(anz_vok / 16d);
            aktuelle_seite = 1;
        }

        private void PrintCards_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Display;
            //1/100 zoll * 0.254 = mm
            //1169|827 (Daten A4-Seite)
            //Seitenränder abfragen

            int left = (int)Math.Round(e.PageSettings.PrintableArea.Left, 1, MidpointRounding.AwayFromZero);
            int right = (int)Math.Round(e.PageSettings.PrintableArea.Right, 1, MidpointRounding.AwayFromZero);
            int top = (int)Math.Round(e.PageSettings.PrintableArea.Top, 1, MidpointRounding.AwayFromZero);
            int bottom = (int)Math.Round(e.PageSettings.PrintableArea.Bottom, 1, MidpointRounding.AwayFromZero);

            //Stift

            Pen pen = new Pen(Color.Black, 1);

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;

            Font font = new Font("Arial", 12);

            //Vorderseite
            if (if_foreside == true)
            {
                //Linien zeichnen

                //Vertikal
                g.DrawLine(pen, 207 - left, 0, 207 - left, 1180);
                g.DrawLine(pen, 413 - left, 0, 413 - left, 1180);
                g.DrawLine(pen, 620 - left, 0, 620 - left, 1180);

                //Horizontal
                g.DrawLine(pen, 0, 292 - top, 866, 292 - top);
                g.DrawLine(pen, 0, 585 - top, 866, 585 - top);
                g.DrawLine(pen, 0, 877 - top, 866, 877 - top);

                //Seite rotieren ||X-Koordinaten negativ, Y-Koordinaten positiv
                g.RotateTransform(-90f);

                //Linien und Wörter einfügen

                int noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 16;
                int vok_beginnen = (aktuelle_seite - 1) * 16 + 1;

                //Falls noch mehr Seiten gedruckt werden müssen
                if (noch_nicht_gedruckt >= 16)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Vokabel schreiben
                        //Schriftgrösse anpassen

                        //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                        if (size_string.Width > 292)
                        {
                            bool is_good = false;
                            int font_size = 12;

                            if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Contains(" ") == true)
                            {
                                //Falls der String leerschläge enthält

                                string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Split(' ');

                                do
                                {
                                    Font font_new = new Font("Arial", font_size);

                                    for (int y = 1; y < splitter.Length; y++)
                                    {
                                        string part1 = "";
                                        string part2 = "";

                                        for (int x = 1; x <= splitter.Length - y; x++)
                                        {
                                            part1 = part1 + " " + splitter[x - 1];

                                            if (x == splitter.Length - y)
                                            {
                                                for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                {
                                                    part2 = part2 + " " + splitter[z];
                                                }
                                            }
                                        }

                                        SizeF size_part1 = g.MeasureString(part1, font_new);
                                        SizeF size_part2 = g.MeasureString(part2, font_new);

                                        if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                        {
                                            is_good = true;

                                            //zwei Zeilen schreiben

                                            g.DrawString(part1, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height - 20), format);
                                            g.DrawString(part2, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height + 20), format);

                                            break;
                                        }
                                    }

                                    if (is_good == false)
                                    {
                                        font_size--;
                                    }

                                } while (is_good == false);
                            }
                            else
                            {
                                do
                                {
                                    font_size--;
                                    Font font_new = new Font("Arial", font_size);

                                    SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                    if (string_size.Width > 292 && font_size > 1)
                                    {
                                        is_good = false;
                                    }
                                    else
                                    {
                                        is_good = true;

                                        //kleinerer Text schreiben
                                        g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                    }

                                } while (is_good == false);
                            }
                        }
                        else //Falls Text nicht zu gross
                        {
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                        }
                    }
                }
                else //Falls dies die letzte Seite ist
                {
                    for (int i = 0; i < noch_nicht_gedruckt; i++)
                    {
                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Vokabel schreiben
                        //Schriftgrösse anpassen

                        //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                        if (size_string.Width > 292)
                        {

                            bool is_good = false;
                            int font_size = 12;

                            if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Contains(" ") == true)
                            {
                                //Falls der String leerschläge enthält

                                string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text.Trim().Split(' ');

                                do
                                {
                                    Font font_new = new Font("Arial", font_size);

                                    for (int y = 1; y < splitter.Length; y++)
                                    {
                                        string part1 = "";
                                        string part2 = "";

                                        for (int x = 1; x <= splitter.Length - y; x++)
                                        {
                                            part1 = part1 + " " + splitter[x - 1];

                                            if (x == splitter.Length - y)
                                            {
                                                for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                {
                                                    part2 = part2 + " " + splitter[z];
                                                }
                                            }
                                        }


                                        SizeF size_part1 = g.MeasureString(part1, font_new);
                                        SizeF size_part2 = g.MeasureString(part2, font_new);

                                        if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                        {
                                            is_good = true;

                                            //zwei Zeilen schreiben

                                            g.DrawString(part1, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height - 20), format);
                                            g.DrawString(part2, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height + 20), format);

                                            break;
                                        }
                                    }

                                    if (is_good == false)
                                    {
                                        font_size--;
                                    }

                                } while (is_good == false);
                            }
                            else // Falls keine Leerzeichen vorhanden sind
                            {
                                do
                                {
                                    font_size--;
                                    Font font_new = new Font("Arial", font_size);

                                    SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                    if (string_size.Width > 292 && font_size > 1)
                                    {
                                        is_good = false;
                                    }
                                    else
                                    {
                                        is_good = true;

                                        //kleinerer Text schreiben
                                        g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                    }

                                } while (is_good == false);
                            }
                        }
                        else
                        {
                            //Falls Text nicht zu gross
                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                        }
                    }

                    //nicht benötigte Linien entfernen
                    g.RotateTransform(+90);

                    if (noch_nicht_gedruckt <= 4)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 292 - top + 1, 866, 1180));
                    }
                    else if (noch_nicht_gedruckt > 4 && noch_nicht_gedruckt <= 8)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 585 - top + 1, 866, 1180));
                    }
                    else if (noch_nicht_gedruckt > 8 && noch_nicht_gedruckt <= 12)
                    {
                        g.FillRectangle(Brushes.White, new Rectangle(0, 877 - top - top + 1, 866, 1180));
                    }

                    //Vertikale Linien


                    //Vertikal
                    //g.DrawLine(pen, 207 - left, 0, 207 - left, 1180);
                    //g.DrawLine(pen, 413 - left, 0, 413 - left, 1180);
                    //g.DrawLine(pen, 620 - left, 0, 620 - left, 1180);

                    ////Horizontal
                    //g.DrawLine(pen, 0, 292 - top, 866, 292 - top);
                    //g.DrawLine(pen, 0, 585 - top, 866, 585 - top);
                    //g.DrawLine(pen, 0, 877 - top, 866, 877 - top);

                    Rectangle rect = new Rectangle();

                    if (noch_nicht_gedruckt < 4)
                    {
                        rect.Y = 0;
                        rect.Height = 1180;
                    }
                    else if (noch_nicht_gedruckt > 4 && noch_nicht_gedruckt < 8)
                    {
                        rect.Y = 292 - top + 1;
                        rect.Height = 888;
                    }
                    else if (noch_nicht_gedruckt > 8 & noch_nicht_gedruckt < 12)
                    {
                        rect.Y = 585 - top + 1;
                        rect.Height = 593;
                    }
                    else if (noch_nicht_gedruckt > 12 & noch_nicht_gedruckt < 16)
                    {
                        rect.Y = 877 - top + 1;
                        rect.Height = 298;
                    }

                    if (noch_nicht_gedruckt == 1 || noch_nicht_gedruckt == 5 || noch_nicht_gedruckt == 9 || noch_nicht_gedruckt == 13)
                    {
                        rect.X = 207 - left + 1;
                        rect.Width = 650;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    else if (noch_nicht_gedruckt == 2 || noch_nicht_gedruckt == 6 || noch_nicht_gedruckt == 10 || noch_nicht_gedruckt == 14)
                    {
                        rect.X = 413 - left + 1;
                        rect.Width = 435;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    else if (noch_nicht_gedruckt == 3 || noch_nicht_gedruckt == 7 || noch_nicht_gedruckt == 11)
                    {
                        rect.X = 620 - left + 1;
                        rect.Width = 218;

                        g.FillRectangle(Brushes.White, rect);
                    }
                    g.RotateTransform(-90);
                }

                //Pfeil zeichnen
                if (aktuelle_seite == anzahl_seiten || aktuelle_seite == 1)
                {
                    //rotieren

                    g.RotateTransform(+90);
                    Font pfeil = new Font("Arial", 12, FontStyle.Bold);

                    g.DrawString("↑", pfeil, Brushes.Black, new Point(413 - left - 30, 0), format);
                    g.DrawString("↑", pfeil, Brushes.Black, new Point(413 - left + 30, 0), format);
                }

            }
            else //Rückseite
            {
                //Seite rotieren ||X-Koordinaten positiv, Y-Koordinaten negativ
                g.RotateTransform(+90);

                int noch_nicht_gedruckt;
                int vok_beginnen;

                if (is_front == true)
                {
                    noch_nicht_gedruckt = anz_vok - (anzahl_seiten - aktuelle_seite) * 16;
                    vok_beginnen = ((anzahl_seiten) - (aktuelle_seite)) * 16 + 1;
                }
                else
                {
                    noch_nicht_gedruckt = anz_vok - (aktuelle_seite - 1) * 16;
                    vok_beginnen = (aktuelle_seite - 1) * 16 + 1;
                }

                //Falls noch mehr Seiten gedruckt werden müssen
                if (noch_nicht_gedruckt >= 16)
                {

                    //Positionsverschiebung der Rückseite
                    int links_rechts_verschiebung = -3;

                    for (int i = 0; i < 16; i++)
                    {
                        //Positionszugabe ändern
                        switch (links_rechts_verschiebung)
                        {
                            case 1:
                                links_rechts_verschiebung = -1;
                                break;
                            case -1:
                                links_rechts_verschiebung = -3;
                                break;
                            case -3:
                                links_rechts_verschiebung = 3;
                                break;
                            case 3:
                                links_rechts_verschiebung = 1;
                                break;
                        }

                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1 + links_rechts_verschiebung);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Falls es ein Synonym gibt
                        if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Contains("=") == true)
                        {
                            string[] split_text = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Split('=');

                            SizeF size_foreign = g.MeasureString(split_text[0], font);
                            SizeF size_synonym = g.MeasureString(split_text[1], font);

                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[0].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[0].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 60 - height), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Foreign normal schreiben
                                g.DrawString(split_text[0], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20 - height), format);
                            }

                            //Trennlinie zeichnen
                            g.DrawLine(pen, new Point((coordinates[0] * (-1)) - top - 10, (coordinates[1] * (-1)) + left - height / 2), new Point((coordinates[0] * (-1)) - top + 10, (coordinates[1] * (-1)) + left - height / 2));

                            //Falls Synonym zu gross
                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[1].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[1].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }


                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 60), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Synonym normal schreiben
                                g.DrawString(split_text[1], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                            }
                            //Falls es kein Synonym gibt
                        }
                        else
                        {
                            //Schriftgrösse anpassen
                            //Falls Text zu gross

                            //Falls Text zu gross, string auf mehrere Zeilen aufteilen falls möglich
                            if (size_string.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                                else
                                {
                                    do
                                    {
                                        font_size--;
                                        Font font_new = new Font("Arial", font_size);

                                        SizeF string_size = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new);

                                        if (string_size.Width > 292 && font_size > 1)
                                        {
                                            is_good = false;
                                        }
                                        else
                                        {
                                            is_good = true;

                                            //kleinerer Text schreiben
                                            g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[1].Text, font_new, Brushes.Black, new Point(coordinates[0] + top, coordinates[1] - left - height), format);
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Normal schreiben
                                g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height), format);
                            }
                        }
                    }
                    //Falls letzte Seite
                }
                else
                {
                    //Positionsverschiebung der Rückseite
                    int links_rechts_verschiebung = -3;

                    for (int i = 0; i < noch_nicht_gedruckt; i++)
                    {

                        //Positionszugabe ändern
                        switch (links_rechts_verschiebung)
                        {
                            case 1:
                                links_rechts_verschiebung = -1;
                                break;
                            case -1:
                                links_rechts_verschiebung = -3;
                                break;
                            case -3:
                                links_rechts_verschiebung = 3;
                                break;
                            case 3:
                                links_rechts_verschiebung = 1;
                                break;
                        }

                        //Koordinaten abfragen
                        int[] coordinates = get_coordinates(i + 1 + links_rechts_verschiebung);

                        //Grösse des Textes abfragen

                        SizeF size_string = g.MeasureString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font);

                        int height = Convert.ToInt32(size_string.Height / 2);

                        //Schriftgrösse anpassen

                        //Vokabel schreiben
                        //Falls es ein Synonym gibt
                        if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Contains("=") == true)
                        {
                            string[] split_text = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Split('=');

                            SizeF size_foreign = g.MeasureString(split_text[0], font);
                            SizeF size_synonym = g.MeasureString(split_text[1], font);

                            //Falls Foreign zu gross
                            if (size_foreign.Width > 292)
                            {
                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[0].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[0].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben
                                                g.DrawString(part1, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 60 - height), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Foreign normal schreiben
                                g.DrawString(split_text[0], font, Brushes.Black, new Point(-coordinates[0] - top, -coordinates[1] + left - height - 20 - height), format);
                            }

                            //Trennlinie zeichnen
                            g.DrawLine(pen, new Point(-coordinates[0] - top - 10, -coordinates[1] + left - height / 2), new Point(-coordinates[0] - top + 10, -coordinates[1] + left - height / 2));

                            //Falls synonym zu gross
                            if (size_foreign.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (split_text[1].Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = split_text[1].Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }


                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben


                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 60), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Synonym normal schreiben
                                g.DrawString(split_text[1], font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);
                            }
                        }
                        else //Falls es kein Synonym gibt
                        {
                            //Schriftgrösse anpassen
                            //Falls Text zu gross
                            if (size_string.Width > 292)
                            {

                                bool is_good = false;
                                int font_size = 12;

                                if (listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Contains(" ") == true)
                                {
                                    //Falls der String leerschläge enthält

                                    string[] splitter = listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text.Trim().Split(' ');

                                    do
                                    {
                                        Font font_new = new Font("Arial", font_size);

                                        for (int y = 1; y < splitter.Length; y++)
                                        {
                                            string part1 = "";
                                            string part2 = "";

                                            for (int x = 1; x <= splitter.Length - y; x++)
                                            {
                                                part1 = part1 + " " + splitter[x - 1];

                                                if (x == splitter.Length - y)
                                                {
                                                    for (int z = splitter.Length - y; z < splitter.Length; z++)
                                                    {
                                                        part2 = part2 + " " + splitter[z];
                                                    }
                                                }
                                            }

                                            SizeF size_part1 = g.MeasureString(part1, font_new);
                                            SizeF size_part2 = g.MeasureString(part2, font_new);

                                            if (size_part1.Width <= 292 && size_part2.Width <= 292)
                                            {
                                                is_good = true;

                                                //zwei Zeilen schreiben

                                                g.DrawString(part1, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height - 20), format);
                                                g.DrawString(part2, font_new, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height + 20), format);

                                                break;
                                            }
                                        }

                                        if (is_good == false)
                                        {
                                            font_size--;
                                        }

                                    } while (is_good == false);
                                }
                            }
                            else
                            {
                                //Text normal schreiben
                                g.DrawString(listView_vokabeln.Items[vokabelliste[vok_beginnen - 1 + i]].SubItems[2].Text, font, Brushes.Black, new Point((coordinates[0] * (-1)) - top, (coordinates[1] * (-1)) + left - height), format);
                            }
                        }
                    }
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

        private int[] get_coordinates(int number)
        {
            switch (number)
            {
                case 01: return new int[] { -146, 103 };
                case 02: return new int[] { -146, 310 };
                case 03: return new int[] { -146, 516 };
                case 04: return new int[] { -146, 723 };
                case 05: return new int[] { -438, 103 };
                case 06: return new int[] { -438, 310 };
                case 07: return new int[] { -438, 516 };
                case 08: return new int[] { -438, 723 };
                case 09: return new int[] { -731, 103 };
                case 10: return new int[] { -731, 310 };
                case 11: return new int[] { -731, 516 };
                case 12: return new int[] { -731, 723 };
                case 13: return new int[] { -1023, 103 };
                case 14: return new int[] { -1023, 310 };
                case 15: return new int[] { -1023, 516 };
                default: return new int[] { -1023, 723 };
            }
        }
    }
}
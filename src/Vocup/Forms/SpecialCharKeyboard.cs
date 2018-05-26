using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Vocup.Forms
{
    public partial class SpecialCharKeyboard : Form
    {
        private TextBox textBox;

        public SpecialCharKeyboard()
        {
            InitializeComponent();
        }

        [Obsolete("", false)]
        public event EventHandler choose_char;

        public void RegisterTextBox(TextBox control)
        {
            textBox = control;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Util.AppInfo.SpecialCharDirectory);

            if (dirInfo.Exists)
            {
                foreach (FileInfo info in dirInfo.GetFiles("*.txt"))
                {
                    try
                    {
                        using (StreamReader reader = new StreamReader(info.FullName, Encoding.UTF8))
                        {
                            TabPage page = new TabPage()
                            {
                                Name = "Custom_" + Path.GetFileNameWithoutExtension(info.FullName),
                                Text = Path.GetFileNameWithoutExtension(info.FullName),
                                AutoScroll = true,
                                UseVisualStyleBackColor = true,
                                Font = new Font("Arial", 9.75f)
                            };

                            int index = 0;
                            int itemsPerLine = 12;
                            Point offset = new Point(8, 6);
                            Point space = new Point(6, 6);
                            Size size = new Size(25, 25);

                            while (!reader.EndOfStream)
                            {
                                string line = reader.ReadLine();

                                if (line.Length > 0)
                                    line = line.Substring(0, 1);
                                else
                                    continue;

                                int x = offset.X + (size.Width + space.X) * (index % itemsPerLine);
                                int y = offset.Y + (size.Height + space.Y) * (index / itemsPerLine);

                                Button button = new Button()
                                {
                                    Name = page.Name + "_Char_" + (ushort)line[0],
                                    Text = line,
                                    UseVisualStyleBackColor = true,
                                    Size = size,
                                    Location = new Point(x, y)
                                };
                                button.Click += Button_Click;

                                page.Controls.Add(button);
                                index++;
                            }

                            TcMain.TabPages.Add(page);
                        }
                    }
                    catch
                    {
                        // TODO: Show message when skipping file
                    }
                }
            }

            // Try to open recent tab
            string last = Properties.Settings.Default.sonderzeichen_registerkarte;
            if (!string.IsNullOrWhiteSpace(last) && TcMain.TabPages.ContainsKey(last))
                TcMain.SelectTab(last);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (textBox != null)
            {
                Button button = (Button)sender;
                textBox.Text += button.Text;
                textBox.SelectionStart = textBox.TextLength;
                textBox.Focus();
            }
            
            // For compatibility
            choose_char?.Invoke(sender, e);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.sonderzeichen_registerkarte = TcMain.SelectedTab.Name;
            Properties.Settings.Default.Save();
        }
    }
}
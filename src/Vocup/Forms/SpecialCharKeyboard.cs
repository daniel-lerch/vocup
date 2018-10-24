using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup.Forms
{
    public partial class SpecialCharKeyboard : Form
    {
        private TextBox textBox;

        public SpecialCharKeyboard()
        {
            InitializeComponent();
        }

        public void Initialize(Form owner)
        {
            Owner = owner;
            Owner.Move += Owner_MoveResize;
            Owner.Resize += Owner_MoveResize;
            Owner_MoveResize(Owner, null);
        }

        public void RegisterTextBox(TextBox control)
        {
            if (textBox != null)
                textBox.EnabledChanged -= TextBox_EnabledChanged;
            textBox = control;
            textBox.EnabledChanged += TextBox_EnabledChanged;
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
                            string name = Path.GetFileNameWithoutExtension(info.FullName);
                            TabPage page = new TabPage()
                            {
                                Name = "Custom_" + name,
                                Tag = "Custom_" + name,
                                Text = name,
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
                        MessageBox.Show(
                            string.Format(Messages.SpecialCharInvalidFile, Path.GetFileNameWithoutExtension(info.FullName), info.FullName),
                            Messages.SpecialCharInvalidFileT,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }

            // Try to open recent tab
            foreach (TabPage page in TcMain.TabPages)
            {
                if (page.Tag.ToString().Equals(Settings.Default.SpecialCharTab, StringComparison.OrdinalIgnoreCase))
                    TcMain.SelectTab(page);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (textBox != null)
            {
                Button button = (Button)sender;
                if (textBox.Enabled && !textBox.ReadOnly)
                    textBox.Text += button.Text;
                textBox.SelectionStart = textBox.TextLength;
                textBox.Focus();
            }
        }

        private void TextBox_EnabledChanged(object sender, EventArgs e)
        {
            Visible = textBox.Enabled;
        }

        private void Owner_MoveResize(object sender, EventArgs e)
        {
            Left = Owner.Left + (Owner.Width - Width) / 2;
            Top = Owner.Top + Owner.Height;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.SpecialCharTab = TcMain.SelectedTab.Tag.ToString();
            Settings.Default.Save();

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Visible = false;
            }
        }
    }
}
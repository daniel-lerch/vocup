using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

#nullable disable

namespace Vocup.Forms;

public partial class SpecialCharKeyboard : Form
{
    private TextBox textBox;
    private Button trigger;
    private bool _keyboardEnabled = true;

    public SpecialCharKeyboard()
    {
        InitializeComponent();

        Text = Words.SpecialChars;
        foreach (Control control in TcMain.Controls)
            if (control is TabPage page)
                page.Text = new CultureInfo(page.Tag.ToString()).DisplayName;
    }

    /// <summary>
    /// Gets or sets whether the keyboard can be opened via its trigger.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool KeyboardEnabled
    {
        get => _keyboardEnabled;
        set
        {
            if (value != _keyboardEnabled)
            {
                _keyboardEnabled = value;
                RefreshTriggerAndVisibility();
            }
        }
    }

    public void Initialize(Form owner, Button trigger)
    {
        Owner = owner;
        Owner.Move += Owner_MoveResize;
        Owner.Resize += Owner_MoveResize;
        Owner.FormClosing += Owner_FormClosing;
        Owner_MoveResize(Owner, EventArgs.Empty);

        this.trigger = trigger;
        this.trigger.Click += Trigger_Click;

        RefreshTriggerAndVisibility();
    }

    public void RegisterTextBox(TextBox control)
    {
        if (textBox != null)
            textBox.EnabledChanged -= TextBox_EnabledChanged;
        textBox = control;
        textBox.EnabledChanged += TextBox_EnabledChanged;

        RefreshTriggerAndVisibility();
    }

    private void RefreshTriggerAndVisibility()
    {
        trigger.Enabled = KeyboardEnabled && (textBox?.Enabled ?? false) && !Visible;
        if (Visible && (!KeyboardEnabled || textBox == null || !textBox.Enabled))
            Hide();
    }

    private void Form_Load(object sender, EventArgs e)
    {
        DirectoryInfo dirInfo = new(Util.AppInfo.SpecialCharDirectory);

        if (dirInfo.Exists)
        {
            foreach (FileInfo info in dirInfo.GetFiles("*.txt"))
            {
                try
                {
                    using StreamReader reader = new(info.FullName, Encoding.UTF8);
                    string name = Path.GetFileNameWithoutExtension(info.FullName);
                    TabPage page = new()
                    {
                        Name = "TpCustom" + name,
                        Tag = "Custom" + name,
                        Text = name,
                        AutoScroll = true,
                        UseVisualStyleBackColor = true,
                        Font = new Font("Arial", 9.75f)
                    };

                    int index = 0;
                    int itemsPerLine = 12;
                    Point offset = new(8, 6);
                    Point space = new(6, 6);
                    Size size = new(25, 25);

                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        if (line.Length > 0)
                            line = line.Substring(0, 1);
                        else
                            continue;

                        int x = offset.X + (size.Width + space.X) * (index % itemsPerLine);
                        int y = offset.Y + (size.Height + space.Y) * (index / itemsPerLine);

                        Button button = new()
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
                catch
                {
                    MessageBox.Show(
                        string.Format(Messages.SpecialCharCorruptedFile, Path.GetFileNameWithoutExtension(info.FullName), info.FullName),
                        Messages.SpecialCharCorruptedFileT,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        // Try to open recent tab
        foreach (TabPage page in TcMain.TabPages)
        {
            if (page.Tag.ToString().Equals(Program.Settings.SpecialCharTab, StringComparison.OrdinalIgnoreCase))
                TcMain.SelectTab(page);
        }
    }

    private void Button_Click(object sender, EventArgs e)
    {
        if (textBox != null)
        {
            Button button = (Button)sender;
            if (textBox.Enabled && !textBox.ReadOnly)
            {
                int selectionStart = textBox.SelectionStart;
                int selectionLength = textBox.SelectionLength;
                textBox.Text = textBox.Text
                    .Remove(selectionStart, selectionLength)
                    .Insert(selectionStart, button.Text);
                textBox.SelectionStart = selectionStart + button.Text.Length;
                textBox.SelectionLength = 0;
            }

            textBox.Focus();
        }
    }

    private void Trigger_Click(object sender, EventArgs e)
    {
        Show();
        textBox.Focus();
    }

    private void TextBox_EnabledChanged(object sender, EventArgs e)
    {
        RefreshTriggerAndVisibility();
    }

    private void Owner_MoveResize(object sender, EventArgs e)
    {
        Left = Owner.Left + (Owner.Width - Width) / 2;
        Top = Owner.Top + Owner.Height;
    }

    private void Owner_FormClosing(object sender, FormClosingEventArgs e)
    {
        Program.Settings.SpecialCharTab = TcMain.SelectedTab.Tag.ToString();
    }

    private void Form_VisibleChanged(object sender, EventArgs e)
    {
        RefreshTriggerAndVisibility();
    }

    private void Form_FormClosing(object sender, FormClosingEventArgs e)
    {
        Program.Settings.SpecialCharTab = TcMain.SelectedTab.Tag.ToString();

        if (e.CloseReason == CloseReason.UserClosing)
        {
            e.Cancel = true;
            Hide();
        }
    }
}

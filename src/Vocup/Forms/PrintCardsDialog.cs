using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Vocup.Properties;

namespace Vocup.Forms
{
    public partial class PrintCardsDialog : Form
    {
        public PrintCardsDialog()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Icons.Print.GetHicon());
        }
    }
}
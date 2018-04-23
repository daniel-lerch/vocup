using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vocup.Forms
{
    public partial class EvaluationInfoDialog : Form
    {
        public EvaluationInfoDialog()
        {
            InitializeComponent();
            Icon = Icon.FromHandle(icons.info.GetHicon());
        }
    }
}
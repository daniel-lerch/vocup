using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Vocup
{
    public partial class splashscreen : Form
    {
        public splashscreen()
        {
            InitializeComponent();
        }

        private void splashscreen_Paint(object sender, PaintEventArgs e)
        {
            

            Pen pen_line = new Pen(Brushes.Gray,2);
            
            //Linien zeichnen
            e.Graphics.DrawLine(pen_line, new Point(0,0),new Point(0,200));
            e.Graphics.DrawLine(pen_line, new Point(0, 0), new Point(450, 0));
            e.Graphics.DrawLine(pen_line,new Point(0,200), new Point(450,200));
            e.Graphics.DrawLine (pen_line, new Point(450,0), new Point(450,200));
        }

        private void splashscreen_Load(object sender, EventArgs e)
        {
            //Version anzeigen

            Version.Text = String.Format("Version {0}", Assembly.GetExecutingAssembly().GetName().Version.Major.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString() + "." + Assembly.GetExecutingAssembly().GetName().Version.Build.ToString()); 
        }
    }
}
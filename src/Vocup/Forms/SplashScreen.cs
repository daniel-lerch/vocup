using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Vocup.Forms
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Paint(object sender, PaintEventArgs e)
        {
            using (Pen grayPen = new Pen(Brushes.Gray, width: 2.0f))
            {
                // Draw window border

                e.Graphics.DrawLine(grayPen, new Point(0, 0), new Point(0, 200));
                e.Graphics.DrawLine(grayPen, new Point(0, 0), new Point(450, 0));
                e.Graphics.DrawLine(grayPen, new Point(0, 200), new Point(450, 200));
                e.Graphics.DrawLine(grayPen, new Point(450, 0), new Point(450, 200));
            }
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            // Display version and copyright

            LbVersion.Text = Util.AppInfo.GetVersion(3);
            LbCopyright.Text = AppInfo.Copyright;
        }
    }
}
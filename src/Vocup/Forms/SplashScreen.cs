using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            using (LinearGradientBrush brush = new LinearGradientBrush(new Point(0, 0), new Point(0, 200), Color.White, Color.LightGray))
            using (Pen grayPen = new Pen(Brushes.Gray, width: 2.0f))
            {
                // Draw background
                e.Graphics.FillRectangle(brush, 0, 0, Width, Height);

                // Draw window border
                e.Graphics.DrawRectangle(grayPen, 0, 0, Width, Height);
            }
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            // Display version and copyright

            LbVersion.Text = string.Format(LbVersion.Text, Util.AppInfo.GetVersion(3));
            LbCopyright.Text = Util.AppInfo.CopyrightInfo;
        }
    }
}
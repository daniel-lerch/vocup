﻿using PInvoke;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Vocup.Windows
{
    public partial class SplashScreen : Form
    {
        public static void StartShow()
        {
            ThreadPool.QueueUserWorkItem(RunSplashScreen);
        }

        private static void RunSplashScreen(object? state)
        {
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            // Visual stlyes are not necessary for the splash screen so we omit this step
            Application.SetCompatibleTextRenderingDefault(false);
            using SplashScreen splashScreen = new();
            Application.Run(splashScreen);
        }

        public static void Close(IntPtr handle)
        {
            User32.SetForegroundWindow(handle);
            Application.Exit();
        }


        /// <summary>
        /// The position relative to the control where the left mouse button down was pressed last.
        /// </summary>
        private Point dragOffset;

        public SplashScreen()
        {
            InitializeComponent();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            int outerSquare = (int)Math.Ceiling(Math.Sqrt(2) * Math.Max(e.ClipRectangle.Width, e.ClipRectangle.Height));
            using GraphicsPath path = new();
            path.AddEllipse((e.ClipRectangle.Width - outerSquare) / 2, (e.ClipRectangle.Height - outerSquare) / 2, outerSquare, outerSquare);

            using PathGradientBrush brush = new(path);
            brush.CenterColor = Color.FromArgb(70, 121, 172);
            brush.SurroundColors = new[] { Color.FromArgb(30, 81, 132) };

            e.Graphics.FillRectangle(brush, e.ClipRectangle);
        }

        private void NameLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                dragOffset = e.Location;
            }
        }

        private void NameLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button.HasFlag(MouseButtons.Left))
            {
                // The coordinates of e.X and e.Y are relative to the control
                Location = new Point(Location.X + e.X - dragOffset.X, Location.Y + e.Y - dragOffset.Y);
            }
        }
    }
}

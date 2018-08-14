using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls
{
    public class ResponsiveButton : Button
    {

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            if (Image != null)
            {
                var newSize = new Size(ClientRectangle.Height - 6, ClientRectangle.Height - 6).Multiply(factor).Round().Rectify();
                var newImage = new Bitmap(newSize.Width, newSize.Height);
                var oldImage = Image;
                using (var graph = Graphics.FromImage(newImage))
                {
                    graph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graph.SmoothingMode = SmoothingMode.AntiAlias;
                    graph.DrawImage(oldImage, 0, 0, newSize.Width, newSize.Height);
                }
                Image = newImage;
                oldImage.Dispose();
            }

            base.ScaleControl(factor, specified);
        }
    }
}

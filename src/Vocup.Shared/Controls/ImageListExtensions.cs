using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Vocup.Controls
{
    public static class ImageListExtensions
    {
        public static ImageList Scale(this ImageList original, Size size)
        {
            ImageList result = new ImageList
            {
                ColorDepth = ColorDepth.Depth32Bit,
                ImageSize = size
            };

            for (int i = 0; i < original.Images.Count; i++)
            {
                Bitmap bitmap = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawImage(original.Images[i], 0, 0, size.Width, size.Height);
                }
                result.Images.Add(bitmap);
            }

            return result;
        }
    }
}

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
                    // Make sharper edges for integer scaling factors
                    if (original.ImageSize.Width % size.Width == 0 && original.ImageSize.Height % size.Height == 0)
                        g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    else
                        g.InterpolationMode = InterpolationMode.HighQualityBilinear;

                    g.DrawImage(original.Images[i], 0, 0, size.Width, size.Height);
                }
                result.Images.Add(bitmap);
            }

            return result;
        }
    }
}

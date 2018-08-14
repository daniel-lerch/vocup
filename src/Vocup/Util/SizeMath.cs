using System;
using System.Drawing;

namespace Vocup.Util
{
    public static class SizeMath
    {
        public static SizeF Multiply(this SizeF first, SizeF other)
        {
            return new SizeF(first.Width * other.Width, first.Height * other.Height);
        }

        public static SizeF Multiply(this SizeF first, Size other)
        {
            return new SizeF(first.Width * other.Width, first.Height * other.Height);
        }

        public static SizeF Multiply(this Size first, SizeF other)
        {
            return new SizeF(first.Width * other.Width, first.Height * other.Height);
        }

        public static Size Round(this SizeF size)
        {
            return new Size((int)Math.Round(size.Width), (int)Math.Round(size.Height));
        }

        public static Size Rectify(this Size size)
        {
            if (size.Width > size.Height)
                return new Size(size.Height, size.Height);
            else
                return new Size(size.Width, size.Width);
        }
    }
}

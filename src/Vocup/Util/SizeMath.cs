using System;
using System.Drawing;

namespace Vocup.Util
{
    public static class SizeMath
    {
        public static SizeF Multiply(SizeF left, SizeF right)
        {
            return new SizeF(left.Width * right.Width, left.Height * right.Height);
        }

        public static Size MultiplyAndRound(Size left, SizeF right)
        {
            int width = (int)Math.Round(left.Width * right.Width);
            int height = (int)Math.Round(left.Height * right.Height);
            return new Size(width, height);
        }
    }
}

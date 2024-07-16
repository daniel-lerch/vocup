using System;
using System.Drawing;

namespace Vocup.Util
{
    public static class DrawingMath
    {
        public static SizeF Multiply(this SizeF first, SizeF other)
        {
            return new SizeF(first.Width * other.Width, first.Height * other.Height);
        }

        public static SizeF Multiply(this Size first, SizeF other)
        {
            return new SizeF(first.Width * other.Width, first.Height * other.Height);
        }

        public static SizeF Multiply(this Size first, float other)
        {
            return new SizeF(first.Width * other, first.Height * other);
        }

        public static Size Round(this SizeF size)
        {
            return new Size((int)Math.Round(size.Width), (int)Math.Round(size.Height));
        }

        public static SizeF Rectify(this SizeF size)
        {
            if (size.Width > size.Height)
                return new SizeF(size.Height, size.Height);
            else
                return new SizeF(size.Width, size.Width);
        }

        public static Size Rectify(this Size size)
        {
            if (size.Width > size.Height)
                return new Size(size.Height, size.Height);
            else
                return new Size(size.Width, size.Width);
        }


        public static Point Move(this Point point, int x, int y)
        {
            return new Point(point.X + x, point.Y + y);
        }


        public static Rectangle Move(this Rectangle rect, int x, int y)
        {
            return new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);
        }

        public static Rectangle MarginTop(this Rectangle rect, int value)
        {
            int delta = Math.Min(value, rect.Height); // prevent underflow 
            return new Rectangle(rect.X, rect.Y + delta, rect.Width, rect.Height - delta);
        }

        public static Rectangle MarginSide(this Rectangle rect, int value)
        {
            int delta = Math.Min(value, rect.Width / 2); // prevent underflow 
            return new Rectangle(rect.X + delta, rect.Y, rect.Width - 2 * delta, rect.Height);
        }

        public static Rectangle MarginLeft(this Rectangle rect, int value)
        {
            int delta = Math.Min(value, rect.Width); // prevent underflow
            return new Rectangle(rect.X + delta, rect.Y, rect.Width - value, rect.Height);
        }

        public static Rectangle SetHeight(this Rectangle rect, int value)
        {
            return new Rectangle(rect.X, rect.Y, rect.Width, value);
        }
    }
}

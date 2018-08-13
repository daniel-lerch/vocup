using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls
{
    public class ResponsiveMenuStrip : MenuStrip
    {
        private Size _imageScalingBaseSize = new Size(16, 16);
        private SizeF scalingFactor = new SizeF(1F, 1F);

        [DefaultValue(typeof(Size), "16,16")]
        public Size ImageScalingBaseSize
        {
            get => _imageScalingBaseSize;
            set
            {
                _imageScalingBaseSize = value;
                ImageScalingSize = SizeMath.MultiplyAndRound(value, scalingFactor);
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            scalingFactor = SizeMath.Multiply(scalingFactor, factor);
            ImageScalingSize = SizeMath.MultiplyAndRound(_imageScalingBaseSize, scalingFactor);
            ImageScalingSize = SizeMath.Rectify(ImageScalingSize);
            base.ScaleControl(factor, specified);
        }
    }
}

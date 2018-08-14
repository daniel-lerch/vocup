using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls
{
    public class ResponsiveToolStrip : ToolStrip
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
                ImageScalingSize = value.Multiply(scalingFactor).Round().Rectify();
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            scalingFactor = scalingFactor.Multiply(factor);
            ImageScalingSize = _imageScalingBaseSize.Multiply(scalingFactor).Round().Rectify();
            base.ScaleControl(factor, specified);
        }
    }
}

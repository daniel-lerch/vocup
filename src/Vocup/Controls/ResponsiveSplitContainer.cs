using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls
{
    public class ResponsiveSplitContainer : SplitContainer
    {
        private int _splitterBaseDistance = 50;
        private SizeF scalingFactor = new SizeF(1F, 1F);

        [DefaultValue(50)]
        public int SplitterBaseDistance
        {
            get => _splitterBaseDistance;
            set
            {
                _splitterBaseDistance = value;
                SplitterDistance = (int)Math.Round( _splitterBaseDistance * scalingFactor.Width);
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            scalingFactor = scalingFactor.Multiply(factor);
            SplitterDistance = (int)Math.Round(_splitterBaseDistance * scalingFactor.Width);
            base.ScaleControl(factor, specified);
        }
    }
}

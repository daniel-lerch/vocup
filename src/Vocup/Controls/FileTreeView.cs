using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls
{
    public partial class FileTreeView : UserControl
    {
        private Size _imageScalingBaseSize = new Size(16, 16);
        private SizeF scalingFactor = new SizeF(1F, 1F);

        public FileTreeView()
        {
            InitializeComponent();
        }

        [DefaultValue(typeof(Size), "16,16")]
        public Size ImageScalingBaseSize
        {
            get => _imageScalingBaseSize;
            set
            {
                _imageScalingBaseSize = value;
                ScaleImageList();
            }
        }

        protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
        {
            scalingFactor = scalingFactor.Multiply(factor);
            ScaleImageList();
            base.ScaleControl(factor, specified);
        }

        private void ScaleImageList()
        {
            ImageList old = MainTreeView.ImageList;
            MainTreeView.ImageList = IconImageList.Scale(_imageScalingBaseSize.Multiply(scalingFactor).Rectify().Round());
            old?.Dispose();
        }
    }
}

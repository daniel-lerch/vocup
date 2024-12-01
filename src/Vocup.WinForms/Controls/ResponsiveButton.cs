using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Vocup.Util;

namespace Vocup.Controls;

public partial class ResponsiveButton : Button
{
    private Image? _baseImage;
    private Size _imageSize = new(16, 16);
    private SizeF scalingFactor = new(1F, 1F);

    [Localizable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Image? BaseImage
    {
        get => _baseImage;
        set
        {
            _baseImage = value;
            SetScaledImage();
        }
    }

    [DefaultValue(typeof(Size), "16,16")]
    public Size ImageSize
    {
        get => _imageSize;
        set
        {
            _imageSize = value;
            SetScaledImage();
        }
    }

    protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
    {
        scalingFactor = scalingFactor.Multiply(factor);
        SetScaledImage();
        base.ScaleControl(factor, specified);
    }

    private void SetScaledImage()
    {
        if (_baseImage is not null)
        {
            Size newSize = _imageSize.Multiply(scalingFactor).Rectify().Round();
            Image newImage = new Bitmap(newSize.Width, newSize.Height);
            Image? oldImage = Image;
            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.DrawImage(_baseImage, 0, 0, newSize.Width, newSize.Height);
            }
            Image = newImage;
            oldImage?.Dispose();
        }
    }
}

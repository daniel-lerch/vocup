using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vocup.Controls
{
    public class ResponsiveButton : Button
    {

        public new Image Image
        {
            get => base.Image;
            set
            {
                if (value.Height > this.Height + 10)
                {
                    var newHeight = this.Height + 10;
                    var newImage = new Bitmap(newHeight, newHeight);
                    using (var graph = Graphics.FromImage(newImage)) { 
                        graph.DrawImage(value, 0, 0, newHeight, newHeight);
                    }
                    base.Image = newImage;
                    value.Dispose();
                }
                else base.Image = value;
            }
        }

    }
}

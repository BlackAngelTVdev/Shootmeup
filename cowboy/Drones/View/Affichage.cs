using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CowBoy.Helpers;

namespace CowBoy
{
    public partial class Affichage
    {
        public static void Render(BufferedGraphics drawingSpace, string Text)
        {
            Graphics g = drawingSpace.Graphics;

            Rectangle rect = new Rectangle(Sand.WIDTH / 2 - 100, Sand.HEIGHT / 2, 200, 30);

            using (SolidBrush semiTransparentBrush = new SolidBrush(Color.FromArgb(128, Color.Gray)))
            {
                g.FillRectangle(semiTransparentBrush, rect);
            }

            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            g.DrawString(Text, TextHelpers.drawFont, TextHelpers.writingBrush, rect, format);
        }
    }

}


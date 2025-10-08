using CowBoy.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowBoy
{
    public partial class Projectil
    {
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(Texture, new Rectangle(X, Y,100,100));
        }
    }
}

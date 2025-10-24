using CowBoy.Helpers;
using CowBoy.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowBoy
{
    public partial class Obstacle
    {

        private Image obstacle = Resources.kayou;
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(obstacle, new Rectangle(_x, _y, Width, Height));

        }
    }
}

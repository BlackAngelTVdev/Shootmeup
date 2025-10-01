using Drones.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public partial class Obstacle
    {
        
        private Image obstacle = Image.FromFile(@"D:\Poo\P_oo-Shoot-me-up\cowboy\Drones\Resources\obstacle.png");
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(obstacle, new Rectangle(_x,_y, _width, _heith));
            
        }
    }
}

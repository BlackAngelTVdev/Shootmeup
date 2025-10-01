using Drones.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public partial class ennemi
    {
        
        
            //private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);
            private Image kangourou = Image.FromFile(@"D:\Poo\P_oo-Shoot-me-up\cowboy\Drones\Resources\kangaroo.png");

            // De manière graphique
            public void Render(BufferedGraphics drawingSpace)
            {
                drawingSpace.Graphics.DrawImage(kangourou, new Rectangle(x, y, 100, 100));
               
            }


        
    }

}

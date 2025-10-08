using CowBoy.Helpers;
using CowBoy.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CowBoy
{
    public partial class ennemi
    {
        
        
            //private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);
            private Image kangourou = Resources.kangou;

            // De manière graphique
            public void Render(BufferedGraphics drawingSpace)
            {
                drawingSpace.Graphics.DrawImage(kangourou, new Rectangle(_x, _y, 100, 100));

               
            }


        
    }

}

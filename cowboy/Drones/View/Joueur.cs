using Drones.Helpers;
using Drones.Properties;
using System.Resources;
using System.Xml.Linq;

namespace Drones
{
    // Cette partie de la classe Drone définit comment on peut voir un drone

    public partial class Player
    {
        private Pen droneBrush = new Pen(new SolidBrush(Color.Purple), 3);
        private Image joueur = Image.FromFile(@"D:\Poo\P_oo-Shoot-me-up\cowboy\Drones\Resources\cowboy.png");

        // De manière graphique
        public void Render(BufferedGraphics drawingSpace)
        {
            drawingSpace.Graphics.DrawImage(joueur, new Rectangle(X, _y, Direction,100));
            drawingSpace.Graphics.DrawString($"{_vie}PV", TextHelpers.drawFont, TextHelpers.writingBrush, X + _direction /2, Y-10);
        }
        

    }
}

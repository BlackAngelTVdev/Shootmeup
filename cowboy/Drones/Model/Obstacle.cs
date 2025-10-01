using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drones
{
    public partial class Obstacle
    {
        public int _vie = 2;
        public int _x;
        public int _y;
        public int _width = 70;
        public int _heith = 70;

        public int Vie { get => _vie; set => _vie = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        public Obstacle( int x, int y)
        {
            this.Vie = 2;
            this.X = x;
            this.Y = y;
        }
        public static int NbObstcle(List<Obstacle> champ)
        {
            int Nb = 0;
            foreach (var obstacle in champ)
            {
                Nb++;
            }

            return Nb;
        }

    }
}

using System.Collections.Generic;
using System.Drawing; // Pour Rectangle

namespace CowBoy
{
    public partial class Obstacle
    {
        private static int _nextId = 1;

        private int _id;
        private int _vie = 2;
        private int _x;
        private int _y;
        private int _width = 70;
        private int _height = 70;

        public int Id => _id;
        public int Vie { get => _vie; set => _vie = value; }
        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Width { get => _width; set => _width = value; }  
        public int Height { get => _height; set => _height = value; }

        public Obstacle(int x, int y)
        {
            _id = _nextId++;
            _x = x;
            _y = y;
        }

        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, Width, Height);
        }

        public static int NbObstacle(List<Obstacle> champ)
        {
            return champ?.Count ?? 0;
        }
    }
}

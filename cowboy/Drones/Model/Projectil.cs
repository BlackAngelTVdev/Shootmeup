using CowBoy.Helpers;
using System;
using System.Drawing;  // Pour Image

namespace CowBoy
{
    public partial class Projectil
    {
        private static int _nextId = 1;


        private double _vx;
        private double _vy;
        private int _id;
        private double _realX;
        private double _realY;

        private bool _ennemi;

        public int Id { get => _id; private set => _id = value; }

        public int X => (int)_realX;
        public int Y => (int)_realY;

        public Image Texture { get; private set; }
        public float Speed { get; private set; }

        public float TargetX { get; private set; }
        public float TargetY { get; private set; }
        public bool Ennemi { get => _ennemi; set => _ennemi = value; }

        public Projectil(int xInitial, int yInitial, Image texture, float speed, float targetX, float targetY, bool ennemi)
        {
            Id = _nextId++;  // attribution auto d'un ID unique incrémental

            _realX = xInitial;
            _realY = yInitial;
            Texture = texture;
            Speed = speed;
            _ennemi = ennemi;

            double dx = targetX - _realX;
            double dy = targetY - _realY;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance == 0) distance = 1;

            _vx = Speed * dx / distance;
            _vy = Speed * dy / distance;
        }

        public void Update()
        {
            _realX += _vx;
            _realY += _vy;
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, 100, 100); // selon taille sprite projectile
        }

    }

}

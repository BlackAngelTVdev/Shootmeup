using Drones.Helpers;
using System;
using System.Drawing;  // Pour Image

namespace Drones
{
    public partial class Prjectil
    {
        private double realX;
        private double realY;
        private bool ennemi;
        
        public int X { get { return (int)realX; } }
        public int Y { get { return (int)realY; } }

        public Image Texture { get; private set; }
        public float Speed { get; private set; }

        public float TargetX { get; private set; }
        public float TargetY { get; private set; }

        private double vx;
        private double vy;

        public Prjectil(int xInitial, int yInitial, Image texture, float speed, float targetX, float targetY, bool ennemi)
        {
            realX = xInitial;
            realY = yInitial;
            Texture = texture;
            Speed = speed;
            this.ennemi = ennemi;

            double dx = targetX - realX;
            double dy = targetY - realY;
            double distance = Math.Sqrt(dx * dx + dy * dy);
            if (distance == 0) distance = 1;  

            vx = Speed * dx / distance;
            vy = Speed * dy / distance;
        }


        public void Update()
        {
            realX += vx;
            realY += vy;
        }


    }
}

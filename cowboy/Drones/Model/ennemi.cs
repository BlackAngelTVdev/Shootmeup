using System;

namespace Drones
{
    public partial class ennemi
    {
        public int x;
        public int y;
        public int speed = 5;

        public ennemi(int startX, int startY, int speed)
        {
            this.x = startX;
            this.y = startY;
            this.speed = speed;
        }

        // Update prend la cible actuelle en paramètre
        public void Update(int targetX, int targetY)
        {
            int dx = targetX - x;
            int dy = targetY - y;

            if (dx != 0)
                x += Math.Sign(dx) * Math.Min(speed, Math.Abs(dx));
            if (dy != 0)
                y += Math.Sign(dy) * Math.Min(speed, Math.Abs(dy));
        }
    }
}

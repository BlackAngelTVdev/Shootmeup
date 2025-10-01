using Drones.Helpers;
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
        public void Update(int targetX, int targetY, List<Prjectil> pulls)
        {
            int dx = targetX - x;
            int dy = targetY - y;

            if (dx != 0)
                x += Math.Sign(dx) * Math.Min(speed, Math.Abs(dx));
            if (dy != 0)
                y += Math.Sign(dy) * Math.Min(speed, Math.Abs(dy));
            if (RandomHelper.NbrRandom(0,150, false) == 5)
            {
                Image img = Image.FromFile(@"D:\Poo\P_oo-Shoot-me-up\cowboy\Drones\Resources\gantboxe.png");


                pulls.Add(new Prjectil(this.x,this.y,img, 30, targetX, targetY, true));
            }    
        }
        public static int Nbennemi(List<ennemi> military)
        {
            int Nb = 0;
            foreach (var ennemi in military)
            {
                Nb++;
            }

            return Nb;
        }
    }
}

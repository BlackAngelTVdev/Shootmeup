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
                Image img = Image.FromFile(@"D:\Poo\P_oo-Shoot-me-up\cowboy\Drones\Resources\gantboxe1.png");

                // 1. Calcul de l’angle en radians
                double dx1 = targetX - this.x;
                double dy1 = targetY - this.y;
                double angleRad = Math.Atan2(dy1, dx1);

                // 2. Conversion en degrés
                float angleDeg = (float)(angleRad * (180.0 / Math.PI));

                // 3. On crée l’image pivotée
                Image rotated = RotateImage(img, angleDeg);


                pulls.Add(new Prjectil(this.x,this.y, rotated, 20, targetX, targetY, true));
            }    
        }

        public Image RotateImage(Image img, float angle)
        {
            Bitmap rotatedBmp = new Bitmap(img.Width, img.Height); //nouveau bitmap(information graphique) avec la taille de l'image
            rotatedBmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);


            using (Graphics g = Graphics.FromImage(rotatedBmp)) //permet de dessinner dans la nouvelle image (bitmap)
            {
                g.TranslateTransform((float)img.Width / 2, (float)img.Height / 2); // centre
                g.RotateTransform(angle); //rotation de l'angle
                g.TranslateTransform(-(float)img.Width / 2, -(float)img.Height / 2); //remet en haud a gauche
                g.DrawImage(img, new Point(0, 0)); //
            }

            return rotatedBmp; //envoie du nouveau bitmap avec l'iimage pivoter
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

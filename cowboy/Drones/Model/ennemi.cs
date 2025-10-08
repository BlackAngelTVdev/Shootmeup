using CowBoy.Helpers;
using CowBoy.Properties;
using System;

namespace CowBoy
{
    public partial class ennemi
    {
        private int _x;
        private int _y;
        private int _speed = 5;
        private int _vie = 3;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public int Vie { get => _vie; set => _vie = value; }

        public ennemi(int startX, int startY, int speed)
        {
            this._x = startX;
            this._y = startY;
            this._speed = speed;
        }

        // Update prend la cible actuelle en paramètre
        public void Update(int targetX, int targetY, List<Prjectil> pulls)
        {
            int dx = targetX - _x;
            int dy = targetY - _y;

            if (dx != 0)
                _x += Math.Sign(dx) * Math.Min(_speed, Math.Abs(dx));
            if (dy != 0)
                _y += Math.Sign(dy) * Math.Min(_speed, Math.Abs(dy));
            if (RandomHelper.NbrRandom(0,200, false) == 5)
            {
                Image img = Resources.gant;

                // 1. Calcul de l’angle en radians
                double dx1 = targetX - this._x;
                double dy1 = targetY - this._y;
                double angleRad = Math.Atan2(dy1, dx1);

                // 2. Conversion en degrés
                float angleDeg = (float)(angleRad * (180.0 / Math.PI));

                // 3. On crée l’image pivotée
                Image rotatead = RotateImage(img, angleDeg);


                pulls.Add(new Prjectil(this._x,this._y + 75, rotatead, 20, targetX, targetY, true));
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
        public Rectangle GetRectangle()
        {
            return new Rectangle(_x, _y, 70, 70); // Ajuste aussi selon la taille visible
        }


    }
}

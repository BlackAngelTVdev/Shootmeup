using CowBoy.Helpers;
using CowBoy.Properties;
using System;

namespace CowBoy
{
    public partial class Ennemis
    {
        private int _x;
        private int _y;
        private int _speed = 5;
        private int _vie = 3;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int Speed { get => _speed; set => _speed = value; }
        public int Vie { get => _vie; set => _vie = value; }

        public enum EnemyState
        {
            ChasingPlayer,
            GoingToRandomPoint
        }
        private EnemyState _state = EnemyState.ChasingPlayer;
        private Point? _temporaryTarget = null;

        public EnemyState State { get => _state; set => _state = value; }
        public Point? TemporaryTarget { get => _temporaryTarget; set => _temporaryTarget = value; }

        public Ennemis(int startX, int startY, int speed)
        {
            this._x = startX;
            this._y = startY;
            this._speed = speed;
        }

        // Update prend la cible actuelle en paramètre
        public void Update(int playerX, int playerY, List<Projectil> pulls)
        {
            int targetX, targetY;

            // Choix de la cible selon l'état
            if (_state == EnemyState.GoingToRandomPoint && _temporaryTarget.HasValue)
            {
                targetX = _temporaryTarget.Value.X;
                targetY = _temporaryTarget.Value.Y;

                // Si on atteint le point aléatoire, on revient à la poursuite du joueur
                if (Math.Abs(_x - targetX) < 5 && Math.Abs(_y - targetY) < 5)
                {
                    _state = EnemyState.ChasingPlayer;
                    _temporaryTarget = null;
                    targetX = playerX;
                    targetY = playerY;
                }
            }
            else
            {
                targetX = playerX;
                targetY = playerY;
            }

            // Déplacement simple
            int dx = targetX - _x;
            int dy = targetY - _y;

            if (dx != 0)
                _x += Math.Sign(dx) * Math.Min(_speed, Math.Abs(dx));
            if (dy != 0)
                _y += Math.Sign(dy) * Math.Min(_speed, Math.Abs(dy));

            // Tir (uniquement quand il poursuit le joueur)
            if (_state == EnemyState.ChasingPlayer && RandomHelper.NbrRandom(0, 200, false) == 5)
            {
                Image img = Resources.gant;

                double dx1 = targetX - this._x;
                double dy1 = targetY - this._y;
                double angleRad = Math.Atan2(dy1, dx1);
                float angleDeg = (float)(angleRad * (180.0 / Math.PI));

                Image rotated = RotateImage(img, angleDeg);
                pulls.Add(new Projectil(this._x, this._y + 75, rotated, 20, targetX, targetY, true));
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


        public static int Nbennemi(List<Ennemis> military)
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

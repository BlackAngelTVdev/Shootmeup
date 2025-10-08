using CowBoy.Properties;
using System.Drawing;

namespace CowBoy
{
    // Cette partie de la classe Drone définit ce qu'est un drone par un modèle numérique
    public partial class Player
    {
        
        private int _vie = 50;                            // La charge actuelle de la batterie                        // Un nom
        private int _x = Sand.WIDTH / 2;                                 // Position en X depuis la gauche de l'espace aérien
        private int _y = Sand.HEIGHT - 200;
        private int _direction = 100;
        private int _cooldownPV = 0;
        private DateTime _lastTireCall = DateTime.MinValue;

        // Constructeur
        
        public int X { get { return _x; } private set { _x = value; } }
        public int Y { get { return _y; } }
        public int Direction { get => _direction; set => _direction = value; }
        public int Vie { get => _vie; set => _vie = value; }

        public Player()
        {
            _x = Sand.WIDTH / 2;
        }
        public void gauche()
        {
            if (_x > 90)
            {
                _x -= 15;

                if (_direction == 100)
                {
                    _x += 100;
                    _direction = -100;
                }
            }
        }
        public void droit()
        {
            if (_x < Sand.WIDTH - 100)
            {
                _x += 15;
                if (_direction == -100)
                {
                    _x -= 100;
                    _direction = 100;
                }
            }
        }
        public void addvie()
        {

            _cooldownPV++;
            if (_cooldownPV % 20 == 0)
            {
                _cooldownPV = 0;
                if (_vie <= 99)
                {
                    _vie++;
                }
            }
        }
        public void tire(List<Prjectil> pulls, Point targetPosition)
        {
            DateTime now = DateTime.Now;

            if ((now - _lastTireCall).TotalSeconds >= 0.33)
            {
                

                
                pulls.Add(new Prjectil(this.X, this.Y, Resources.cactus,50, targetPosition.X, targetPosition.Y, false));

                _lastTireCall = now;
            }
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle(X, Y, 50, 50); // Ajuste selon la taille visible du joueur
        }



    }
}

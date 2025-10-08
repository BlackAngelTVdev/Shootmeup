using CowBoy;
using CowBoy.Helpers;

namespace CowBoy
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class Sand : Form
    {
        public static readonly int WIDTH = 1920;        // Dimensions of the airspace
        public static readonly int HEIGHT = 1080;
        private Image _backgroundImage = Image.FromFile(@"C:\Users\pb17shq\Documents\Shootmeup\cowboy\Drones\Resources\background1.png");
        private Player _player;
        
        private Point _mousePosition;





        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien

        private List<Obstacle> _fields;
        private List<Prjectil> _pulls;
        private List<ennemi> _military;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;
        




        // Initialisation de l'espace aérien avec un certain nombre de drones
        public Sand(List<Obstacle> fields, List<Prjectil> pulls, List<ennemi> military)
        {
            InitializeComponent();
            this.MouseMove += AirSpace_MouseMove;  // Changer le nom pour éviter conflit
            this.MouseClick += AirSpace_MouseClick;

            this.KeyPreview = true;
            this.KeyDown += AirSpace_KeyDown;

            currentContext = BufferedGraphicsManager.Current;
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this._fields = fields;
            this._pulls = pulls;
            
            this._military = military;
            _player = new Player();
        }
        private void AirSpace_MouseMove(object sender, MouseEventArgs e)
        {
            _mousePosition = e.Location;
        }
        private void AirSpace_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                _player.tire(_pulls, _mousePosition);
            }
        }

        private void AirSpace_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    _player.gauche();
                    break;
                case Keys.D:
                    _player.droit();
                    break;
            }
        }


        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.DrawImage(_backgroundImage, new Rectangle(0, 0, Width, Height));
            _player.Render(airspace);


            foreach (Obstacle obstacle in _fields)
            {
                obstacle.Render(airspace);
            }
            foreach (Prjectil prjectil in _pulls)
            {
                prjectil.Render(airspace);
            }
            foreach (ennemi ennemi in _military)
            {
                ennemi.Render(airspace);
            }

            airspace.Render();

        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            _player.addvie();

            if (Obstacle.NbObstacle(_fields) < 10)
            {
                _fields.Add(new Obstacle(RandomHelper.NbrRandom(0, WIDTH - 70, false), RandomHelper.NbrRandom(300, 800, true)));
            }
            if (ennemi.Nbennemi(_military) < 10)
            {
                _military.Add(new ennemi(RandomHelper.NbrRandom(0, WIDTH, false), 20, RandomHelper.NbrRandom(1, 2, true)));
            }

            // Mise à jour des projectiles
            foreach (Prjectil projectil in _pulls)
            {
                projectil.Update();
            }

            // Mise à jour des ennemis
            foreach (ennemi ennemi in _military)
            {
                ennemi.Update(_player.X, _player.Y, _pulls);
            }

            List<Prjectil> projectilesToRemove = new List<Prjectil>();
            List<Obstacle> obstaclesToRemove = new List<Obstacle>();
            List<ennemi> ennemisToRemove = new List<ennemi>();

            foreach (var projectile in _pulls)
            {
                

                Rectangle projRect = projectile.GetRectangle();

                bool handled = false;

                
                foreach (var obstacle in _fields)
                {
                    if (projRect.IntersectsWith(obstacle.GetRectangle()))
                    {
                        obstacle.Vie--;
                        projectilesToRemove.Add(projectile);

                        if (obstacle.Vie <= 0)
                            obstaclesToRemove.Add(obstacle);

                        handled = true;
                        break;
                    }
                }
                if (handled) continue;

                if (!projectile.Ennemi)
                {
                    
                    foreach (var enemy in _military)
                    {
                        if (projRect.IntersectsWith(enemy.GetRectangle()))
                        {
                            enemy.Vie--;
                            projectilesToRemove.Add(projectile);

                            if (enemy.Vie <= 0)
                                ennemisToRemove.Add(enemy);

                            handled = true;
                            break;
                        }
                    }
                }
                else
                {
                    
                    Rectangle playerRect = _player.GetRectangle();  
                    if (projRect.IntersectsWith(playerRect))
                    {
                        _player.Vie-=10;
                        projectilesToRemove.Add(projectile);
                        handled = true;
                    }
                }
            }
            
            // Suppression après boucle
            foreach (var p in projectilesToRemove)
                _pulls.Remove(p);

            foreach (var o in obstaclesToRemove)
                _fields.Remove(o);

            foreach (var e in ennemisToRemove)
                _military.Remove(e);




        }


        // Méthode appelée à chaque frame
        private void NewFrame(object sender, EventArgs e)
        {
            this.Update(ticker.Interval);
            this.Render();
        }

        private void AirSpace_Load(object sender, EventArgs e)
        {

        }
    }
}
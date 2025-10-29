using CowBoy;
using CowBoy;
using CowBoy.Helpers;
using CowBoy.Properties;
using static System.Formats.Asn1.AsnWriter;

namespace CowBoy
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class Sand : Form
    {


        public static readonly int WIDTH = 1920;        // Dimensions of the airspace
        public static readonly int HEIGHT = 1080;
        private Image _backgroundImage = Resources.bk;
        private Player _player;

        private Point _mousePosition;
        private bool run = true;





        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien

        private List<Obstacle> _fields;
        private List<Projectil> _pulls;
        private List<Ennemis> _military;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;
        private int playerShotsCount = 0;





        // Initialisation de l'espace aérien avec un certain nombre de drones
        public Sand(List<Obstacle> fields, List<Projectil> pulls, List<Ennemis> military)
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
            Score s = new Score();

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
                playerShotsCount++;
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
            if (run)
            {


                Console.WriteLine(playerShotsCount);
                Score s = new Score();
                Affichage affichage = new Affichage();
                airspace.Graphics.DrawImage(_backgroundImage, new Rectangle(0, 0, Width, Height));
                _player.Render(airspace);


                foreach (Obstacle obstacle in _fields)
                {
                    obstacle.Render(airspace);
                }
                foreach (Projectil prjectil in _pulls)
                {
                    prjectil.Render(airspace);
                }
                foreach (Ennemis ennemi in _military)
                {
                    ennemi.Render(airspace);
                }
                s.Render(airspace);
                if (_player.Vie <= 0)
                {
                    Affichage.Render(airspace, "GAME OVER !");
                    run = false;
                }
                if (s.score >= 100)
                {
                    Affichage.Render(airspace, "Tu as Gagné !");
                    run = false;
                }
                airspace.Render();
            }

        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {




            _player.addvie();

            if (Obstacle.NbObstacle(_fields) < 10)
            {
                _fields.Add(new Obstacle(RandomHelper.NbrRandom(0, WIDTH - 70, false), RandomHelper.NbrRandom(300, 800, true)));
            }
            if (Ennemis.Nbennemi(_military) < 10)
            {
                _military.Add(new Ennemis(RandomHelper.NbrRandom(0, WIDTH, false), 20, RandomHelper.NbrRandom(1, 2, true)));
            }

            // Mise à jour des projectiles
            foreach (Projectil projectil in _pulls)
            {
                projectil.Update();
            }

            // Mise à jour des ennemis
            foreach (Ennemis ennemi in _military)
            {
                ennemi.Update(_player.X, _player.Y, _pulls);
            }

            List<Projectil> projectilesToRemove = new List<Projectil>();
            List<Obstacle> obstaclesToRemove = new List<Obstacle>();
            List<Ennemis> ennemisToRemove = new List<Ennemis>();

            // Vérification des collisions physiques entre ennemis et joueur
            Rectangle playerRect = _player.GetRectangle();

            foreach (var enemy in _military)
            {
                if (enemy.GetRectangle().IntersectsWith(playerRect))
                {
                    _player.Vie -= 10;        // Le joueur perd 15 PV
                    ennemisToRemove.Add(enemy);  // L'ennemi disparaît
                }
            }


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
                            // Tous les 10 tirs : dégâts réduits à 3
                            if (playerShotsCount % 10 == 0)
                            {
                                enemy.Vie -= 3;
                            }
                            else
                            {
                                enemy.Vie--;
                            }
                            projectilesToRemove.Add(projectile);

                            if (enemy.Vie <= 0)
                            {
                                Score.add();
                                ennemisToRemove.Add(enemy);
                            }
                            handled = true;
                            break;
                        }
                    }
                }
                else
                {


                    if (projRect.IntersectsWith(playerRect))
                    {
                        _player.Vie -= 5;
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
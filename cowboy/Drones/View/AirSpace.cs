using Drones;
using Drones.Helpers;

namespace Drones
{
    // La classe AirSpace représente le territoire au dessus duquel les drones peuvent voler
    // Il s'agit d'un formulaire (une fenêtre) qui montre une vue 2D depuis en dessus
    // Il n'y a donc pas de notion d'altitude qui intervient

    public partial class AirSpace : Form
    {
        public static readonly int WIDTH = 1920;        // Dimensions of the airspace
        public static readonly int HEIGHT = 1080;
        private Image backgroundImage = Image.FromFile(@"D:\Poo\P_oo-Shoot-me-up\cowboy\Drones\Resources\background1.png");
        private Player player;
        private Point mousePosition;





        // La flotte est l'ensemble des drones qui évoluent dans notre espace aérien

        private List<Obstacle> fields;
        private List<Prjectil> pulls;
        private List<ennemi> military;

        BufferedGraphicsContext currentContext;
        BufferedGraphics airspace;





        // Initialisation de l'espace aérien avec un certain nombre de drones
        public AirSpace(List<Obstacle> fields, List<Prjectil> pulls, List<ennemi> military)
        {
            InitializeComponent();
            this.MouseMove += AirSpace_MouseMove;  // Changer le nom pour éviter conflit

            this.KeyPreview = true;
            this.KeyDown += AirSpace_KeyDown;

            currentContext = BufferedGraphicsManager.Current;
            airspace = currentContext.Allocate(this.CreateGraphics(), this.DisplayRectangle);
            this.fields = fields;
            this.pulls = pulls;
            player = new Player();
            this.military = military;
        }
        private void AirSpace_MouseMove(object sender, MouseEventArgs e)
        {
            mousePosition = e.Location;
        }

        private void AirSpace_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:

                    player.gauche();
                    break;

                case Keys.D:
                    
                    player.droit();
                    break;
                case Keys.Space:
                    player.tire(pulls, mousePosition);
                    
                    break;
            }
        }


        // Affichage de la situation actuelle
        private void Render()
        {
            airspace.Graphics.DrawImage(backgroundImage, new Rectangle(0, 0, Width, Height));
            player.Render(airspace);
            
           
            foreach (Obstacle obstacle in fields)
            {
                obstacle.Render(airspace);
            }
            foreach (Prjectil prjectil in pulls)
            {
                prjectil.Render(airspace);
            }
            foreach (ennemi ennemi in military)
            {
                ennemi.Render(airspace);
            }

            airspace.Render();

        }

        // Calcul du nouvel état après que 'interval' millisecondes se sont écoulées
        private void Update(int interval)
        {
            player.addvie();
            if (Obstacle.NbObstcle(fields) < 15)
            {
                fields.Add(new Obstacle(RandomHelper.NbrRandom(0, WIDTH, false), RandomHelper.NbrRandom(300, 800, true)));
                military.Add(new ennemi(RandomHelper.NbrRandom(0, WIDTH, false), 20, RandomHelper.NbrRandom(1,2, true)));
            }
            foreach (Prjectil prjectil in pulls)
            {
                prjectil.Update();
            }
            foreach (ennemi ennemi in military)
            {
                ennemi.Update(player.X,player.Y);
            }
            

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
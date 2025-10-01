using Drones;

namespace Drones
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Création de la flotte de drones
            
            List<Obstacle> fields = new List<Obstacle>();
            List<Prjectil> pulls = new List<Prjectil> ();
            List<ennemi> military = new List<ennemi> ();



            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new AirSpace(fields, pulls ,military)); // passe ta liste de drones ici
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue : " + ex.Message + "\n\n" + ex.StackTrace);
            }
           
        }
    }
}
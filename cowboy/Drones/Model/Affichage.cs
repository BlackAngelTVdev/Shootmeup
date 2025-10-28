namespace CowBoy
{
    public partial class Affichage
    {
        private string _text = "";  // champ privé d’instance

        public string Text
        {
            get => _text;
            set => _text = value;
        }
    }
}

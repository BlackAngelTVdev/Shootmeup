using System.Drawing;
using System.Windows.Forms;
using CowBoy.Helpers;

namespace CowBoy
{
    public partial class Affichage
    {
        public static void Render(BufferedGraphics drawingSpace, string Text)
        {
            Graphics g = drawingSpace.Graphics;

            // Définition d'une zone centrale un peu plus grande et centrée
            Rectangle rect = new Rectangle(Sand.WIDTH / 2 - 150, Sand.HEIGHT / 2 - 30, 300, 60);

            // Rendu du Fond : Noir très opaque pour mettre en évidence le message
            using (SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(220, 0, 0, 0)))
            {
                g.FillRectangle(backgroundBrush, rect);
            }

            // Ajout d'une bordure visible (couleur Or/Jaune pour le thème Western)
            using (Pen borderPen = new Pen(Color.Yellow, 3))
            {
                g.DrawRectangle(borderPen, rect);
            }

            // Format de chaîne pour un centrage parfait
            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Un pinceau de couleur différente pour le texte (Blanc ou Jaune Vif)
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            {
                // Utiliser une police légèrement plus grande pour l'affichage central si possible
                // Sinon, utiliser TextHelpers.drawFont
                g.DrawString(Text, TextHelpers.drawFont, textBrush, rect, format);
            }
        }
    }
}
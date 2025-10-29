using System.Drawing;
using System.Windows.Forms;
using CowBoy.Helpers;

namespace CowBoy
{
    public partial class Score
    {
        public void Render(BufferedGraphics drawingSpace)
        {
            Graphics g = drawingSpace.Graphics;

            Rectangle rect = new Rectangle(Sand.WIDTH - 160, 10, 150, 30);

            using (SolidBrush backgroundBrush = new SolidBrush(Color.FromArgb(180, 50, 50, 50)))
            {
                g.FillRectangle(backgroundBrush, rect);
            }

            using (Pen borderPen = new Pen(Color.Gold, 2))
            {
                g.DrawRectangle(borderPen, rect);
            }

            StringFormat format = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Assurez-vous que la variable 'score' est accessible dans ce contexte.
            string scoreText = $"SCORE: {score}";

            g.DrawString(scoreText, TextHelpers.drawFont, TextHelpers.writingBrush, rect, format);
        }
    }
}
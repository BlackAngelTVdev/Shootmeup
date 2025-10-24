using CowBoy.Helpers;


namespace CowBoy
{
    public partial class Score
    {
        public void Render(BufferedGraphics drawingSpace)
        {
            Graphics g = drawingSpace.Graphics;


            Rectangle rect = new Rectangle(Sand.WIDTH - 160, 0, 150, 30);


            using (SolidBrush semiTransparentBrush = new SolidBrush(Color.FromArgb(128, Color.Gray)))
            {

                g.FillRectangle(semiTransparentBrush, rect);
            }


            g.DrawString($"{score}", TextHelpers.drawFont, TextHelpers.writingBrush, rect.Left + 10, rect.Top + 5);
        }




    }
}


using System.Drawing;
using FormsGamePlayer;
using System.Windows.Forms;

namespace WForm_Game_Test
{
    public class Scene1 : Scene
    {
        int i;
        readonly Point center;

        Point clickedPoint;
        Color keyboardState;

        public Scene1(GamePlayerControl controller) : base(controller)
        {
            i = 0;
            center = new Point(GameData.PictureBoxWidth / 2, GameData.PictureBoxHeight / 2);

            controller.MouseClick += (o, e) =>
            {
                clickedPoint = new Point(e.X, e.Y);
            };

            controller.KeyDown += (o, e) =>
            {
                switch (e.KeyData)
                {
                    case Keys.Up:
                        keyboardState = Color.Red;
                        break;
                    case Keys.Down:
                        keyboardState = Color.Blue;
                        break;
                    case Keys.Left:
                        keyboardState = Color.Green;
                        break;
                    case Keys.Right:
                        keyboardState = Color.Yellow;
                        break;
                    default:
                        break;
                }
            };
        }

        public override void Draw(Graphics g)
        {
            g.DrawString(i.ToString(), SystemFonts.DefaultFont, Brushes.Black, center);
            g.DrawString("CLICK!", SystemFonts.DefaultFont, Brushes.Brown, clickedPoint);
            g.FillRectangle(new SolidBrush(keyboardState), 0, 0, 100, 100);
        }

        public override void Tick()
        {
            base.Tick();
            i++;
        }
    }
}

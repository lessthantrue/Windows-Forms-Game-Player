using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms.GamePlayer;
using CustomInput;

namespace WForm_Game_Test
{
    public class Scene1 : Scene
    {
        int i;
        readonly Point center;

        Point clicked;

        Color keyboard;

        public Scene1(GamePlayerControl controller) : base(controller)
        {
            i = 0;
            center = new Point(base.GameData.PictureBoxWidth / 2, base.GameData.PictureBoxHeight / 2);

            base.MouseControl.AddPulseEvent(System.Windows.Forms.MouseButtons.Left, (o, a) => clicked = new Point(MouseControl.X, MouseControl.Y));

            KeyboardControl.AddPulseEvent(System.Windows.Forms.Keys.Up, (o, a) => keyboard = Color.Blue);
            KeyboardControl.AddPulseEvent(System.Windows.Forms.Keys.Down, (o, a) => keyboard = Color.Red);
            KeyboardControl.AddPulseEvent(System.Windows.Forms.Keys.Left, (o, a) => keyboard = Color.Green);
            KeyboardControl.AddPulseEvent(System.Windows.Forms.Keys.Right, (o, a) => keyboard = Color.Yellow);
        }

        public override void Draw(Graphics g)
        {
            g.DrawString(i.ToString(), SystemFonts.DefaultFont, Brushes.Black, center);
            g.DrawString("CLICK!", SystemFonts.DefaultFont, Brushes.Brown, clicked);
            g.FillRectangle(new SolidBrush(keyboard), 0, 0, 100, 100);
        }

        public override void Tick()
        {
            base.Tick();
            i++;
        }
    }
}

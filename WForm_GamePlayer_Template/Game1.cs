using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WForm_GamePlayer;
using System.Drawing;
using System.Windows.Forms.GamePlayer;
using CustomInput;

namespace $safeprojectname$
{
    class Game1 : Game
    {
        public Game1(GamePlayerControl controller) : base(controller)
        {
            
        }

        public override void Draw(Graphics g)
        {
          throw new NotImplementedException();
        }

        public override void Tick()
        {
            base.Tick();
            throw new NotImplementedException();
        }
    }
}

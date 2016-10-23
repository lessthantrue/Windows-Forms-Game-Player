using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.GamePlayer;

namespace $safeprojectname$
{
    public static class Scenes
    {
        public static void initScenes(GamePlayerControl control)
        {
            scene1 = new Scene1(control);
        }

        Scene1 scene1;
    }
}

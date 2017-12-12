using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.GamePlayer;

namespace WForm_Game_Test
{
    public static class Scenes
    {
        public static void initScenes(GamePlayerControl control)
        {
            scene1 = new Scene1(control);
        }

        public static Scene1 scene1;
    }
}

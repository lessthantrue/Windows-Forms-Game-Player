using System.Drawing;
using CustomInput;

namespace System.Windows.Forms.GamePlayer
{
    /// <summary>
    /// All required functions for a Windows Form Game Player to run your game
    /// </summary>
    public abstract class Game
    {
        protected Keyboard KeyboardControl;
        protected Mouse MouseControl;

        protected GameData GameData;

        protected Game(GamePlayerControl controller)
        {
            GameData.PictureBoxHeight = controller.Height;
            GameData.PictureBoxWidth = controller.Width;
            GameData.TicksPerSecond = controller.TicksPerSecond;
            GameData.Dt = 1.0/controller.TicksPerSecond;

            KeyboardControl = new Keyboard(controller);
            MouseControl = new Mouse(controller);
        }

        /// <summary>
        /// Draws the current playable
        /// </summary>
        /// <param name="g">The graphics object for the PictureBox</param>
        public abstract void Draw(Graphics g);

        /// <summary>
        /// Called whenever the timer ticks. Calls <see cref="KeyboardControl.Tick()"/> and <see cref="MouseControl.Tick()"/>
        /// </summary>
        public virtual void Tick()
        {
            KeyboardControl.Tick();
            MouseControl.Tick();
            GameData.TickCount++;
        }
    }
}
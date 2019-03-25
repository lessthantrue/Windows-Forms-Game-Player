using System;
using System.Timers;
using System.Windows.Forms;

namespace FormsGamePlayer
{
    public class GamePlayerControl : PictureBox
    {
        private readonly object m_locker = new object();

        private System.Timers.Timer Tick;
        private DateTime framerateMeasurement;

        /// <summary>
        /// How many ticks per second the game will run
        /// </summary>
        public int TicksPerSecond;
        /// <summary>
        /// The current game being played.
        /// </summary>
        public Scene CurrentGame { get; set; }

        /// <summary>
        /// Instantiates the Timer and makes the control selectable
        /// </summary>
        public GamePlayerControl() : base()
        {
            SetStyle(ControlStyles.Selectable, true);
        }

        /// <summary>
        /// Initializes the GamePlayerControl with a specific game and a number of ticks per second
        /// </summary>
        /// <param name="tps">Ticks per second</param>
        /// <param name="game">The game to be played</param>
        public void Init(int tps, Scene game)
        {
            Console.WriteLine("Initializing: " + CurrentGame);
            CurrentGame = game;
            Paint += (sender, e) => { CurrentGame?.Draw(e.Graphics); };
            Tick = new System.Timers.Timer();
            TicksPerSecond = tps;
            Tick.Elapsed += TickHandler;
            framerateMeasurement = DateTime.Now;
            this.Focus();
        }

        private void TickHandler(object sender, ElapsedEventArgs e)
        {
            lock (m_locker)
            {
                CurrentGame?.setActualDt((DateTime.Now - framerateMeasurement).TotalSeconds);
                framerateMeasurement = DateTime.Now;
                CurrentGame?.Tick();
                Draw();

                if (CurrentGame?.nextScene != null)
                    CurrentGame = CurrentGame.nextScene;
            }
        }

        private void Draw()
        {
            if (InvokeRequired)
            {
                Action act = Refresh;
                Invoke(act);
            }
            else
            {
                Refresh();
            }
        }

        /// <summary>
        /// Starts the Timer, playing the game.
        /// </summary>
        public void Play()
        {
            Tick.Enabled = true;
            Tick.Start();
        }

        /// <summary>
        /// Stops the timer, pausing the game.
        /// </summary>
        public void Pause()
        {
            Tick.Stop();
        }

        /// <summary>
        /// For compatibility with input
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// For compatibility with input
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool IsInputKey(Keys keyData)
        {
            return true;
        }
    }
}

using System;
using System.Timers;

namespace System.Windows.Forms.GamePlayer
{
    public class GamePlayerControl : PictureBox
    {
        private readonly object m_locker = new object();

        private Timers.Timer Tick;
        private DateTime framerateMeasurement;

        /// <summary>
        /// How many ticks per second the game will run
        /// </summary>
        public int TicksPerSecond
        {
            get { return (int)(1.0 / (Tick.Interval / 1000.0)); }
            set { Tick.Interval = 1000 * (1.0 / value); }
        }

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
            Tick = new Timers.Timer();
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
            Tick = new Timers.Timer();
            TicksPerSecond = tps;
            Tick.Elapsed += tHandler;
            framerateMeasurement = DateTime.Now;
            this.Focus();
        }

        private void tHandler(object sender, ElapsedEventArgs e)
        {
            lock (m_locker)
            {
                //Console.WriteLine("Ticking: " + game);
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
                //Console.WriteLine("Invoking Refresh: " + game);
                Action act = Refresh;
                Invoke(act);
            }
            else
            {
                //Console.WriteLine("Refreshing: " + game);
                Refresh();
            }
        }

        /// <summary>
        /// Starts the Timer, playing the game.
        /// </summary>
        public void Play()
        {
            //Console.WriteLine("Playing: " + CurrentGame);
            Tick.Enabled = true;
            Tick.Start();
        }

        /// <summary>
        /// Stops the timer, pausing the game.
        /// </summary>
        public void Pause()
        {
            //Console.WriteLine("Pausing: " + CurrentGame);
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

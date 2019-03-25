namespace FormsGamePlayer
{
    /// <summary>
    /// Contains various information about the game
    /// </summary>
    public struct GameData
    {
        /// <summary>
        /// Total count the game has been ticking for
        /// </summary>
        public long TickCount { get; internal set; }

        /// <summary>
        /// height of the drawable window in pixels
        /// </summary>
        public int PictureBoxHeight { get; internal set; }

        /// <summary>
        /// width of the drawable window in pixels
        /// </summary>
        public int PictureBoxWidth { get; internal set; }

        /// <summary>
        /// ticks per second as set by the GameControl
        /// </summary>
        public int TicksPerSecond { get; internal set; }

        /// <summary>
        /// the expected delta time according to TicksPerSecond
        /// </summary>
        public double ExpectedDt { get; internal set; }

        /// <summary>
        /// measured delta time
        /// </summary>
        public double actualDt { get; internal set; }
    }
}

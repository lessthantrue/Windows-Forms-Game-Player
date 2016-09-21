namespace System.Windows.Forms.GamePlayer
{
    /// <summary>
    /// Contains various information about the game
    /// </summary>
    public struct GameData
    {
        public long TickCount { get; internal set; }

        public int PictureBoxHeight { get; internal set; }
        public int PictureBoxWidth { get; internal set; }

        public int TicksPerSecond { get; internal set; }

        public double Dt { get; internal set; }
    }
}

namespace SCUMServerListener
{
    public sealed class AppSettings
    {
        public static readonly AppSettings Instance = Configuration.Load();
        public string DefaultServerId { get; set; } = "16315624";
        public int PositionX { get; set; } = 15;
        public int PositionY { get; set; } = 15;
        public bool DisableBackground { get; set; } = true;
        public bool OverlayAllWindows { get; set; } = false;
        public bool ShowName { get; set; } = true;
        public bool ShowPlayers { get; set; } = true; 
        public bool ShowTime { get; set; } = true;
        public bool ShowPing { get; set; } = true;
        public string OnlineColor { get; set; } = "Green";
        public string OfflineColor { get; set; } = "Red";
        public string BackgroundColor { get; set; } = "Dark";
    }
}

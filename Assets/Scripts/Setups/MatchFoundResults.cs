
    using System.Collections.Generic;

    public class MatchFoundResults
    {
        public static MatchFoundResults Instance { get; private set; } = new();
    
        public string RoomId { get; set; }
        public List<Player> Players { get; set; }
    }

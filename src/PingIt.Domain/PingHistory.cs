using System;

namespace PingIt.Domain
{
    public class PingHistory
    {
        public DateTime PingedAt { get; set; }
        public PingStatus Status { get; set; }
        public long Duration { get; set; }
    }
}
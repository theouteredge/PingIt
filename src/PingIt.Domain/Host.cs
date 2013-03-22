using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Timers;

namespace PingIt.Domain
{
    public class Host
    {
        public string Uri { get; set; }
        public int Port { get; set; }

        public string Name { get; set; }
        public PingStatus Status { get; set; }
        
        public int PingEvery { get; set; }
        public List<PingHistory> PingHistory { get; set; }



        private readonly Ping _ping = new Ping();
        private readonly Timer _timer = new Timer();



        public Host()
        {
            PingHistory = new List<PingHistory>();
        }


        public Host(string uri, int port, string name, int pingEvery)
        {
            Uri = uri;
            Port = port;
            Name = name;
            PingEvery = pingEvery;

            PingHistory = new List<PingHistory>();
        }


        public void Start()
        {
            _timer.Elapsed += (sender, args) => Ping();
            _timer.Interval = PingEvery == 0 ? 30000 : PingEvery;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }


        public bool Ping()
        {
            try
            {
                var reply = _ping.Send(Uri, Port);
                if (reply != null)
                {
                    if (reply.Status == IPStatus.Success)
                    {
                        Status = PingStatus.Up;
                        PingHistory.Add(new PingHistory { PingedAt = DateTime.Now, Duration = reply.RoundtripTime, Status = PingStatus.Up });

                        return true;
                    }
                }
                Status = PingStatus.Down;
            }
            catch (Exception)
            {
                Status = PingStatus.Down;
            }

            PingHistory.Add(new PingHistory { PingedAt = DateTime.Now, Duration = 0, Status = PingStatus.Down });
            return false;
        }


        public int? AverageResponseTime()
        {
            var history = PingHistory.Where(x => x.Status == PingStatus.Up).ToList();
            if (history.Count > 0)
                return Convert.ToInt32(history.Average(x => x.Duration));

            return null;
        }

        public int? AverageResponseTimeRecent()
        {
            var history = PingHistory.Where(x => x.PingedAt >= DateTime.Now.AddMinutes(-10) && x.Status == PingStatus.Up).ToList();
            if (history.Count > 0)
                return Convert.ToInt32(history.Average(x => x.Duration));

            return null;
        }
    }
}

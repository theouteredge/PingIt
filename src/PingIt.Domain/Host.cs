using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization;
using System.Text;
using System.Timers;

namespace PingIt.Domain
{
    [DataContract]
    public class Host
    {
        [DataMember]
        public string Id { get; private set; }

        [DataMember]
        public string Uri { get; set; }

        [DataMember]
        public int Port { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int PingEvery { get; set; }

        [DataMember]
        public PingStatus Status { get; set; }
        
        public List<PingHistory> PingHistory { get; set; }


        private readonly Ping _ping = new Ping();
        private readonly Timer _timer = new Timer();



        public Host()
        {
            Id = Guid.NewGuid().ToString();
            Status = PingStatus.Down;

            PingHistory = new List<PingHistory>();
        }


        public Host(string uri, int port, string name, int pingEvery)
        {
            Id = Guid.NewGuid().ToString();
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
                var reply = _ping.Send(Uri);
                if (reply != null)
                {
                    if (reply.Status == IPStatus.Success)
                    {
                        Status = PingStatus.Up;
                        PingHistory.Add(new PingHistory
                            {
                                PingedAt = DateTime.Now, 
                                Duration = reply.RoundtripTime, 
                                Status = PingStatus.Up,
                                Message = Encoding.UTF8.GetString(reply.Buffer)
                            });

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
            try
            {
                var history = PingHistory.Where(x => x.Status == PingStatus.Up).ToList();
                if (history.Count > 0)
                    return Convert.ToInt32(history.Average(x => x.Duration));

                return null;
            }
            catch
            {
                return null;
            }
        }

        public int? AverageResponseTimeRecent()
        {
            try
            {
                var history = PingHistory.Where(x => x.PingedAt >= DateTime.Now.AddMinutes(-10) && x.Status == PingStatus.Up).ToList();
                if (history.Count > 0)
                    return Convert.ToInt32(history.Average(x => x.Duration));

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

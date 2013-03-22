using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using PingIt.Domain;

namespace PingIt.Wpf.ViewModels
{
    public class DisplayHostViewModel
    {
        private readonly Host _host;
        private List<ResponseGraphViewModel> _recentGraphData;
        private List<ResponseGraphViewModel> _averageGraphData;
        private readonly Timer _timer;

        private const int PRIVATE_GRAPH_SIZE = 50;



        public DisplayHostViewModel(Host host)
        {
            _host = host;

            _recentGraphData = new List<ResponseGraphViewModel>(PRIVATE_GRAPH_SIZE);
            _averageGraphData = new List<ResponseGraphViewModel>(PRIVATE_GRAPH_SIZE);


            _timer = new Timer { Interval = 1000 };
            _timer.Elapsed += (sender, args) => UpdateGraphData();
            _timer.Start();
        }

        

        public string Name { get { return _host.Name.ToLower(); } }
        public PingStatus Status { get { return _host.PingHistory.Any() ? _host.PingHistory.Last().Status : PingStatus.Down; } }


        public long? LastPingTime { get { return _host.PingHistory.Any() ? _host.PingHistory.Last().Duration : (long?)null; } }
        public long? AveragePingTime { get { return _host.AverageResponseTime(); } }
        public long? AveragePingTimeRecent { get { return _host.AverageResponseTimeRecent(); } }


        public List<ResponseGraphViewModel> RecentGraphModel { get { return _recentGraphData; } }
        public List<ResponseGraphViewModel> AverageGraphModel { get { return _averageGraphData; } }


        private void UpdateGraphData()
        {
            // if we have more that 50 items in the list, dump the first item from the list so we have space on the end for the next item
            if (_recentGraphData.Count >= PRIVATE_GRAPH_SIZE)
                _recentGraphData = _recentGraphData.Skip(1).Take(_recentGraphData.Count).ToList();

            _recentGraphData.Add(new ResponseGraphViewModel(_host.AverageResponseTimeRecent(), DateTime.Now.Ticks));

            // if we have more that 50 items in the list, dump the first item from the list so we have space on the end for the next item
            if (_averageGraphData.Count >= PRIVATE_GRAPH_SIZE)
                _averageGraphData = _averageGraphData.Skip(1).Take(_averageGraphData.Count).ToList();

            _averageGraphData.Add(new ResponseGraphViewModel(_host.AverageResponseTime(), DateTime.Now.Ticks));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using PingIt.Domain;

namespace PingIt.Wpf.ViewModels
{
    public class HostListItemViewModel : ViewModelBase
    {
        private Host _host;
        private List<ResponseGraphViewModel> _graphData;
        private List<ResponseGraphViewModel> _averageGraphData;
        private readonly Timer _timer;

        private const int PRIVATE_GRAPH_SIZE = 100;


        /// <summary>
        /// ctor used for the Designer with the xaml ViewModel
        /// </summary>
        public HostListItemViewModel()
        {
            _host = new Host()
                {
                    PingHistory = new List<PingHistory>()
                        {
                            new PingHistory {Duration = 650, PingedAt = DateTime.Now, Status = PingStatus.Up},
                            new PingHistory {Duration = 50, PingedAt = DateTime.Now, Status = PingStatus.Up},
                            new PingHistory {Duration = 55, PingedAt = DateTime.Now, Status = PingStatus.Up},
                            new PingHistory {Duration = 65, PingedAt = DateTime.Now, Status = PingStatus.Up},
                            new PingHistory {Duration = 550, PingedAt = DateTime.Now, Status = PingStatus.Up},
                            new PingHistory {Duration = 650, PingedAt = DateTime.Now, Status = PingStatus.Up}
                        }
                };

            _graphData = new List<ResponseGraphViewModel>(PRIVATE_GRAPH_SIZE);
            _averageGraphData = new List<ResponseGraphViewModel>(PRIVATE_GRAPH_SIZE);

            UpdateGraphData();
        }

        public HostListItemViewModel(Dispatcher dispatcher, Host host)
            : base(dispatcher)
        {
            _host = host;
            _host.Start();

            _graphData = new List<ResponseGraphViewModel>(PRIVATE_GRAPH_SIZE);
            _averageGraphData = new List<ResponseGraphViewModel>(PRIVATE_GRAPH_SIZE);


            _timer = new Timer { Interval = 100 };
            _timer.Elapsed += (sender, args) => UpdateGraphData();
            _timer.Start();
        }



        public string Name
        {
            get { return _host == null || _host.Name == null ? null : _host.Name.ToUpperInvariant(); }
            set
            {
                if (_host == null)
                    _host = new Host() { Name = value };
                else
                    _host.Name = value;

                NotifyPropertyChanged("Name");
            }
        }

        public PingStatus Status
        {
            get
            {
                var history = _host.LastPingHistory();
                return history == null ? PingStatus.Down : history.Status;
            }
            set
            {
                if (_host == null)
                    _host = new Host() { Status = value };
                else
                    _host.Status = value;

                NotifyPropertyChanged("Status");
            }
        }

        public string LastMessage
        {
            get
            {
                var history = _host.LastPingHistory();
                return history == null ? "" : history.Message;
            }
        }


        public SolidColorBrush StatusFillColor
        {
            get
            {
                if (_host.Status == PingStatus.Up)
                    return (SolidColorBrush)Application.Current.Resources["AppAccentGreenBrush"];

                return (SolidColorBrush)Application.Current.Resources["AppAccentRedBrush"];
            }
        }


        public string LastPingTime
        {
            get
            {
                var history = _host.LastPingHistory();
                return history == null ? "n/a" : string.Format("{0:0,0}", _host.PingHistory.Last().Duration);
            }
        }

        public SolidColorBrush LastPingTimeFillColor
        {
            get
            {
                var ping = _host.PingHistory.LastOrDefault();
                if (ping != null && ping.Duration < 500)
                    return (SolidColorBrush)Application.Current.Resources["AppAccentGreenBrush"];

                return (SolidColorBrush)Application.Current.Resources["AppAccentRedBrush"];
            }
        }


        public string AveragePingTime
        {
            get
            {
                var response = _host.AverageResponseTime();
                return response == null ? "n/a" : string.Format("{0:0,0}", response);
            }
        }

        public SolidColorBrush AveragePingTimeFillColor
        {
            get
            {
                var ping = _host.AverageResponseTime();
                if (ping != null && ping < 500)
                    return (SolidColorBrush)Application.Current.Resources["AppAccentGreenBrush"];

                return (SolidColorBrush)Application.Current.Resources["AppAccentRedBrush"];
            }
        }

        public string AveragePingTimeRecent
        {
            get
            {
                var response = _host.AverageResponseTimeRecent();
                return response == null ? "n/a" : string.Format("{0:0,0}", response);
            }
        }


        public List<ResponseGraphViewModel> GraphModel
        {
            get { return _graphData; }
        }


        private void UpdateGraphData()
        {
            // if we have more that 50 items in the list, dump the first item from the list so we have space on the end for the next item
            if (_graphData.Count >= PRIVATE_GRAPH_SIZE)
                _graphData = _graphData.Skip(1).Take(_graphData.Count).ToList();

            var history = _host.LastPingHistory();
            _graphData.Add(new ResponseGraphViewModel(_host.AverageResponseTimeRecent(), history == null ? 0 : history.Duration, DateTime.Now.Ticks));

            RefreshPropertiesOnUI();
        }

        private void RefreshPropertiesOnUI()
        {
            NotifyPropertyChanged("GraphModel");
            NotifyPropertyChanged("AveragePingTimeRecent");
            NotifyPropertyChanged("LastPingTime");
            NotifyPropertyChanged("AveragePingTime");
            NotifyPropertyChanged("Status");

            NotifyPropertyChanged("StatusFillColor");
            NotifyPropertyChanged("AveragePingTimeFillColor");
            NotifyPropertyChanged("LastPingTimeFillColor");
        }
    }
}

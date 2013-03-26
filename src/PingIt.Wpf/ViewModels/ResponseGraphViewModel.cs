using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace PingIt.Wpf.ViewModels
{
    public class ResponseGraphViewModel : ViewModelBase
    {
        public int AvgDuration { get; set; }
        public long LastDuration { get; set; }

        public long Time { get; set; }


        public ResponseGraphViewModel() { }
        public ResponseGraphViewModel(int? duration, long last, long time) : base(null)
        {
            AvgDuration = duration == null ? 0 : duration.Value;
            LastDuration = last;

            Time = time;
        }
    }
}
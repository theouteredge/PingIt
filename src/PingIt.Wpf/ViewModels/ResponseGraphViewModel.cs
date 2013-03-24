using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


namespace PingIt.Wpf.ViewModels
{
    public class ResponseGraphViewModel : ViewModelBase
    {
        private int _duration;
        private long _time;

        public int Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }
        public long Time
        {
            get { return _time; }
            set { _time = value; }
        }


        public ResponseGraphViewModel() { }
        public ResponseGraphViewModel(int? duration, long time) : base(null)
        {
            _duration = duration == null ? 0 : duration.Value;
            _time = time;
        }
    }
}
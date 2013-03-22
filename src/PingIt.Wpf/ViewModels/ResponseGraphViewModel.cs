namespace PingIt.Wpf.ViewModels
{
    public class ResponseGraphViewModel
    {
        private readonly int _duration;
        private readonly long _time;

        public int Duration { get { return _duration; } }
        public long Time { get { return _time; } }

        public ResponseGraphViewModel(int? duration, long time)
        {
            _duration = duration == null ? 0 : duration.Value;
            _time = time;
        }
    }
}
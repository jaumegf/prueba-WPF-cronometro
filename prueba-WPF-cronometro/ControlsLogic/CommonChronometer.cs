using System;
using System.Windows.Threading;
using static prueba_WPF_cronometro.ControlsLogic.IChronometer;

namespace prueba_WPF_cronometro.ControlsLogic
{
    public class CommonChronometer: IChronometer
    {
        public DispatcherTimer Timer { get; set; }

        public DelegateRefresh CallBackRefresh { get; set; }

        public void Refresh(DelegateRefresh callback)
        {
            callback();
        }

        public TimeSpan AgregatedTime { get; set; }

        public DateTime StartTime { get; set; }


        public virtual void Start()
        {
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            Timer.Tick += OnTimerTick;

            StartTime = DateTime.Now;
            AgregatedTime = new TimeSpan(0);
            Timer.Start();
        }

        public virtual void Pause()
        {
            AgregatedTime +=  (DateTime.Now - StartTime);
            Timer.Stop();
        }

        public virtual void Resume()
        {
            StartTime = DateTime.Now;
            Timer.Start();
        }

        public virtual void Stop()
        {
            Timer.Stop();
            AgregatedTime = new TimeSpan(0);
        }


        public virtual string DefaultShowTime => "00:00:00";

        public virtual string TimeToShow
        {
            get
            {
                TimeSpan duration = (DateTime.Now - StartTime + AgregatedTime);
                return $"{Math.Floor(duration.TotalHours)}:{(Math.Floor(duration.TotalMinutes)%60).ToString("00")}:{(Math.Floor(duration.TotalSeconds)%60).ToString("00")}";
            }
        }


        private void OnTimerTick(object sender, EventArgs e)
        {
            CallBackRefresh();
        }
    }
}

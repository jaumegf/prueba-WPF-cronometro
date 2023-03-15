using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace prueba_WPF_cronometro.Logic
{
    public class CommonChronometer: IChronometer
    {
        public DispatcherTimer Timer { get; set; }

        public TimeSpan AgregatedTime { get; set; }

        public DateTime StartTime { get; set; }


        public virtual void Init(DispatcherTimer timer)
        {
            Timer = timer;
            Start();
        }

        public virtual void Start()
        {
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
                return $"{Math.Floor(duration.TotalHours)}:{(Math.Floor(duration.TotalMinutes)%60).ToString("00")}:{(duration.TotalSeconds%60).ToString("00")}";
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace prueba_WPF_cronometro.Logic
{
    public class SecondsChronometer: CommonChronometer, IChronometer
    {
        public override string DefaultShowTime => "0";

        public override string TimeToShow
        {
            get
            {
                TimeSpan duration = (DateTime.Now - StartTime + AgregatedTime);
                return $"{Math.Floor(duration.TotalSeconds)}";
            }
        }
    }
}

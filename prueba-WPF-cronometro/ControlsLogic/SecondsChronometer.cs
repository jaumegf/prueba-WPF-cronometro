using System;

namespace prueba_WPF_cronometro.ControlsLogic
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

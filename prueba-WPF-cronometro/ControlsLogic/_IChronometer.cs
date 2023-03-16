using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace prueba_WPF_cronometro.ControlsLogic
{
    public interface IChronometer
    {
        DispatcherTimer Timer { get; set; }

        delegate void DelegateRefresh();
        DelegateRefresh CallBackRefresh { get; set; }
        void Refresh(DelegateRefresh callback);

        /// <summary>
        /// Tiempo acumulado que debe acumularse en cada pausa.
        /// Debe reiniciarse cada vez que se inicia de nuevo
        /// </summary>
        TimeSpan AgregatedTime { get; set; }

        /// <summary>
        /// Inicio del cronómetro
        /// </summary>
        DateTime StartTime { get; set; }

        void Start();

        void Pause();

        void Resume();

        void Stop();

        
        /// <summary>
        /// Texto que muestra por defecto al iniciar
        /// </summary>
        string DefaultShowTime { get; }

        /// <summary>
        /// Texto que muestra en cada tick
        /// </summary>
        string TimeToShow { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace prueba_WPF_cronometro.Logic
{
    public interface IChronometer
    {
        DispatcherTimer Timer { get; set; }

        /// <summary>
        /// Tiempo acumulado que debe acumularse en cada pausa.
        /// Debe reiniciarse cada vez que se inicia de nuevo
        /// </summary>
        TimeSpan AgregatedTime { get; set; }

        /// <summary>
        /// Inicio del cronómetro
        /// </summary>
        DateTime StartTime { get; set; }

        void Init(DispatcherTimer timer);

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

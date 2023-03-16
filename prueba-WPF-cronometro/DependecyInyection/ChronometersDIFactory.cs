using prueba_WPF_cronometro.ControlsLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prueba_WPF_cronometro.DependecyInyection
{
    /*
     Tal y como piden las indicaciones del ejercicio,
     se pide un ejemplo de implementación de Inyección de dependencias(DI) sin utilizar ningún framework.
     Solución aportada:
         Se crea una lista de objetos IChronometer(que implementan la interfaz IChronometer)
         Tanto la interfaz como los objetos, estan definidos en el namespace prueba_WPF_cronometro.ControlsLogic
         Ambos objetos implementan la misma lógica, pero como ejemplo tienen la siguiente diferencia:
            * CommonChronometer: Mostrará el tiempo en formato hh:mm:ss
            * SecondsChronometer: Mostrará el total de segundos transcurridos.
            *  Puesto que la lógica de SecondsChronometer es igual a la de CommonChronometer, 
              se utiliza la herencia con sobreescritura de métodos (override) para reutilizar la lógica
              , no obstante, queda así un ejemplo de que se puede cambiar la lógica de cualquiera de los cronómetros
    */
    public static class ChronometersDIFactory
    {
        public enum EChronometers
        {
            COMMON,
            SECONDS
        }

        public static readonly List<KeyValuePair<EChronometers, IChronometer>> Chronometers =
                new List<KeyValuePair<EChronometers, IChronometer>>
                {
                    new KeyValuePair<EChronometers, IChronometer>(EChronometers.COMMON, new CommonChronometer()),
                    new KeyValuePair<EChronometers, IChronometer>(EChronometers.SECONDS, new SecondsChronometer()),
                };
    }
}

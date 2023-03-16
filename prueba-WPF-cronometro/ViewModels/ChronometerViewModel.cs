using prueba_WPF_cronometro.Commands;
using prueba_WPF_cronometro.ControlsLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using static prueba_WPF_cronometro.ControlsLogic.IChronometer;


namespace prueba_WPF_cronometro.ViewModels
{
    public class ChronometerViewModel : INotifyPropertyChanged
    {
        //Consultar clase ChronometersDIFactory para detalles sobre la solución aportada con Inyección de dependencias
        // Los comandos SelectCommonChronometer y SelectSecondsChronometer
        // . Están vinculados con los comandos de los botones del XALM, cronómetro clásico y cronómetro en segundos
        // . Harán uso de la clase ChronometersDIFactory para instanciar las clases CommonChronometer o SecondsChronometer 
        //   según la elección del usuario, y, de esta forma utilizar la lógica de uno u otro
        //   cronómetro según seleccione el usuario sin conocer la implementación específica de cada uno

        private IChronometer SelectedChronometer;
             
        private DependecyInyection.ChronometersDIFactory.EChronometers CurrentChronometer;


        public ChronometerViewModel()
        {
            SelectCommonChronometerCommand = new RelayCommand(SelectCommonChronometer);
            SelectSecondsChronometerCommand = new RelayCommand(SelectSecondsChronometer);
            SelectCommonChronometerEnabled = true;
            SelectSecondsChronometerEnabled = true;


            StartCommand = new RelayCommand(Start);
            PauseCommand = new RelayCommand(Pause);
            StopCommand = new RelayCommand(Stop);
            StartEnabled = true;
            PauseEnabled = false;
            StopEnabled = false;

            PauseContent = "Pause";

            SelectChrono(DependecyInyection.ChronometersDIFactory.EChronometers.COMMON);
        }


        private void SelectChrono(DependecyInyection.ChronometersDIFactory.EChronometers chroneToSelect)
        {
            CurrentChronometer = chroneToSelect;
            SelectedChronometer = DependecyInyection.ChronometersDIFactory
                                  .Chronometers
                                  .FirstOrDefault(c => c.Key == CurrentChronometer).Value;

            SelectedChronometer.CallBackRefresh = new DelegateRefresh(Chronometer_Tick);

            if (chroneToSelect== DependecyInyection.ChronometersDIFactory.EChronometers.COMMON)
            {
                SelectCommonChronometerSelectedColor = Brushes.LightGray;
                SelectSecondsChronometerSelectedColor = Brushes.White;
            }
            else
            {
                SelectCommonChronometerSelectedColor = Brushes.White;
                SelectSecondsChronometerSelectedColor = Brushes.LightGray;
            }

            TimeToShow = SelectedChronometer.DefaultShowTime;
            ChronometerColor = Brushes.Gray;
        }


        public ICommand SelectCommonChronometerCommand { get; private set; }

        public ICommand SelectSecondsChronometerCommand { get; private set; }

        public ICommand StartCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }



        private void SelectCommonChronometer(object obj)
        {
            SelectChrono(DependecyInyection.ChronometersDIFactory.EChronometers.COMMON);
        }

        private void SelectSecondsChronometer(object obj)
        {
            SelectChrono(DependecyInyection.ChronometersDIFactory.EChronometers.SECONDS);
        }

        private void Chronometer_Tick()
        {
            TimeToShow = SelectedChronometer.TimeToShow;
        }


        private void Start(object obj)
        {
            SelectCommonChronometerEnabled = false;
            SelectSecondsChronometerEnabled = false;

            StartEnabled = false;
            PauseEnabled = true;
            StopEnabled = true;

            TimeToShow = SelectedChronometer.DefaultShowTime;
            SelectedChronometer.Start();
            ChronometerColor = Brushes.Green;
        }


        private bool PauseOn { get; set; }

        private void Pause(object obj)
        {
            PauseOn = !PauseOn;

            if (PauseOn)
            {
                PauseContent = "Resume";
                SelectedChronometer.Pause();
                ChronometerColor = Brushes.Gray;
            }
            else
            {
                PauseContent = "Pause";
                SelectedChronometer.Resume();
                ChronometerColor = Brushes.Green;
            }
        }

        private void Stop(object obj)
        {
            SelectCommonChronometerEnabled = true;
            SelectSecondsChronometerEnabled = true;

            SelectedChronometer.Stop();
            PauseOn = false;
            PauseContent = "Pause";
            ChronometerColor = Brushes.Red;

            StartEnabled = true;
            PauseEnabled = false;
            StopEnabled = false;
        }


        private bool _selectCommonChronometerEnabled;
        public bool SelectCommonChronometerEnabled
        {
            get { return _selectCommonChronometerEnabled; }
            set
            {
                _selectCommonChronometerEnabled = value;
                OnPropertyChanged("SelectCommonChronometerEnabled");
            }
        }


        private Brush _selectCommonChronometerSelectedColor;
        public Brush SelectCommonChronometerSelectedColor
        {
            get { return _selectCommonChronometerSelectedColor; }
            set
            {
                _selectCommonChronometerSelectedColor = value;
                OnPropertyChanged("SelectCommonChronometerSelectedColor");
            }
        }


        private bool _selectSecondsChronometerEnabled;
        public bool SelectSecondsChronometerEnabled
        {
            get { return _selectSecondsChronometerEnabled; }
            set
            {
                _selectSecondsChronometerEnabled = value;
                OnPropertyChanged("SelectSecondsChronometerEnabled");
            }
        }


        private Brush _selectSecondsChronometerSelectedColor;
        public Brush SelectSecondsChronometerSelectedColor
        {
            get { return _selectSecondsChronometerSelectedColor; }
            set
            {
                _selectSecondsChronometerSelectedColor = value;
                OnPropertyChanged("SelectSecondsChronometerSelectedColor");
            }
        }



        private bool _startEnabled;
        public bool StartEnabled
        {
            get { return _startEnabled; }
            set
            {
                _startEnabled = value;
                OnPropertyChanged("StartEnabled");
            }
        }

        private string _pauseContent;
        public string PauseContent
        {
            get { return _pauseContent; }
            set
            {
                if (_pauseContent != value)
                {
                    _pauseContent = value;
                    OnPropertyChanged("PauseContent");
                }
            }
        }

        private bool _pauseEnabled;
        public bool PauseEnabled
        {
            get { return _pauseEnabled; }
            set
            {
                _pauseEnabled = value;
                OnPropertyChanged("PauseEnabled");
            }
        }

        private bool _stopEnabled;
        public bool StopEnabled
        {
            get { return _stopEnabled; }
            set
            {
                _stopEnabled = value;
                OnPropertyChanged("StopEnabled");
            }
        }

        
        private string _time;
        public string TimeToShow
        {
            get { return _time; }
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged("TimeToShow");
                }
            }
        }

        private Brush _chronometerColor = Brushes.Gray;
        public Brush ChronometerColor
        {
            get { return _chronometerColor; }
            set
            {
                _chronometerColor = value;
                OnPropertyChanged(nameof(ChronometerColor)); // Notify the view that the property has changed
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

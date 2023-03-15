using prueba_WPF_cronometro.Commands;
using prueba_WPF_cronometro.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace prueba_WPF_cronometro.ViewModels
{
    public class ChronometerViewModel : INotifyPropertyChanged
    {
        private IChronometer SelectedChronometer;
        private DispatcherTimer _timer;

        //Consultar ChronometersDIFactory para detalles sobre la solución aportada con Inyección de dependencias
        // Los comandos InitCommonChronometer InitSecondsChronometer
        // . Están vinculados con los botones del XALM, cronómetro clásico y cronómetro en segundos
        // . Harán uso de esta clase para utilizar uno u otro cronómetro según seleccione el usuario
        //  , sin conocer la implementación específica de cada uno

        private DependecyInyection.ChronometersDIFactory.EChronometers CurrentChronometer;


        public ChronometerViewModel()
        {
            InitCommonChronometerCommand = new RelayCommand(InitCommonChronometer);
            InitSecondsChronometerCommand = new RelayCommand(InitSecondsChronometer);
            InitCommonChronometerEnabled = true;
            InitSecondsChronometerEnabled = true;


            StartCommand = new RelayCommand(Start);
            PauseCommand = new RelayCommand(Pause);
            StopCommand = new RelayCommand(Stop);
            StartEnabled = true;
            PauseEnabled = false;
            StopEnabled = false;

            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += OnTimerTick;

            SelectChrono(DependecyInyection.ChronometersDIFactory.EChronometers.COMMON);
            TimeToShow = SelectedChronometer.DefaultShowTime;
        }


        private void SelectChrono(DependecyInyection.ChronometersDIFactory.EChronometers chroneToSelect)
        {
            CurrentChronometer = chroneToSelect;
            SelectedChronometer = DependecyInyection.ChronometersDIFactory
                                  .Chronometers
                                  .FirstOrDefault(c => c.Key == CurrentChronometer).Value;


            if (chroneToSelect== DependecyInyection.ChronometersDIFactory.EChronometers.COMMON)
            {
                InitCommonChronometerSelectedColor = Brushes.LightGray;
                InitSecondsChronometerSelectedColor = Brushes.White;
            }
            else
            {
                InitCommonChronometerSelectedColor = Brushes.White;
                InitSecondsChronometerSelectedColor = Brushes.LightGray;
            }

            TimeToShow = SelectedChronometer.DefaultShowTime;
        }


        public ICommand InitCommonChronometerCommand { get; private set; }

        public ICommand InitSecondsChronometerCommand { get; private set; }

        public ICommand StartCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }



        private void InitCommonChronometer(object obj)
        {
            SelectChrono(DependecyInyection.ChronometersDIFactory.EChronometers.COMMON);
        }

        private void InitSecondsChronometer(object obj)
        {
            SelectChrono(DependecyInyection.ChronometersDIFactory.EChronometers.SECONDS);
        }


        private void OnTimerTick(object sender, EventArgs e)
        {
            TimeToShow = SelectedChronometer.TimeToShow;
        }

        private void Start(object obj)
        {
            InitCommonChronometerEnabled = false;
            InitSecondsChronometerEnabled = false;

            StartEnabled = false;
            PauseEnabled = true;
            StopEnabled = true;

            TimeToShow = SelectedChronometer.DefaultShowTime;
            SelectedChronometer.Init(_timer);
            ChronometerColor = Brushes.Green;
        }


        private bool PauseOn { get; set; }

        private void Pause(object obj)
        {
            PauseOn = !PauseOn;

            if (PauseOn)
            {
                SelectedChronometer.Pause();
                ChronometerColor = Brushes.Gray;
            }
            else
            {
                SelectedChronometer.Resume();
                ChronometerColor = Brushes.Green;
            }
        }

        private void Stop(object obj)
        {
            InitCommonChronometerEnabled = true;
            InitSecondsChronometerEnabled = true;

            SelectedChronometer.Stop();
            PauseOn = false;
            ChronometerColor = Brushes.Red;

            StartEnabled = true;
            PauseEnabled = false;
            StopEnabled = false;
        }


        private bool _initCommonChronometerEnabled;
        public bool InitCommonChronometerEnabled
        {
            get { return _initCommonChronometerEnabled; }
            set
            {
                _initCommonChronometerEnabled = value;
                OnPropertyChanged("InitCommonChronometerEnabled");
            }
        }


        private Brush _initCommonChronometerSelectedColor;
        public Brush InitCommonChronometerSelectedColor
        {
            get { return _initCommonChronometerSelectedColor; }
            set
            {
                _initCommonChronometerSelectedColor = value;
                OnPropertyChanged("InitCommonChronometerSelectedColor");
            }
        }



        private bool _initSecondsChronometerEnabled;
        public bool InitSecondsChronometerEnabled
        {
            get { return _initSecondsChronometerEnabled; }
            set
            {
                _initSecondsChronometerEnabled = value;
                OnPropertyChanged("InitSecondsChronometerEnabled");
            }
        }


        private Brush _initSecondsChronometerSelectedColor;
        public Brush InitSecondsChronometerSelectedColor
        {
            get { return _initSecondsChronometerSelectedColor; }
            set
            {
                _initSecondsChronometerSelectedColor = value;
                OnPropertyChanged("InitSecondsChronometerSelectedColor");
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

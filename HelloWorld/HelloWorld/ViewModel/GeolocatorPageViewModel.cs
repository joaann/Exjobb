using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Geolocator.Plugin;
using XLabs;
using HelloWorld.Model;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Services.Geolocation;

namespace HelloWorld.ViewModel
{
    internal class GeolocatorPageViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private Button _stopBtn;
        private Button _startBtn;
        private MainModel _mainModel;
        private Command _myCommand;
        private Command _startCommand;
        private Position _position;

        private IGeolocator _geoLocator;

        public GeolocatorPageViewModel()
        {
            _geoLocator = Resolver.Resolve<IGeolocator>();
            
            //var locator = CrossGeolocator.Current;
            _geoLocator.DesiredAccuracy = 10;
            _geoLocator.StartListening(1000, 0);
            _geoLocator.PositionChanged += (sender, args) =>
            {
                var positionArgs = (PositionEventArgs) args;
                DisplayMainModel.Latitude = "Latitude: " + positionArgs.Position.Latitude.ToString();
                DisplayMainModel.Longitude = "Longitude: " + positionArgs.Position.Longitude.ToString();
                DisplayMainModel.Timestamp = positionArgs.Position.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");

            };
            //get position async
            //_position = await locator.GetPositionAsync(timeout: 10000);
            _geoLocator.GetPositionAsync(5000).ContinueWith(task =>
            {
                var position = task.Result;
                Debug.WriteLine(position.Latitude);
                Debug.WriteLine(position.Longitude);

                DisplayMainModel = new MainModel
                {
                    Title = "Hello world!",
                    Latitude = "Latitude: " + position.Latitude.ToString(),
                    Longitude = "Longitude: " + position.Longitude.ToString(),
                    Timestamp = position.Timestamp.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss")
                };
            });

            MyCommand = new Command(StopTracking);
            StartCommand = new Command(StartTracking);

            StopBtnButton = new Button
            {
                Text = "Stop Tracking"
            };
            StartBtnButton = new Button
            {
                Text = "Start tracking"
            };
        }

        public MainModel DisplayMainModel
        {
            get
            {
                return _mainModel ?? (_mainModel = new MainModel
                {
                    //if null set title to error
                    Title = "Main model is null"
                });
            }
            set { SetProperty(ref _mainModel, value); }
        }

        public Button StopBtnButton
        {
            get { return _stopBtn; }
            set { SetProperty(ref _stopBtn, value); }
        }

        public Button StartBtnButton
        {
            get { return _startBtn; }
            set { SetProperty(ref _startBtn, value); }
        }

        public Command MyCommand
        {
            get { return _myCommand; }
            set { SetProperty(ref _myCommand, value); }
        }

        public Command StartCommand
        {
            get { return _startCommand; }
            set { SetProperty(ref _startCommand, value); }

        }
        public void StopTracking()
        {
            _geoLocator.StopListening();
            StopBtnButton.Text = "Stopped tracking";
        }

        public void StartTracking()
        {
            _geoLocator.StartListening(1000,0);
            StopBtnButton.Text = "Stop tracking";
        }

        public IGeolocator Geolocator
        {
            get { return _geoLocator;}
            set { SetProperty(ref _geoLocator, value); }
        }

        public Position Position
        {
            get { return _position ?? (_position = new Position()); }
            set { SetProperty(ref _position, value); }

        }
    }
}
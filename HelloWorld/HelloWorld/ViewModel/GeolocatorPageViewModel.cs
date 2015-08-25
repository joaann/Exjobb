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
        private Button _btn;
        private MainModel _mainModel;
        private Command _myCommand;
        private Position _position;

        private Boolean _hasLoadedPosition = false;
        private Boolean _loadingPosition = true;

        public GeolocatorPageViewModel()
        {
            var geoLocator = Resolver.Resolve<IGeolocator>();
            
            //var locator = CrossGeolocator.Current;
            geoLocator.DesiredAccuracy = 50;
            geoLocator.StartListening(1000, 0);
            geoLocator.PositionChanged += (sender, args) =>
            {
                var positionArgs = (PositionEventArgs) args;
                DisplayMainModel.Latitude = "Latitude: " + positionArgs.Position.Latitude.ToString();
                DisplayMainModel.Longitude = "Longitude: " + positionArgs.Position.Longitude.ToString();
                DisplayMainModel.Timestamp = positionArgs.Position.Timestamp.ToString("dd/MM/yyyy HH:mm:ss");

            };
            //get position async
            //_position = await locator.GetPositionAsync(timeout: 10000);
            geoLocator.GetPositionAsync(10000).ContinueWith(task =>
            {
                var position = task.Result;
                Debug.WriteLine(position.Latitude);
                Debug.WriteLine(position.Longitude);
                _loadingPosition = false;
                _hasLoadedPosition = true;

                DisplayMainModel = new MainModel
                {
                    Title = "Hello world!",
                    Latitude = "Latitude: " + position.Latitude.ToString(),
                    Longitude = "Longitude: " + position.Longitude.ToString(),
                    Timestamp = position.Timestamp.ToString("dd/MM/yyyy HH:mm:ss")
                };
            });

            MyCommand = new Command(UpdatePosition);

            BtnButton = new Button
            {
                Text = "Click"
            };
        }

        public MainModel DisplayMainModel
        {
            get
            {
                return _mainModel ?? (_mainModel = new MainModel
                {
                    //if null set title to error
                    Title = "error"
                });
            }
            set { SetProperty(ref _mainModel, value); }
        }

        public Button BtnButton
        {
            get { return _btn; }
            set { SetProperty(ref _btn, value); }
        }

        public Command MyCommand
        {
            get { return _myCommand; }
            set { SetProperty(ref _myCommand, value); }
        }

        public void UpdatePosition()
        {
            //BtnButton.Text = "Button Clicked! ";
        }

        public Position Position
        {
            get { return _position ?? (_position = new Position()); }
            set { SetProperty(ref _position, value); }

        }

        public Boolean HasLoadedPosition
        {
            get { return _hasLoadedPosition; }
            set { SetProperty(ref _hasLoadedPosition, value); }
        }

        public Boolean LoadingPosition
        {
            get { return _loadingPosition; }
            set { SetProperty(ref _loadingPosition, value); }
        }
    }
}
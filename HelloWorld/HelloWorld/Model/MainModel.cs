using System;
using XLabs.Data;

namespace HelloWorld.Model
{
    internal class MainModel : ObservableObject
    {
        private string _latitude;
        private string _longitude;
        private string _timestamp;
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public string Latitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value); }
        }

        public string Longitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value); }
        }

        public string Timestamp
        {
            get { return _timestamp; }
            set { SetProperty(ref _timestamp, value); }
        }
    }
}
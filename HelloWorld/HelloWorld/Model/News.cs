using XLabs.Data;

namespace App1.Model
{
    internal class News : ObservableObject
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
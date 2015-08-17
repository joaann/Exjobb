using HelloWorld.Model;
using Xamarin.Forms;

namespace HelloWorld.ViewModel
{
    internal class HomePageViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        private Button _btn;
        private Command _myCommand;
        private News _news;

        public HomePageViewModel()
        {
            DisplayNews = new News
            {
                Title = "Hello world"
            };

            MyCommand = new Command(UpdateString);

            BtnButton = new Button
            {
                Text = "Click"
            };
        }

        public News DisplayNews
        {
            get
            {
                return _news ?? (_news = new News
                {
                    //if null set title to error
                    Title = "error"
                });
            }
            set { SetProperty(ref _news, value); }
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

        public void UpdateString()
        {
            DisplayNews.Title = "Button Clicked ";
        }
    }
}
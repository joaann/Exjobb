using HelloWorld.ViewModel;
using XLabs.Forms.Mvvm;

namespace HelloWorld.Pages
{
    public partial class GeolocatorPage : BaseView
    {
        public GeolocatorPage()
        {
            InitializeComponent();
            BindingContext = new GeolocatorPageViewModel();
        }
    }
}
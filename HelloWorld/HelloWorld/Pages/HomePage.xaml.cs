using HelloWorld.ViewModel;
using XLabs.Forms.Mvvm;

namespace HelloWorld.Pages
{
    public partial class HomePage : BaseView
    {
        public HomePage()
        {
            InitializeComponent();
            BindingContext = new NewsPageViewModel();
        }
    }
}
using App1.ViewModel;
using XLabs.Forms.Mvvm;

namespace App1.Pages
{
    public partial class NewsPage : BaseView
    {
        public NewsPage()
        {
            InitializeComponent();
            BindingContext = new NewsPageViewModel();
        }
    }
}
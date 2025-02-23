using OsvitaApp.Models;
using OsvitaApp.PageModels;

namespace OsvitaApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}
namespace OsvitaApp.Pages;

public partial class ChaptersPage : ContentPage
{
    private readonly ChaptersPageVM _vm;

    public ChaptersPage(ChaptersPageVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }
}
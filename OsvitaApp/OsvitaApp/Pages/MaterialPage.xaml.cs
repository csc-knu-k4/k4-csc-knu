namespace OsvitaApp.Pages;

public partial class MaterialPage : ContentPage
{
    private readonly MaterialPageVM _vm;

    public MaterialPage(MaterialPageVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }
}
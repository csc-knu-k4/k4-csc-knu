namespace OsvitaApp.Pages;

public partial class SubjectsPage : ContentPage
{
    private readonly SubjectsPageVM _vm;
    public SubjectsPage(SubjectsPageVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }
}
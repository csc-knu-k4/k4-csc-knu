namespace OsvitaApp.Pages;

public partial class SubjectPage : ContentPage
{
    private readonly SubjectPageVM _vm;

    public SubjectPage(SubjectPageVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }
}
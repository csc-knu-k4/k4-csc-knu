using System.Diagnostics;

namespace OsvitaApp.Pages;

public partial class RegistrationPage : ContentPage
{
    private readonly RegistrationPageVM _vm;
    public RegistrationPage(RegistrationPageVM vm)
    {
        _vm = vm;
        this.BindingContext = _vm;
        InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        Debug.WriteLine(Shell.Current.Navigation.NavigationStack.Count());
    }


}
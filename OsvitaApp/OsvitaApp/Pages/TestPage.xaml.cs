namespace OsvitaApp.Pages;

public partial class TestPage : ContentPage
{
	private readonly TestPageVM _vm;
	public TestPage(TestPageVM vm)
	{
		_vm = vm;
		this.BindingContext = _vm;
		InitializeComponent();
	}
}
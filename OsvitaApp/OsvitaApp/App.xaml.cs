namespace OsvitaApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell())
            {
                TitleBar = new TitleBar
                {
                    Opacity = 1,
                    BackgroundColor = Colors.White,
                }
            };
            window.Width = 390;
            window.Height = 844;
            //window.MinimumWidth = 390;
            //window.MaximumHeight = 844;
            //window.MaximumWidth = 390;
            //window.MinimumHeight = 844;
            return window;
        }
    }
}
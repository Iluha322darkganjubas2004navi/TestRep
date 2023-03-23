namespace Calculator;

public partial class App : Application
{
	public App()
    {

        InitializeComponent();
        Routing.RegisterRoute("MainPage", typeof(MainPage));
        Routing.RegisterRoute("Calculator", typeof(Calculator));
        MainPage = new AppShell();
        
	}
}

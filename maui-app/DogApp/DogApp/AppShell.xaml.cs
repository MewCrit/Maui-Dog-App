
namespace DogApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
        InitializeComponent();
        Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        Routing.RegisterRoute(nameof(DogDetailPage), typeof(DogDetailPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
    }
}
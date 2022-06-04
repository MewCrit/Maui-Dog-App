using DogApp.Services;
using DogApp.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DogApp;

public static class MauiProgram 
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream("DogApp.appsettings.json");

        var config = new ConfigurationBuilder()
              .AddJsonStream(stream)
              .Build();


        builder.Configuration.AddConfiguration(config);

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<DogDetailPage>();
        builder.Services.AddTransient<AboutPage>();

        builder.Services.AddTransient<DogsViewModel>();
        builder.Services.AddTransient<DogDetailViewModel>();


        builder.Services.AddSingleton<IDogServices, DogServices>();

        var app = builder.Build();
        Services = app.Services;

        return app;
	}



    public static IServiceProvider Services { get; private set; }
}

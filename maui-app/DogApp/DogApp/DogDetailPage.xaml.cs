using DogApp.ViewModels;

namespace DogApp;

public partial class DogDetailPage : ContentPage
{
	private DogDetailViewModel _dog;

    public DogDetailPage(DogDetailViewModel dog)
	{

		InitializeComponent();
		BindingContext = dog;
        _dog = dog ?? throw new ArgumentNullException(nameof(dog));

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_dog.OnAppearing();
    }
}
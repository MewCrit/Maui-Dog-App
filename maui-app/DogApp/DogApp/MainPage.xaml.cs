using DogApp.ViewModels;

namespace DogApp;

public partial class MainPage : ContentPage
{

	private readonly DogsViewModel _dog;

	public MainPage(DogsViewModel dog)
	{
		_dog = dog;
		InitializeComponent();
		BindingContext = dog;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_dog.OnAppearing();
    }

     void CollectionView_SelectionChanged(object sender, EventArgs e) 
    {
         var binableObject = sender as BindableObject;
         _dog.Touched.Execute(binableObject.BindingContext);
    }


}
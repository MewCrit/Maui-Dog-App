using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DogApp.Models;
using DogApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DogApp.ViewModels
{
    public partial class DogsViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        public ObservableCollection<Dog> Dogs { get; } = new();
          
        private readonly IDogServices _dogServices;

        public Command<Dog> Touched { get; set; }

        public Command LoadDoggies { get; }


        public Dog _selectedItem;

        public Dog SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }


        public DogsViewModel(IDogServices dogServices)
        {
            Title = "Dog App";
            _dogServices = dogServices;

            LoadDoggies = new Command(async () => await GetDogies());

            Touched = new Command<Dog>(OnItemSelected);
        }

        [ICommand]
        async Task GetDogies()
        {
            IsBusy = true;
            try
            {
                var doggies = await _dogServices.ReadDogs();

                if (Dogs.Any())
                {
                    Dogs.Clear();
                }

                foreach (var dog in doggies)
                {
                    Dogs.Add(dog);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error Occured: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }




        async void OnItemSelected(Dog item)
        {
            if (item is null)
                return;


            await Shell.Current.GoToAsync($"{nameof(DogDetailPage)}?IDParameter={item.Id}");
        }


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
            LoadDoggies.Execute(null);
        }

      



    }
}

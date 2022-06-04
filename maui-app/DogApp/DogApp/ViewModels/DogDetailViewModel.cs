using DogApp.Services;
using System.Diagnostics;

namespace DogApp.ViewModels
{
    [QueryProperty(nameof(IDParameter), nameof(IDParameter))]
    public partial class DogDetailViewModel : BaseViewModel
    {

        private string _id;

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _name;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _picture;

        public string Picture
        {
            get => _picture;
            set => SetProperty(ref _picture, value);
        }

        private string _breed;

        public string Breed
        {
            get => _breed;
            set => SetProperty(ref _breed, value);
        }


        private string _about;

        public string About
        {
            get => _about;
            set => SetProperty(ref _about, value);
        }

        private string _gender;

        public string Gender
        {
            get => _gender;
            set => SetProperty(ref _gender, value);
        }


        private string _idParameter;

        public string IDParameter
        {
            get 
            { 
                return _idParameter; 
            }
            set 
            {

                if (value == null)
                    return;

                _idParameter = value;
                GetDoggyByID(value);
            }
        }

        private readonly IDogServices _dogServices;

        public DogDetailViewModel(IDogServices  dogServices)
        {
            _dogServices = dogServices ?? throw new ArgumentNullException(nameof(dogServices));
        }

        
       async void GetDoggyByID(string id)
       {
           IsBusy = true;
           try
           {
              if (string.IsNullOrEmpty(id))
              {
                    IsBusy = false;
                    await Shell.Current.DisplayAlert("Error!", "ID parameter is missing.", "OK");
              }


               var doggy = await _dogServices.ReadDogById(id);
               this.Id = doggy.Id;
               this.Name = doggy.Name;
               this.Picture = doggy.Picture;
               this.Breed = doggy.Breed;
               this.About = doggy.About;
               this.Gender = doggy.Gender;
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


        public void OnAppearing()
        {
            GetDoggyByID(_idParameter);
        }

    }
}

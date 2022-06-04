using DogApp.Models;

namespace DogApp.Services
{
    public interface IDogServices
    {
        Task<IEnumerable<Dog>> ReadDogs();

        Task<Dog> ReadDogById(string id);

    }
}

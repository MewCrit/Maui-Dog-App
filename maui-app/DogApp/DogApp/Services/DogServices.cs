using DogApp.Models;
using Microsoft.Extensions.Configuration;
using RestSharp;

namespace DogApp.Services
{
    public class DogServices : IDogServices
    {
        private readonly IConfiguration _configuration;

        private ApiServices _apiServices;

        public DogServices(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this._apiServices = _configuration.GetRequiredSection("ApiServices").Get<ApiServices>();
        }

        public async Task<Dog> ReadDogById(string id)
        {
            var client = new RestClient(_apiServices.DogBaseUrl);
            var request = new RestRequest($"dog/{id}", Method.Get);
            var doggy = await client.GetAsync<Dog>(request,
                                CancellationToken.None);

            return doggy;
        }

        public async Task<IEnumerable<Dog>> ReadDogs()
        {
            var client = new RestClient(_apiServices.DogBaseUrl);
            var request = new RestRequest("dog");
            var doggy = await client.GetAsync<IList<Dog>>(request,
                                CancellationToken.None);

            return doggy;

        }
    }
}

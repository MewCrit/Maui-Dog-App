using Newtonsoft.Json;

namespace dog_service.Models;

public class DogParameter
{
  
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("picture")]
    public string Picture { get; set; }

    [JsonProperty("breed")]
    public string Breed { get; set; }

    [JsonProperty("about")]
    public string About { get; set; }

    [JsonProperty("gender")]
    public string Gender { get; set; }
    
    [JsonProperty("birthday")]
    public DateTime Birthday { get; set; }
}
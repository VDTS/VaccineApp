using Newtonsoft.Json;

namespace Core.Models;

public class IdTokenModel
{
    [JsonProperty("idToken")]
    public string? IdToken { get; set; }
}

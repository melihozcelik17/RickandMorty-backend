

using System.Collections.Generic;
using Newtonsoft.Json;

public class ApiResponseModel
{
    [JsonProperty("results")]
    public List<EpisodeModel> Results { get; set; }
}

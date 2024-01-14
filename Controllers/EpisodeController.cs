using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Konusarak_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public EpisodeController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public async Task<IActionResult> GetEpisodes()
        {
            var apiEndpoint = "https://rickandmortyapi.com/api/episode";
            var response = await _httpClient.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var episodesJson = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponseModel>(episodesJson);
                return Ok(apiResponse.Results);
            }

            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}

using System.Net.Http;
using Newtonsoft.Json;

namespace Konusarak_backend.services
{
    public class CharacterService
    {
        private readonly IHttpClientFactory _httpClientFactory;


        public CharacterService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }
        public async Task<CharacterModel> GetCharacterById(int characterId)
        {
            // Implementasyon buraya eklenecek

            // Örnek olarak hata durumunda null döndürüldü.
            return null;
        }

        public async Task<List<CharacterModel>> GetCharacters()
        {
            var apiEndpoint = "https://rickandmortyapi.com/api/character/?page=1";
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var charactersJson = await response.Content.ReadAsStringAsync();
                var characters = JsonConvert.DeserializeObject<CharacterListResponse>(charactersJson);

                return characters.results;
            }

            // Hata durumu için gerekli işlemler
            return null;
        }
    }
}

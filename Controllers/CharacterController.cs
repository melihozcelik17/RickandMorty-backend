using Microsoft.AspNetCore.Mvc;
using Konusarak_backend.services; // Eksik olan using direktifi

namespace Konusarak_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly CharacterService _characterService;

        public CharacterController(CharacterService characterService)
        {
            _characterService = characterService ?? throw new ArgumentNullException(nameof(characterService));
        }

        [HttpGet]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _characterService.GetCharacters();

            if (characters != null)
                return Ok(characters);

            return StatusCode(500, "Internal Server Error");
        }

        [HttpGet("{characterId}")]
        public async Task<IActionResult> GetCharacterById(int characterId)
        {
            try
            {
                var character = await _characterService.GetCharacterById(characterId);

                if (character != null)
                    return Ok(character);

                return NotFound(); // Karakter bulunamadıysa 404 döndür
            }
            catch (Exception ex)
            {
                // Hata loglaması yapabilirsiniz
                Console.Error.WriteLine(ex);
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}

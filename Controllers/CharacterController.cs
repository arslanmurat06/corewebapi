using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using advancedwebapi.DTOs;
using advancedwebapi.Models;
using advancedwebapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace advancedwebapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        public async Task<IActionResult> GetAll()
        {

            return Ok(await _characterService.GetAllCharacters());
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {

            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(CharacterDTO newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(CharacterDTO updatedCharacter)
        {

            ServiceResponse<CharacterDTO> response = await _characterService.UpdateCharacter(updatedCharacter);

            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCharacter(int id){

            ServiceResponse<bool> response = await _characterService.RemoveCharacter(id);

            if(response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
    }
}
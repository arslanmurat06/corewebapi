using System.Collections.Generic;
using System.Threading.Tasks;
using advancedwebapi.DTOs;
using advancedwebapi.Models;

namespace advancedwebapi.Services
{
    public interface ICharacterService
    {
         Task<ServiceResponse<List<CharacterDTO>>> GetAllCharacters(int userID);
         Task<ServiceResponse<CharacterDTO>> GetCharacterById(int id);
         Task<ServiceResponse<List<CharacterDTO>>> AddCharacter(CharacterDTO newCharacter);
         Task<ServiceResponse<CharacterDTO>> UpdateCharacter(CharacterDTO updatedCharacter);
         Task<ServiceResponse<bool>> RemoveCharacter(int id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using advancedwebapi.DTOs;
using advancedwebapi.Models;
using AutoMapper;

namespace advancedwebapi.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;

        private List<Character> characterRepo = new List<Character>()
        {
            new Character{Name = "Murat"},
        };

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }


        public async Task<ServiceResponse<List<CharacterDTO>>> AddCharacter(CharacterDTO newCharacter)
        {
            var newCharacterEntity = _mapper.Map<Character>(newCharacter);
            newCharacterEntity.Id = characterRepo.Max(a => a.Id) + 1;
            characterRepo.Add(newCharacterEntity);

            ServiceResponse<List<CharacterDTO>> response = new ServiceResponse<List<CharacterDTO>>();

            response.Data = characterRepo.Select(c => _mapper.Map<CharacterDTO>(c)).ToList();
            response.DataLength = characterRepo.Count();

            return response;
        }

        public async Task<ServiceResponse<List<CharacterDTO>>> GetAllCharacters()
        {
            ServiceResponse<List<CharacterDTO>> response = new ServiceResponse<List<CharacterDTO>>();
            response.Data = characterRepo.Select(c => _mapper.Map<CharacterDTO>(c)).ToList();
            response.DataLength = characterRepo.Count();
            return response;
        }

        public async Task<ServiceResponse<CharacterDTO>> GetCharacterById(int id)
        {
            ServiceResponse<CharacterDTO> response = new ServiceResponse<CharacterDTO>();
            response.Data = _mapper.Map<CharacterDTO>(characterRepo.FirstOrDefault(c => c.Id == id));

            return response;

        }

        public async Task<ServiceResponse<bool>> RemoveCharacter(int id)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                var deletedCharacter = characterRepo.First(c => c.Id == id);
                characterRepo.Remove(deletedCharacter);

                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            return response;
        }

        public async Task<ServiceResponse<CharacterDTO>> UpdateCharacter(CharacterDTO updatedCharacter)
        {
            ServiceResponse<CharacterDTO> response = new ServiceResponse<CharacterDTO>();
            try
            {
                var willUpdatecharacter = characterRepo.First(c => c.Id == updatedCharacter.Id);
                willUpdatecharacter.Defense = updatedCharacter.Defense == default(int) ? willUpdatecharacter.Defense : updatedCharacter.Defense;
                willUpdatecharacter.HitPoints = updatedCharacter.HitPoints == default(int) ? willUpdatecharacter.HitPoints : updatedCharacter.HitPoints;
                willUpdatecharacter.Intelligence = updatedCharacter.Intelligence == default(int) ? willUpdatecharacter.Intelligence : updatedCharacter.Intelligence;
                willUpdatecharacter.Name = updatedCharacter.Name == default(string) ? willUpdatecharacter.Name : updatedCharacter.Name;
                willUpdatecharacter.Strength = updatedCharacter.Strength == default(int) ? willUpdatecharacter.Strength : updatedCharacter.Strength;

                response.Data = _mapper.Map<CharacterDTO>(willUpdatecharacter);

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }

            return response;
        }
    }
}
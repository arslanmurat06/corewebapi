using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using advancedwebapi.Context;
using advancedwebapi.DTOs;
using advancedwebapi.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace advancedwebapi.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context =context;
        }


        public async Task<ServiceResponse<List<CharacterDTO>>> AddCharacter(CharacterDTO newCharacter)
        {
            var newCharacterEntity = _mapper.Map<Character>(newCharacter);
            // newCharacterEntity.Id = characterRepo.Max(a => a.Id) + 1;
            // characterRepo.Add(newCharacterEntity);

            await _context.Characters.AddAsync(newCharacterEntity);
            await _context.SaveChangesAsync();

            ServiceResponse<List<CharacterDTO>> response = new ServiceResponse<List<CharacterDTO>>();

            response.Data = _context.Characters.Select(c => _mapper.Map<CharacterDTO>(c)).ToList();
            response.DataLength = _context.Characters.Count();

            return response;
        }

        public async Task<ServiceResponse<List<CharacterDTO>>> GetAllCharacters()
        {
            List<Character> dbCharacters = await _context.Characters.ToListAsync();
            ServiceResponse<List<CharacterDTO>> response = new ServiceResponse<List<CharacterDTO>>();
            response.Data = dbCharacters.Select(c => _mapper.Map<CharacterDTO>(c)).ToList();
            response.DataLength = dbCharacters.Count();
            return response;
        }

        public async Task<ServiceResponse<CharacterDTO>> GetCharacterById(int id)
        {
            ServiceResponse<CharacterDTO> response = new ServiceResponse<CharacterDTO>();
            response.Data = _mapper.Map<CharacterDTO>(await _context.Characters.FirstOrDefaultAsync(c => c.Id == id));

            return response;
        }

        public async Task<ServiceResponse<bool>> RemoveCharacter(int id)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            try
            {
                var deletedCharacter = _context.Characters.First(c => c.Id == id);
                // characterRepo.Remove(deletedCharacter);
                _context.Characters.Remove(deletedCharacter);

                await _context.SaveChangesAsync();

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
                var willUpdatecharacter = await _context.Characters.FirstAsync(c => c.Id == updatedCharacter.Id);
                willUpdatecharacter.Defense = updatedCharacter.Defense == default(int) ? willUpdatecharacter.Defense : updatedCharacter.Defense;
                willUpdatecharacter.HitPoints = updatedCharacter.HitPoints == default(int) ? willUpdatecharacter.HitPoints : updatedCharacter.HitPoints;
                willUpdatecharacter.Intelligence = updatedCharacter.Intelligence == default(int) ? willUpdatecharacter.Intelligence : updatedCharacter.Intelligence;
                willUpdatecharacter.Name = updatedCharacter.Name == default(string) ? willUpdatecharacter.Name : updatedCharacter.Name;
                willUpdatecharacter.Strength = updatedCharacter.Strength == default(int) ? willUpdatecharacter.Strength : updatedCharacter.Strength;

                _context.Characters.Update(willUpdatecharacter);
                await _context.SaveChangesAsync();
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
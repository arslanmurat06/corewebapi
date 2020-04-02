using advancedwebapi.DTOs;
using advancedwebapi.Models;
using AutoMapper;

namespace advancedwebapi.MapperProfile
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, CharacterDTO>();
            CreateMap<CharacterDTO,Character>();
        }
    }
}
using System.Linq;
using AutoMapper;
using Tinderro.API.Dtos;
using Tinderro.API.Models;

namespace Tinderro.API.Helpers
{
    public class AutoMapperProfiles : Profile // profile to cos z automappera
    {
        public AutoMapperProfiles()
        {
            //ponizsza funkcja ustawia url zdjecia:
            // destination - tam gdzie ma byc docelowo zmapowane cos, src - source/ zrodlo czyli skad bierzemy dane
            // i ta koniec pobieramu url
            CreateMap<User, UserForListDto>()
                .ForMember(destination => destination.PhotoUrl, option => {
                    option.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url);
                })
                .ForMember(destination => destination.Age, option => {
                    option.MapFrom(src => src.DateOfBirth.CalculateAge()); // src.DateOfBirth.CalculateAge()); trzeba calculateAge wywolac na DateOfBirth
                });
            CreateMap<User, UserForDetailedDto>()
                .ForMember(destination => destination.PhotoUrl, option => {
                    option.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url);
                })
                .ForMember(destination => destination.Age, option => {
                    option.MapFrom(src => src.DateOfBirth.CalculateAge());
                });
            CreateMap<Photo, PhotoFiorDetailedDto>(); // to ejst to nowe mapowanie xd
        }
    }
}
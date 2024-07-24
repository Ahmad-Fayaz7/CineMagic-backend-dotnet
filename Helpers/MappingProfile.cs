using AutoMapper;
using CineMagic.DTOs;
using CineMagic.Models;
using NetTopologySuite.Geometries;

namespace CineMagic.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile(GeometryFactory geometryFactory)
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>().ReverseMap();

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>().ForMember(x => x.picture, options => options.Ignore());

            CreateMap<MovieTheater, MovieTheaterDTO>()
                .ForMember(x => x.latitude, dto => dto.MapFrom(prop => prop.Location.Y))
                .ForMember(x => x.longitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<MovieTheaterCreationDTO, MovieTheater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.longitude, dto.latitude))));
        }
    }
}

using AutoMapper;
using Models.DTOs;
using Models.Entidades;

namespace Utilidades
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ============================
            // CATEGORIA
            // ============================
            CreateMap<Categoria, CategoriaDto>().ReverseMap();

            // ============================
            // CANCION
            // ============================
            CreateMap<Cancion, CancionDto>().ReverseMap();
            CreateMap<CrearCancionDto, Cancion>();

            // ============================
            // DEVOCIONAL
            // ============================
            CreateMap<Devocional, DevocionalDto>().ReverseMap();

            // ============================
            // DEVOCIONAL - CANCION
            // ============================
            CreateMap<DevocionalCancion, DevocionalCancionDto>().ReverseMap();
            CreateMap<DevocionalCancion, DevocionalCancionDetalleDto>();
        }
    }
}

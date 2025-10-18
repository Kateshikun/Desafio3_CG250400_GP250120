using AutoMapper;
using SupermercadoAPI.Dtos;
using SupermercadoAPI.Models;

namespace SupermercadoAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductoCreacionDto, Productos>();
            CreateMap<Productos, ProductoDto>();
            CreateMap<ProductoActualizacionDto, Productos>();
            CreateMap<CategoriaDto, Categorias>();
            CreateMap<Inventarios, InventarioDto>();
            CreateMap<Productos, InventarioDto>()
                .ForMember(
                dest => dest.CantidadActual,
                opt => opt.MapFrom(src => src.Inventario.CantidadActual));
            CreateMap<InventarioDto, Inventarios>();
            CreateMap<Usuarios, AuthResponseDto>();
            CreateMap<RegistroUsuarioDto, Usuarios>();
        }
    }
}

using AutoMapper;
using SupermercadoAPI.Dtos;
using SupermercadoAPI.Models;

namespace SupermercadoAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductoCreacionDto, Productos>();          // POST: DTO -> Modelo
            CreateMap<Productos, ProductoDto>();                  // GET, POST: Modelo -> Response DTO (
            CreateMap<ProductoActualizacionDto, Productos>();     // PUT: DTO -> Modelo (Para actualizar)

            // --- Mapeos para AUTENTICACIÓN ---
            CreateMap<Usuarios, AuthResponseDto>();
            CreateMap<RegistroUsuarioDto, Usuarios>();

            // --- Mapeos para INVENTARIO ---
            CreateMap<Inventarios, InventarioDto>();              
            CreateMap<InventarioDto, Inventarios>();              

            CreateMap<CrearInventarioDto, Inventarios>();         
            CreateMap<InventarioActualizacionDto, Inventarios>(); 
            CreateMap<Productos, InventarioDto>()
                .ForMember( // Para combinar propiedades de dos entidades en un solo DTO
                dest => dest.CantidadActual,
                opt => opt.MapFrom(src => src.Inventario.CantidadActual));

            // Mapeo para Categoría 
            CreateMap<CategoriaDto, Categorias>();
            CreateMap<Categorias, CategoriaDto>();
        }
    }
}

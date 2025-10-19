using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermercadoAPI.Data;
using SupermercadoAPI.Dtos;
using SupermercadoAPI.Models;
using AutoMapper;

namespace SupermercadoAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InventarioController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioDto>>> ObtenerTodo()
        {
            var inventario = await _context.Inventarios
                .Include(i => i.producto) //Incluiremos la informacion del producto
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<InventarioDto>>(inventario));
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermercadoAPI.Data;
using SupermercadoAPI.Dtos;
using SupermercadoAPI.Models;
using AutoMapper;

namespace SupermercadoAPI.Controllers
{
    [Authorize (Roles = "Administrador")] //Roles esta definido en el token JWT
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : Controller //Solo se puede acceder si el usuario tiene el rol de Administrador
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductosController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCreacionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = _mapper.Map<Productos>(dto);

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            // Mapea el producto creado para devolverlo en la respuesta
            var responseDto = _mapper.Map<ProductoDto>(producto);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.idProducto }, responseDto);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null){
                return NotFound();
            }

           return Ok(_mapper.Map<ProductoDto>(producto));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Modificar(int id, [FromBody] ProductoActualizacionDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(p => p.idProducto == id);

            if (producto == null)
            {
                return NotFound(new { Message = $"Producto con ID {id} no encontrado." });
            }

            _mapper.Map(dto, producto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.idProducto == id))
                {
                    return NotFound(); //Error 404 si el producto no existe
                }
                else
                {
                    throw; //Re-lanzar la excepción si es otro tipo de error
                }
            }

            // Se retorna una Action Result sin contenido
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _context.Productos.FindAsync(id);

            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent(); // 204 No Content
        }
    }
}

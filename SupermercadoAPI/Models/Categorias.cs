using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SupermercadoAPI.Models;

public class Categorias
{
    [Key]
    public int idCategoria { get; set; }
    [MaxLength(100)]
    public string nombre { get; set; }
    [MaxLength(300)]
    public string descripcion { get; set; }

    public ICollection<Productos> productos { get; set; } = new List<Productos>();

    public Categorias() { }

    public Categorias(int idCategoria, string nombre, string descripcion, ICollection<Productos> productos)
    {
        this.idCategoria = idCategoria;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.productos = productos;
    }
}

namespace SupermercadoAPI.Dtos
{
    public class InventarioDto //Esta clase muestra el estado del stock
                               //Es util para que cuando un usuario haga un metodo get del inventario, vea los productos con su respectivo stock
    {
        public int idProducto { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public int CantidadActual { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }
}

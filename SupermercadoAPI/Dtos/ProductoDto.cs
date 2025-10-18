namespace SupermercadoAPI.Dtos
{
    public class ProductoDto //Clase de salida para los productos (get)
                              //Esta clase se utiliza para transferir datos de productos entre el servidor y el cliente
    {
        public int idProducto { get; set; }
        public string nombre { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int idCategoria { get; set; }
    }
}

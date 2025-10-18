namespace SupermercadoAPI.Dtos
{
    public class ProductoActualizacionDto //Clase para la actualización de un producto 
                                  //Se le daran los mismos atributos que la clase Producto excepto el IdProducto, ya que este no se puede modificar
                                  //Sirve para que el usuario solo vea los atributos necesarios para el PUT
    {
        public int IdProducto { get; set; }
        public string nombre { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int idCategoria { get; set; }
    }
}

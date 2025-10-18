namespace SupermercadoAPI.Dtos
{
    public class ProductoCreacionDto //Clase para la creación de un nuevo producto 
                                  //Se le daran los mismos atributos que la clase Producto excepto el IdProducto, ya que este se genera automáticamente en la base de datos
                                  //Sirve para que el usuario solo vea los atributos necesarios para el POST
    {
        public string nombre { get; set; } = string.Empty;
        public decimal precio { get; set; }
        public int idCategoria { get; set; }
    }
}

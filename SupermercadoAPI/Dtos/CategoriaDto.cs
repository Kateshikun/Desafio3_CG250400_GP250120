namespace SupermercadoAPI.Dtos
{
    public class CategoriaDto //Clase de salida para las categorias (get)
                              //Esta clase se utiliza para transferir datos de categorias entre el servidor y el cliente
                              //Sirve para que el usuario vea los atributos necesarios de una categoria (output)
    {
        public int idCategoria { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
    }
}

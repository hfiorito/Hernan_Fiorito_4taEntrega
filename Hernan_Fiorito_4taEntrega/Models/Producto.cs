namespace Hernan_Fiorito_4taEntrega.Models
{
    public class Producto
    {
        public long Id { get; set; }
        public string Descripcion { get; set; }
        public float Costo { get; set; }
        public float PrecioVenta { get; set; }
        public int Stock { get; set; }
        public long IdUsuario { get; set; }


        public Producto()
        {
            Id = 0;
            Descripcion = "";
            Costo = 0;
            PrecioVenta = 0;
            Stock = 0;
            IdUsuario = 0;
        }

        public Producto(long codigo, string descripcion, float precioCompra, float precioVenta, int stock, long idUsuario)
        {
            Id = codigo;
            Descripcion = descripcion;
            Costo = precioCompra;
            PrecioVenta = precioVenta;
            Stock = stock;
            IdUsuario = idUsuario;
        }
    }
}

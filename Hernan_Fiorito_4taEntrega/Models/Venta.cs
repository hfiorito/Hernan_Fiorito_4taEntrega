namespace Hernan_Fiorito_4taEntrega.Models
{
    public class Venta
    {
        public long id { get; set; }
        public string comentarios { get; set; }
        public long idUsuario { get; set; }
        public List<ProductoVendido> ProductoV { get; set; }

        public Venta()
        {
            id = 0;
            comentarios = "";
            idUsuario = 0;

        }

        public Venta(long id, string comentarios, long idUsuario)
        {
            this.id = id;
            this.comentarios = comentarios;
            this.idUsuario = idUsuario;
        }


        public override string ToString()
        {
            return id + " " + comentarios;
        }
    }
}

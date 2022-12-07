namespace Hernan_Fiorito_4taEntrega.Models
{
    public class Usuario
    {
        public long id { get; set; }
        public string nombre { get; set; }

        public string apellido { get; set; }
        public string nombreUsuario { get; set; }
        public string contrasenia { get; set; }
        public string mail { get; set; }

        public Usuario(long id, string nombre, string apellido, string nombreUsuario, string contrasenia, string mail)
        {
            this.id = id;
            this.nombre = nombre;
            this.apellido = apellido;
            this.nombreUsuario = nombreUsuario;
            this.contrasenia = contrasenia;
            this.mail = mail;

        }

        public Usuario()
        {
            id = 0;
            nombre = "";
            apellido = "";
            nombreUsuario = "";
            contrasenia = "";
            mail = "";
        }

        public override string ToString()
        {
            return nombre;
        }
    }
}

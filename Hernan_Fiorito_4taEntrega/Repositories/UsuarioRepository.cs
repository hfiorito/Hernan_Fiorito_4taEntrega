using Hernan_Fiorito_4taEntrega.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.ComponentModel;


namespace Hernan_Fiorito_4taEntrega.Repositories

{
    public class UsuarioRepository
    {
        private SqlConnection? conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;Database=harry9110_sistemagestion;User Id=harry9110_sistemagestion;Password=Emma9110..9110;";

        public UsuarioRepository()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public List<Usuario> ListaUsuarios()
        {
            List<Usuario> listaU = new List<Usuario>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuario", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = obtenerUsuarioDesdeReader(reader);
                                listaU.Add(usuario);
                            }
                        }

                    }

                }
                
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }

            return listaU;
        }

        public Usuario? obtenerUsuario(long id) 
        { 
            if(conexion == null)
            {
                throw new Exception("Conexión no establecida");

            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using(SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = obtenerUsuarioDesdeReader(reader);
                            return usuario;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public Usuario? modificaUsuario(int id, Usuario usuarioAModificar)
        {
            if(conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Usuario? usuario = obtenerUsuario(id);
                if (usuario == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if(usuario.nombre != usuarioAModificar.nombre && !string.IsNullOrEmpty(usuarioAModificar.nombre))
                {
                    camposAActualizar.Add("nombre = @nombre");
                    usuario.nombre = usuarioAModificar.nombre;
                }
                if(usuario.apellido != usuarioAModificar.apellido && !string.IsNullOrEmpty(usuarioAModificar.apellido))
                {
                    camposAActualizar.Add("apellido = @apellido");
                    usuario.apellido = usuarioAModificar.apellido;
                }
                if(usuario.nombreUsuario != usuarioAModificar.nombreUsuario && !string.IsNullOrEmpty(usuarioAModificar.nombreUsuario))
                {
                    camposAActualizar.Add("nombreUsuario = @nombreUsuario");
                    usuario.nombreUsuario = usuarioAModificar.nombreUsuario;
                }
                if(usuario.contrasenia != usuarioAModificar.contrasenia && !string.IsNullOrEmpty(usuarioAModificar.contrasenia))
                {
                    camposAActualizar.Add("contraseña = @contraseña");
                    usuario.contrasenia = usuarioAModificar.contrasenia;
                }
                if(usuario.mail != usuarioAModificar.mail && !string.IsNullOrEmpty(usuarioAModificar.mail))
                {
                    camposAActualizar.Add("mail = @mail");
                    usuario.mail = usuarioAModificar.mail;
                }
                if(camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using(SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ",camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuarioAModificar.nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuarioAModificar.apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuarioAModificar.nombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuarioAModificar.contrasenia });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuarioAModificar.mail });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return usuario;

                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }
        private Usuario obtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.id = long.Parse(reader["Id"].ToString());
            usuario.nombre = reader["Nombre"].ToString();
            usuario.apellido = reader["Apellido"].ToString();
            usuario.nombreUsuario = reader["NombreUsuario"].ToString();
            usuario.contrasenia = reader["Contraseña"].ToString();
            usuario.mail = reader["Mail"].ToString();
            return usuario;
        }
    }
}

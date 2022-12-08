using Hernan_Fiorito_4taEntrega.Models;
using System.Data;
using System.Data.SqlClient;

namespace Hernan_Fiorito_4taEntrega.Repositories
{
    public class VentasRepository
    {
        private SqlConnection? conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;Database=harry9110_sistemagestion;User Id=harry9110_sistemagestion;Password=Emma9110..9110;";

        public VentasRepository()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public List<Venta> listaVentas()
        {
            List<Venta> lista = new List<Venta>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta ventas = new Venta();
                                ventas.id = long.Parse(reader["Id"].ToString());
                                ventas.comentarios = reader["Comentarios"].ToString();
                                ventas.idUsuario = int.Parse(reader["IdUsuario"].ToString());
                                lista.Add(ventas);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            return lista;
        }

        public Venta nuevaVenta(Venta venta)
        {
            if(conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using(SqlCommand cmd = new SqlCommand("INSERT INTO Venta (Comentarios, IdUsuario) VALUES (@comentarios, @idUsuario); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = venta.comentarios });
                    cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.BigInt) { Value = venta.idUsuario });
                    venta.id = long.Parse(cmd.ExecuteScalar().ToString());
                    return venta;
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


        public Venta? actualizarVenta(long id, Venta ventaAActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Venta? venta = obtenerVenta(id);
                if (venta == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (venta.comentarios != ventaAActualizar.comentarios )
                {
                    camposAActualizar.Add("Comentarios = @comentarios");
                    venta.comentarios = ventaAActualizar.comentarios;
                }
                if (venta.idUsuario != ventaAActualizar.idUsuario && ventaAActualizar.idUsuario > 0)
                {
                    camposAActualizar.Add("IdUsuario = @idUsuario");
                    venta.idUsuario = ventaAActualizar.idUsuario;
                }
               
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Venta SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    cmd.Parameters.Add(new SqlParameter("Comentarios", SqlDbType.VarChar) { Value = ventaAActualizar.comentarios });
                    cmd.Parameters.Add(new SqlParameter("IdUsuario", SqlDbType.Int) { Value = ventaAActualizar.idUsuario });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return venta;
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


        public bool eliminarVenta(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand mcd = new SqlCommand("DELETE FROM Venta WHERE id = @id", conexion))
                {
                    conexion.Open();
                    mcd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    filasAfectadas = mcd.ExecuteNonQuery();
                }
                conexion.Close();
                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
        }

        //Obtener venta por id
        public Venta obtenerVenta(long id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Venta venta = ObtenerVentaDesdeReader(reader);
                            return venta;
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
        private Venta ObtenerVentaDesdeReader(SqlDataReader reader)
        {

            Venta venta = new Venta();
            venta.id = long.Parse(reader["Id"].ToString());
            venta.comentarios = reader["Comentarios"].ToString();
            venta.idUsuario = int.Parse(reader["IdUsuario"].ToString());
            return venta;
        }
    }
}

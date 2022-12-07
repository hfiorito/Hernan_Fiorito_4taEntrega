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
    }
}

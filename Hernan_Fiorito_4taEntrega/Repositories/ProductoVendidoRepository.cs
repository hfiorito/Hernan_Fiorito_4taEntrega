using Hernan_Fiorito_4taEntrega.Models;
using System.Data.SqlClient;
using System.Data;



namespace Hernan_Fiorito_4taEntrega.Repositories
{
    public class ProductoVendidoRepository
    {
        private SqlConnection? conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;Database=harry9110_sistemagestion;User Id=harry9110_sistemagestion;Password=Emma9110..9110;";

        public ProductoVendidoRepository()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {

            }
        }

        public List<ProductoVendido> listaProductosVendidos()
        {
            List<ProductoVendido> listaPV = new List<ProductoVendido>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido pv = new ProductoVendido();
                                pv.id = long.Parse(reader["Id"].ToString());
                                pv.idProducto = int.Parse(reader["IdProducto"].ToString());
                                pv.stock = int.Parse(reader["Stock"].ToString());
                                pv.idVenta = int.Parse(reader["IdVenta"].ToString());
                                listaPV.Add(pv);
                            }
                        }
                    }
                }
                conexion.Close();
            }

            catch
            {
                throw;
            }


            return listaPV;
        }


        public ProductoVendido cargarProductoVendido(ProductoVendido prodV)
        {
            if(conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using(SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido (Stock,IdProducto,IdVenta) VALUES (@stock,@idProducto,@idVenta); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Stock", SqlDbType.Int) { Value = prodV.stock });
                    cmd.Parameters.Add(new SqlParameter("IdProducto", SqlDbType.Int) { Value = prodV.idProducto });
                    cmd.Parameters.Add(new SqlParameter("IdVenta", SqlDbType.BigInt) { Value = prodV.idVenta });
                    prodV.id = long.Parse(cmd.ExecuteScalar().ToString());
                    return prodV;
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

        private ProductoVendido obtenerPrductoVendidoDesdeReader(SqlDataReader reader)
        {
            ProductoVendido prodV = new ProductoVendido();
            prodV.id= long.Parse(reader["Id"].ToString());
            prodV.idProducto = int.Parse(reader["IdProducto"].ToString());
            prodV.stock = int.Parse(reader["Stock"].ToString());
            prodV.idVenta = long.Parse(reader["IdVenta"].ToString());
            return prodV;
        }
    }
}

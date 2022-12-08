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

        //Obtener producto vendido por id
        public ProductoVendido obtenerProductoVendido(long id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido WHERE id = @id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ProductoVendido productoVendido = obtenerPrductoVendidoDesdeReader(reader);
                            return productoVendido;
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

        // obtener producto vendido desde id

        public ProductoVendido? actualizarProductoVendido(long id, ProductoVendido productoVActualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                ProductoVendido? producto = obtenerProductoVendido(id);
                if (producto == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (producto.idProducto != productoVActualizar.idProducto && productoVActualizar.idProducto > 0)
                {
                    camposAActualizar.Add("idProducto = @idProducto");
                    producto.idProducto = productoVActualizar.idProducto;
                }
                if (producto.stock != productoVActualizar.stock && productoVActualizar.stock > 0)
                {
                    camposAActualizar.Add(" stock = @stock");
                    producto.stock = productoVActualizar.stock;
                }
                if (producto.idVenta != productoVActualizar.idVenta && productoVActualizar.idVenta > 0)
                {
                    camposAActualizar.Add("idVenta = @idVenta");
                    producto.idVenta = productoVActualizar.idVenta;
                }
               
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE ProductoVendido SET {String.Join(", ", camposAActualizar)} WHERE id = @id", conexion))
                {
                    
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.Int) { Value = productoVActualizar.idProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.BigInt) { Value = productoVActualizar.idVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVActualizar.stock });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    conexion.Open();
                    cmd.ExecuteNonQuery();
                    return producto;
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


        public bool eliminarProductoVendido(int id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand mcd = new SqlCommand("DELETE FROM ProductoVendido WHERE id = @id", conexion))
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

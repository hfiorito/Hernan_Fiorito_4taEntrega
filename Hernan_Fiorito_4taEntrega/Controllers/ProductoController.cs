using Hernan_Fiorito_4taEntrega.Models;
using Microsoft.AspNetCore.Mvc;
using Hernan_Fiorito_4taEntrega.Repositories;

namespace Hernan_Fiorito_4taEntrega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : Controller
    {
        private ProductosRepository repository = new ProductosRepository();
        [HttpGet]//listo productos
        public ActionResult<List<Producto>> Get()
        {
            try
            {
                List<Producto> lista = repository.listarProductos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]//Modificar producto por id

        public ActionResult<Producto> Put(long id, [FromBody] Producto prductoAActualizar)
        {
            try
            {
                Producto? productoActualizado = repository.actualizarProducto(id, prductoAActualizar);

                return Ok(productoActualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]//creo producto
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                Producto productoCreado = repository.crearProducto(producto);
                return StatusCode(StatusCodes.Status201Created, productoCreado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]//borro producto

        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool seElimino = repository.eliminarProducto(id);
                if (seElimino)
                {
                    return Ok();            
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}

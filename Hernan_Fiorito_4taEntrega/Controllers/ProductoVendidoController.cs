using Hernan_Fiorito_4taEntrega.Models;
using Microsoft.AspNetCore.Mvc;
using Hernan_Fiorito_4taEntrega.Repositories;

namespace Hernan_Fiorito_4taEntrega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoVendidoController : Controller
    {
        private ProductoVendidoRepository repository = new ProductoVendidoRepository();
        [HttpGet]

        public ActionResult<List<ProductoVendido>> Get()
        {
            try
            {
                List<ProductoVendido> lista = repository.listaProductosVendidos();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        [HttpPost]

        public ActionResult Post([FromBody] ProductoVendido Prodv)
        {
            try
            {
                ProductoVendido productoVendido = repository.cargarProductoVendido(Prodv);
                return StatusCode(StatusCodes.Status201Created, productoVendido);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

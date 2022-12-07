using Hernan_Fiorito_4taEntrega.Models;
using Microsoft.AspNetCore.Mvc;
using Hernan_Fiorito_4taEntrega.Repositories;

namespace Hernan_Fiorito_4taEntrega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : Controller
    {
        private VentasRepository repository = new VentasRepository();
        [HttpGet]

        public ActionResult<List<Venta>> Get()
        {
            try
            {
                List<Venta> lista = repository.listaVentas();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]//cargo una venta

        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                Venta newVenta = repository.nuevaVenta(venta);
                return Ok();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}

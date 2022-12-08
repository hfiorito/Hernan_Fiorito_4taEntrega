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

        [HttpPut("{id}")]//Modificar venta por id

        public ActionResult<Venta> Put(long id, [FromBody] Venta ventaAActualizar)
        {
            try
            {
                Venta? ventaActualzizada = repository.actualizarVenta(id, ventaAActualizar);

                return Ok(ventaActualzizada);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }




        [HttpDelete]//borro venta

        public ActionResult Delete([FromBody] int id)
        {
            try
            {
                bool seElimino = repository.eliminarVenta(id);
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

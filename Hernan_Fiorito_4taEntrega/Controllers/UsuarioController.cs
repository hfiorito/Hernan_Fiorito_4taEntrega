using Hernan_Fiorito_4taEntrega.Models;
using Microsoft.AspNetCore.Mvc;
using Hernan_Fiorito_4taEntrega.Repositories;


namespace Hernan_Fiorito_4taEntrega.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private UsuarioRepository repository = new UsuarioRepository();
        [HttpGet]
        public ActionResult<List<Usuario>> Get()
        {
            try
            {
                List<Usuario> lista = repository.ListaUsuarios();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]// Modifico usuario desde id

        public ActionResult<Usuario> Put(int id, [FromBody]Usuario usuarioAModificar)

        {
            try
            {
                Usuario? usuarioActualizado = repository.modificaUsuario(id, usuarioAModificar);
                return Ok(usuarioActualizado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}

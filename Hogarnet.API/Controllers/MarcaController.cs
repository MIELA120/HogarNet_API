using Microsoft.AspNetCore.Mvc;
using Hogarnet.API.Utilidad;
using Hogarnet.NEG.Servicios.Contrato;
using Hogarnet.DTO;

namespace Hogarnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet]
        [Route("Lista")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<MarcaDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _marcaService.Lista();
            }
            catch (Exception ex)
            {

                rsp.Status = false;
                rsp.Msg = ex.Message;
            }

            return Ok(rsp);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Hogarnet.API.Utilidad;
using Hogarnet.NEG.Servicios.Contrato;
using Hogarnet.DTO;

namespace Hogarnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("Lista")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Lista(int idUsuario)
        {
            var rsp = new Response<List<MenuDTO>>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _menuService.Lista(idUsuario);
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

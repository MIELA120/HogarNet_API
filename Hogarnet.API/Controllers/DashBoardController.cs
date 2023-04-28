using Microsoft.AspNetCore.Mvc;
using Hogarnet.API.Utilidad;
using Hogarnet.NEG.Servicios.Contrato;
using Hogarnet.DTO;


namespace Hogarnet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class DashBoardController : ControllerBase
    {
        private readonly IDashBoardService _dashboardService;

        public DashBoardController(IDashBoardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("Resumen")]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashBoardDTO>();

            try
            {
                rsp.Status = true;
                rsp.Value = await _dashboardService.Resumen();
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

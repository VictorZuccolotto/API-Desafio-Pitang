using DesafioPitang.Business.Interface.IBusiness;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Model;
using Microsoft.AspNetCore.Mvc;


namespace DesafioPitang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteBusiness _pacienteBusiness;

        public PacienteController(IPacienteBusiness pacienteBusiness)
        {
            _pacienteBusiness = pacienteBusiness;
        }

        [HttpGet("agendamentos")]
        public async Task<ActionResult<List<AgendamentoDTO>>> Get(int pacienteId)
        {
            return await _pacienteBusiness.ListarAgentamentosDTOFromPacienteById(pacienteId);
        }

    }
}

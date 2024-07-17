using DesafioPitang.Business.Interface.IBusiness;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Model;
using DesafioPitang.Utils.Attributes;
using Microsoft.AspNetCore.Mvc;


namespace DesafioPitang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {

        private readonly IAgendamentoBusiness _agendamentoBusiness;

        public AgendamentoController(IAgendamentoBusiness agendamentoBusiness)
        {
            _agendamentoBusiness = agendamentoBusiness;
        }

        [HttpGet("horarios/{data}")]
        public async Task<List<HorarioDisponivelDTO>> GetHorarios([FromRoute] DateTime data)
        {
            return await _agendamentoBusiness.ListarHorariosDisponiveisByDia(data);
        }

        [HttpPost]
        [Transaction]
        public async Task<ActionResult<int>> Post([FromBody] CadastroAgendamentoModel agendamento)
        {
            return await _agendamentoBusiness.CadastrarAgendamento(agendamento);
        }

    }
}

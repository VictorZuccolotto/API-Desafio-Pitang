using Microsoft.AspNetCore.Mvc;


namespace DesafioPitang.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        [HttpGet]
        public void Get()
        {
        }

        [HttpGet("{id}")]
        public void Get(int id)
        {
        }

        [HttpPost]
        public void Post([FromBody] string agendamento)
        {
        }

    }
}

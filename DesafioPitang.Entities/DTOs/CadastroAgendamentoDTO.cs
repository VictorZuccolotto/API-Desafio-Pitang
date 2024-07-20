using DesafioPitang.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.DTOs
{
    public class CadastroAgendamentoDTO
    {
        public PacienteDTO Paciente { get; set; }
        public AgendamentoDTO Agendamento { get; set; }
    }
}

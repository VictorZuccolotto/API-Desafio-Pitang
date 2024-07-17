using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.Model
{
    public class CadastroAgendamentoModel
    {
        public PacienteModel Paciente { get; set; }
        public AgendamentoModel Agendamento { get; set; }
    }
}

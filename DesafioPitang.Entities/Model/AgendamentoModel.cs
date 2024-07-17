using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.Model
{
    public class AgendamentoModel
    {
        public int? PacienteId { get; set; }
        public DateTime DataAgendamento { get; set; }
        public TimeSpan HoraAgendamento { get; set; }
    }
}

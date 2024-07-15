using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.Entities
{
    public class Agendamento: IdEntity<int>
    {

        public int PacienteId { get; set; }
        public DateTime DataAgendamento { get; set; }
        public TimeSpan HoraAgendamento { get; set; }
        public string Status { get; set; }
        public DateTime DataCriacao { get; set; }

    }
}

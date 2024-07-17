using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.DTOs
{
    public class HorarioDisponivelDTO
    {
        public DateTime Data { get; set; }
        public TimeSpan Horario { get; set; }
        public bool Disponivel { get; set; }
        public int? QuantidadePacientes { get; set; }


    }
}

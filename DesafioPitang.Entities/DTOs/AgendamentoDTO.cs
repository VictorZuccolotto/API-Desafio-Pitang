using DesafioPitang.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.DTOs
{
    public class AgendamentoDTO
    {
        public AgendamentoDTO()
        {
            
        }
        public AgendamentoDTO(Agendamento agendamento)
        {
            Data = agendamento.DataAgendamento;
            Horario = agendamento.HoraAgendamento;
        }

        public DateTime Data { get; set; }

        public TimeSpan Horario { get; set; }

        public string Status { get; set; }

        public bool Realizado { get; set; }
    }
}

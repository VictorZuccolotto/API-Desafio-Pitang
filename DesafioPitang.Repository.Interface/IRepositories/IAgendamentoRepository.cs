using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Repository.Interface.IRepositories
{
    public interface IAgendamentoRepository : ICrudRepository<Agendamento>
    {

        Task<bool> IsDiaVago(DateTime dia);
        Task<bool> IsHorarioVagoByDia(TimeSpan horario, DateTime dia);
        Task<bool> IsPacienteByIdAgendadoByDiaAndHora(int pacienteId, DateTime dia, TimeSpan horario);
        Task<List<HorarioDisponivelDTO>> ListarHorariosByDia(DateTime dia);
        Task<List<Agendamento>> ListarAgentamentosFromPacienteById(int pacienteId);

    }
}

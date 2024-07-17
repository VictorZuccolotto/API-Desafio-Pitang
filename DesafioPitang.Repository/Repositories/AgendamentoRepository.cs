using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Repository.Repositories
{
    public class AgendamentoRepository : CrudRepository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(Context context) : base(context)
        {
        }

        public async Task<bool> IsDiaVago(DateTime dia)
        {
            var query = EntitySet.Where(a => a.DataAgendamento == dia);
            var quantidade = await query.CountAsync();
            return quantidade < 20;
        }

        public async Task<bool> IsHorarioVagoByDia(TimeSpan horario, DateTime dia)
        {
            var query = EntitySet.Where(a => a.DataAgendamento == dia && a.HoraAgendamento == horario);
            var quantidade = await query.CountAsync();
            return quantidade < 2;
        }

        public async Task<bool> IsPacienteByIdAgendadoByDiaAndHora(int pacienteId, DateTime dia, TimeSpan horario)
        {
            return await EntitySet
                            .AnyAsync(a => a.PacienteId == pacienteId && a.DataAgendamento == dia && a.HoraAgendamento == horario);
        }

        public async Task<List<Agendamento>> ListarAgendamentoByDia(DateTime dia)
        {
            var query = EntitySet.Where(a => a.DataAgendamento == dia);
            return await query.ToListAsync();
        }


        public async Task<List<Agendamento>> ListarAgentamentosFromPacienteById(int pacienteId)
        {
            var query = EntitySet.Where(a => a.PacienteId == pacienteId)
                                 .OrderBy(a => a.DataAgendamento)
                                 .ThenBy(a => a.HoraAgendamento);

            return await query.ToListAsync();
        }


        public async Task<List<HorarioDisponivelDTO>> ListarHorariosByDia(DateTime dia)
        {
            var agendamentos = await EntitySet.Where(a => a.DataAgendamento == dia).ToListAsync();

            var groupedAgendamentos = agendamentos.GroupBy(a => new { a.HoraAgendamento, a.DataAgendamento });
            return groupedAgendamentos.Select(g => new HorarioDisponivelDTO()
            {
                Data = g.Key.DataAgendamento,
                Horario = g.Key.HoraAgendamento,
                Disponivel = g.Count() < 2,
                QuantidadePacientes = g.Count()
            }).ToList();
        }
    }
}

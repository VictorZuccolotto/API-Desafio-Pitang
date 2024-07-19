using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Business.Interface.IBusiness
{
    public class PacienteBusiness : IPacienteBusiness
    {
        protected readonly IAgendamentoRepository _agendamentoRepository;
        protected readonly IPacienteRepository _pacienteRepository;
        public PacienteBusiness(IAgendamentoRepository agendamentoRepository, IPacienteRepository pacienteRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _pacienteRepository = pacienteRepository;
        }
        public async Task<List<AgendamentoDTO>> ListarAgentamentosDTOFromPacienteById(int pacienteId)
        {
            var agendamentos = await _agendamentoRepository.ListarAgentamentosFromPacienteById(pacienteId);
            return agendamentos.Select(x => new AgendamentoDTO(x)
            {
                Realizado = x.Status == "Realizado",
                Status = x.Status
            }).ToList();
        }
    }
}

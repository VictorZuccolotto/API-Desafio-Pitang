using DesafioPitang.Business.Interface.IBusiness;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Model;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Business.Business
{
    public class AgendamentoBusiness : IAgendamentoBusiness
    {
        protected readonly IAgendamentoRepository _agendamentoRepository;
        protected readonly IPacienteRepository _pacienteRepository;
        public AgendamentoBusiness(IAgendamentoRepository agendamentoRepository, IPacienteRepository pacienteRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _pacienteRepository = pacienteRepository;
        }

        public async Task<int> CadastrarAgendamento(CadastroAgendamentoModel agendamento)
        {
            //IF nao trouxe id
            int userId;
            if (agendamento.Paciente.Id == null)
            {
                //Criar paciente e retornar o ID
                var user = await _pacienteRepository.Add(new Paciente()
                {
                    Nome = agendamento.Paciente.Nome,
                    DataNascimento = agendamento.Paciente.DataNascimento,
                    DataCriacao = DateTime.Now
                });
                userId = user.Id;
            }
            //ELSE trouxe id
            else
            {
                //VALIDAÇÃO data de nascimento e nome
                userId = agendamento.Paciente.Id.Value;


                //Já não está agendado para esse horario e dia
                if (await _agendamentoRepository.IsPacienteByIdAgendadoByDiaAndHora(userId, agendamento.Agendamento.DataAgendamento.Date, agendamento.Agendamento.HoraAgendamento))
                {
                    throw new BadRequestException("Você já está agendado nesse horário");
                }

            }

            //Horario do dia vago
            if (!await _agendamentoRepository.IsHorarioVagoByDia(agendamento.Agendamento.HoraAgendamento, agendamento.Agendamento.DataAgendamento))
            {
                throw new BadRequestException("Horario lotado");
            }
            //Nao esgotou as 20 vagas do dia
            if(!await _agendamentoRepository.IsDiaVago(agendamento.Agendamento.DataAgendamento))
            {
                throw new BadRequestException("Acabaram as vagas desse dia");
            }


            await _agendamentoRepository.Add(new Agendamento()
            {
                PacienteId = userId,
                DataAgendamento = agendamento.Agendamento.DataAgendamento,
                HoraAgendamento = agendamento.Agendamento.HoraAgendamento,
                DataCriacao = DateTime.Now,
                Status = "Pendente"
            });

            return userId;
        }

        public async Task<List<HorarioDisponivelDTO>> ListarHorariosDisponiveisByDia(DateTime dia)
        {
            var horarios = await _agendamentoRepository.ListarHorariosByDia(dia.Date);

            var todosHorarios = Enumerable.Range(6, 19).Select(h => new TimeSpan(h, 0, 0)).ToList();
            foreach (var horario in todosHorarios)
            {
                if (!horarios.Any(r => r.Horario == horario))
                {
                    horarios.Add(new HorarioDisponivelDTO
                    {
                        Data = dia,
                        Horario = horario,
                        Disponivel = true,
                        QuantidadePacientes = 0
                    });
                }
            }
            return horarios.OrderBy(r => r.Horario).ToList();
        }
    }
}

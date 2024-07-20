using DesafioPitang.Business.Interface.IBusiness;
using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Model;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using DesafioPitang.Validators.Manual;
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

        public async Task<CadastroAgendamentoDTO> CadastrarAgendamento(CadastroAgendamentoModel agendamento)
        {
            //IF nao trouxe id
            int userId;
            Paciente paciente;
            if (agendamento.Paciente.Id == null)
            {
                //Criar paciente e retornar o ID
                paciente = await _pacienteRepository.Add(new Paciente()
                {
                    Nome = agendamento.Paciente.Nome,
                    DataNascimento = agendamento.Paciente.DataNascimento,
                    DataCriacao = DateTime.Now
                });
                userId = paciente.Id;
            }
            //ELSE trouxe id
            else
            {
                //VALIDAÇÃO data de nascimento e nome
                paciente = await _pacienteRepository.GetById(agendamento.Paciente.Id.Value);
                PacienteMatchingValidator.Validar(agendamento.Paciente, paciente);
                userId = paciente.Id;

                //Já não está agendado para esse horario e dia
                if (await _agendamentoRepository.IsPacienteByIdAgendadoByDiaAndHora(userId, agendamento.Agendamento.Data.Date, agendamento.Agendamento.Horario))
                {
                    throw new BadRequestException(AgendamentoMessages.JaAgendado);
                }

            }

            //Horario do dia vago
            if (!await _agendamentoRepository.IsHorarioVagoByDia(agendamento.Agendamento.Horario, agendamento.Agendamento.Data.Date))
            {
                throw new BadRequestException(AgendamentoMessages.HorarioCheio);
            }
            //Nao esgotou as 20 vagas do dia
            if(!await _agendamentoRepository.IsDiaVago(agendamento.Agendamento.Data))
            {
                throw new BadRequestException(AgendamentoMessages.DiaCheio);
            }


            var agendamentoResult = await _agendamentoRepository.Add(new Agendamento()
            {
                PacienteId = userId,
                DataAgendamento = agendamento.Agendamento.Data,
                HoraAgendamento = agendamento.Agendamento.Horario,
                DataCriacao = DateTime.Now,
                Status = "Pendente"
            });

            return new CadastroAgendamentoDTO()
            {
                Paciente = new PacienteDTO(paciente),
                Agendamento = new AgendamentoDTO(agendamentoResult)
                //{
                //    Realizado = agendamentoResult.Status == "Realizado",
                //    Status = agendamentoResult.Status
                //}
            };
        }

        public async Task<List<HorarioDisponivelDTO>> ListarHorariosDisponiveisByDia(DateTime dia)
        {
            var horarios = await _agendamentoRepository.ListarHorariosByDia(dia.Date);

            var todosHorarios = Enumerable.Range(6, 14).Select(h => new TimeSpan(h, 0, 0)).ToList();
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

using DesafioPitang.Entities.Model;
using DesafioPitang.Utils.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Validators.Fluent
{
    public class CadastroAgendamentoValidator : AbstractValidator<CadastroAgendamentoModel>
    {
        public CadastroAgendamentoValidator()
        {
            RuleFor(cadastro => cadastro.Agendamento.Horario)
                                                    .NotNull()
                                                    .Must(horario => horario.Minutes == 0 && horario.Seconds == 0 && horario.Milliseconds == 0)
                                                        .WithMessage(AgendamentoMessages.AgendamentoDeHoraEmHora)
                                                    .Must(horario => horario.Hours >=6 && horario.Hours <= 19)
                                                        .WithMessage(AgendamentoMessages.HorarioParaAgendamento);

            RuleFor(cadastro => cadastro.Agendamento.Data)
                                                    .NotEmpty()
                                                    .NotNull()
                                                    .GreaterThanOrEqualTo(DateTime.Now.Date)
                                                        .WithMessage(AgendamentoMessages.AgendamentoNoPassado);

            RuleFor(cadastro => cadastro.Paciente.Id)
                                                 .GreaterThan(0);

            RuleFor(cadastro => cadastro.Paciente.Nome)
                                                 .NotEmpty()
                                                 .NotNull();

            RuleFor(cadastro => cadastro.Paciente.DataNascimento)
                                                 .NotEmpty()
                                                 .NotNull();

        }
    }
}

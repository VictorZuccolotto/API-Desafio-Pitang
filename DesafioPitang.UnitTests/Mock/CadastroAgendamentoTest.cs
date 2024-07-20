using DesafioPitang.Business.Business;
using DesafioPitang.Business.Interface.IBusiness;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Model;
using DesafioPitang.Repository.Interface.IRepositories;
using DesafioPitang.Utils.Exceptions;
using DesafioPitang.Utils.Messages;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.UnitTests.Mock
{
    public class CadastroAgendamentoTest : UnitTestBase
    {
        private IAgendamentoBusiness _business;
        private Mock<IAgendamentoRepository> _mockAgendamentoRepository;
        private Mock<IPacienteRepository> _mockPacienteRepository;


        [SetUp]
        public void SetUp()
        {
            Registrar<IAgendamentoBusiness, AgendamentoBusiness>();
            _mockAgendamentoRepository = RegistrarMock<IAgendamentoRepository>();
            _mockPacienteRepository = RegistrarMock<IPacienteRepository>();
            _business = ObterServico<IAgendamentoBusiness>();

        }


        
        [TestCase(null)]
        [TestCase(1)]
        public void CadastrarAgendamento_Sucesso(int? id)
        {
            var data = DateTime.Now;
            var horario = new TimeSpan(8,0,0);

            var dataNasciemto = new DateTime(2000, 7, 19);
            var pacienteModel = new PacienteModel()
            {
                Id = id,
                DataNascimento = dataNasciemto,
                Nome = "Teste"
            };

            var agendamentoModel = new AgendamentoModel()
            {
                Data = data,
                Horario = horario
            };

            var cadastro = new CadastroAgendamentoModel()
            {
                Paciente = pacienteModel,
                Agendamento = agendamentoModel
            };

            var paciente = new Paciente()
            {
                Id = 1,
                DataNascimento = dataNasciemto,
                Nome = "Teste",
                DataCriacao = DateTime.Now
            };

            _mockPacienteRepository.Setup(e => e.Add(It.IsAny<Paciente>()))
                            .Returns(() => Task.FromResult(paciente));

            _mockPacienteRepository.Setup(e => e.GetById(paciente.Id))
                            .Returns(() => Task.FromResult(paciente));

            _mockAgendamentoRepository.Setup(e => e.IsPacienteByIdAgendadoByDiaAndHora(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .Returns(() => Task.FromResult(false));
            
            _mockAgendamentoRepository.Setup(e => e.IsHorarioVagoByDia(It.IsAny<TimeSpan>(), It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));
            
            _mockAgendamentoRepository.Setup(e => e.IsDiaVago(It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));
            
            var agendamento = new Agendamento()
            {
                PacienteId = 1,
                DataAgendamento = data,
                HoraAgendamento = horario,
                Status = "Pendente"
            };

            _mockAgendamentoRepository.Setup(e => e.Add(It.IsAny<Agendamento>()))
                .Returns(() => Task.FromResult(agendamento));


            async Task action() => await _business.CadastrarAgendamento(cadastro);

            Assert.DoesNotThrowAsync(action);
        }


        [TestCase(1)]
        public void CadastrarAgendamento_PacienteModelDataNascimentoDiferentePaciente(int id)
        {
            var data = DateTime.Now;
            var horario = new TimeSpan(8, 0, 0);

            var dataNascimento = new DateTime(2000, 7, 19);
            var pacienteModel = new PacienteModel()
            {
                Id = id,
                DataNascimento = dataNascimento,
                Nome = "Teste"
            };

            var agendamentoModel = new AgendamentoModel()
            {
                Data = data,
                Horario = horario
            };

            var cadastro = new CadastroAgendamentoModel()
            {
                Paciente = pacienteModel,
                Agendamento = agendamentoModel
            };

            var paciente = new Paciente()
            {
                Id = 1,
                DataNascimento = dataNascimento.AddYears(1),
                Nome = "Teste",
                DataCriacao = DateTime.Now
            };

            _mockPacienteRepository.Setup(e => e.Add(It.IsAny<Paciente>()))
                            .Returns(() => Task.FromResult(paciente));

            _mockPacienteRepository.Setup(e => e.GetById(paciente.Id))
                            .Returns(() => Task.FromResult(paciente));

            _mockAgendamentoRepository.Setup(e => e.IsPacienteByIdAgendadoByDiaAndHora(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .Returns(() => Task.FromResult(false));

            _mockAgendamentoRepository.Setup(e => e.IsHorarioVagoByDia(It.IsAny<TimeSpan>(), It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));

            _mockAgendamentoRepository.Setup(e => e.IsDiaVago(It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));

            var agendamento = new Agendamento()
            {
                PacienteId = 1,
                DataAgendamento = data,
                HoraAgendamento = horario,
                Status = "Pendente"
            };

            _mockAgendamentoRepository.Setup(e => e.Add(It.IsAny<Agendamento>()))
                .Returns(() => Task.FromResult(agendamento));


            async Task action() => await _business.CadastrarAgendamento(cadastro);

            var exception = Assert.ThrowsAsync<BadRequestException>(action);

            Assert.IsTrue(exception.Message == string.Format("Seus dados não coincidem com os registrados anteriormente: Data de nascimento"));
        }
        
        [TestCase(1)]
        public void CadastrarAgendamento_PacienteModelNomeDiferentePaciente(int id)
        {
            var data = DateTime.Now;
            var horario = new TimeSpan(8, 0, 0);

            var dataNascimento = new DateTime(2000, 7, 19);
            var pacienteModel = new PacienteModel()
            {
                Id = id,
                DataNascimento = dataNascimento,
                Nome = "Teste"
            };

            var agendamentoModel = new AgendamentoModel()
            {
                Data = data,
                Horario = horario
            };

            var cadastro = new CadastroAgendamentoModel()
            {
                Paciente = pacienteModel,
                Agendamento = agendamentoModel
            };

            var paciente = new Paciente()
            {
                Id = 1,
                DataNascimento = dataNascimento,
                Nome = "Teste2",
                DataCriacao = DateTime.Now
            };

            _mockPacienteRepository.Setup(e => e.Add(It.IsAny<Paciente>()))
                            .Returns(() => Task.FromResult(paciente));

            _mockPacienteRepository.Setup(e => e.GetById(paciente.Id))
                            .Returns(() => Task.FromResult(paciente));

            _mockAgendamentoRepository.Setup(e => e.IsPacienteByIdAgendadoByDiaAndHora(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .Returns(() => Task.FromResult(false));

            _mockAgendamentoRepository.Setup(e => e.IsHorarioVagoByDia(It.IsAny<TimeSpan>(), It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));

            _mockAgendamentoRepository.Setup(e => e.IsDiaVago(It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));

            var agendamento = new Agendamento()
            {
                PacienteId = 1,
                DataAgendamento = data,
                HoraAgendamento = horario,
                Status = "Pendente"
            };

            _mockAgendamentoRepository.Setup(e => e.Add(It.IsAny<Agendamento>()))
                .Returns(() => Task.FromResult(agendamento));


            async Task action() => await _business.CadastrarAgendamento(cadastro);

            var exception = Assert.ThrowsAsync<BadRequestException>(action);

            Assert.IsTrue(exception.Message == string.Format("Seus dados não coincidem com os registrados anteriormente: Nome"));
        }        
        
        [TestCase(1)]
        public void CadastrarAgendamento_PacienteModelNomeEDataNascimentoDiferentePaciente(int id)
        {
            var data = DateTime.Now;
            var horario = new TimeSpan(8, 0, 0);

            var dataNascimento = new DateTime(2000, 7, 19);
            var pacienteModel = new PacienteModel()
            {
                Id = id,
                DataNascimento = dataNascimento,
                Nome = "Teste"
            };

            var agendamentoModel = new AgendamentoModel()
            {
                Data = data,
                Horario = horario
            };

            var cadastro = new CadastroAgendamentoModel()
            {
                Paciente = pacienteModel,
                Agendamento = agendamentoModel
            };

            var paciente = new Paciente()
            {
                Id = 1,
                DataNascimento = dataNascimento.AddYears(1),
                Nome = "Teste2",
                DataCriacao = DateTime.Now
            };

            _mockPacienteRepository.Setup(e => e.Add(It.IsAny<Paciente>()))
                            .Returns(() => Task.FromResult(paciente));

            _mockPacienteRepository.Setup(e => e.GetById(paciente.Id))
                            .Returns(() => Task.FromResult(paciente));

            _mockAgendamentoRepository.Setup(e => e.IsPacienteByIdAgendadoByDiaAndHora(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .Returns(() => Task.FromResult(false));

            _mockAgendamentoRepository.Setup(e => e.IsHorarioVagoByDia(It.IsAny<TimeSpan>(), It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));

            _mockAgendamentoRepository.Setup(e => e.IsDiaVago(It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));

            var agendamento = new Agendamento()
            {
                PacienteId = 1,
                DataAgendamento = data,
                HoraAgendamento = horario,
                Status = "Pendente"
            };

            _mockAgendamentoRepository.Setup(e => e.Add(It.IsAny<Agendamento>()))
                .Returns(() => Task.FromResult(agendamento));


            async Task action() => await _business.CadastrarAgendamento(cadastro);

            var exception = Assert.ThrowsAsync<BadRequestException>(action);

            Assert.IsTrue(exception.Message == string.Format("Seus dados não coincidem com os registrados anteriormente: Data de nascimento, Nome"));
        }

        [TestCase(1)]
        public void CadastrarAgendamento_PacienteJaAgendadoNoHorario(int id)
        {
            var data = DateTime.Now;
            var horario = new TimeSpan(8, 0, 0);

            var dataNasciemto = new DateTime(2000, 7, 19);
            var pacienteModel = new PacienteModel()
            {
                Id = id,
                DataNascimento = dataNasciemto,
                Nome = "Teste"
            };

            var agendamentoModel = new AgendamentoModel()
            {
                Data = data,
                Horario = horario
            };

            var cadastro = new CadastroAgendamentoModel()
            {
                Paciente = pacienteModel,
                Agendamento = agendamentoModel
            };

            var paciente = new Paciente()
            {
                Id = 1,
                DataNascimento = dataNasciemto,
                Nome = "Teste",
                DataCriacao = DateTime.Now
            };

            _mockPacienteRepository.Setup(e => e.Add(It.IsAny<Paciente>()))
                            .Returns(() => Task.FromResult(paciente));

            _mockPacienteRepository.Setup(e => e.GetById(paciente.Id))
                            .Returns(() => Task.FromResult(paciente));

            _mockAgendamentoRepository.Setup(e => e.IsPacienteByIdAgendadoByDiaAndHora(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .Returns(() => Task.FromResult(true));

            _mockAgendamentoRepository.Setup(e => e.IsHorarioVagoByDia(It.IsAny<TimeSpan>(), It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));

            _mockAgendamentoRepository.Setup(e => e.IsDiaVago(It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));


            var agendamento = new Agendamento()
            {
                PacienteId = 1,
                DataAgendamento = data,
                HoraAgendamento = horario,
                Status = "Pendente"
            };

            _mockAgendamentoRepository.Setup(e => e.Add(It.IsAny<Agendamento>()))
                .Returns(() => Task.FromResult(agendamento));


            async Task action() => await _business.CadastrarAgendamento(cadastro);


            var exception = Assert.ThrowsAsync<BadRequestException>(action);

            Assert.IsTrue(exception.Message == string.Format(AgendamentoMessages.JaAgendado));
        }

        [TestCase(null)]
        [TestCase(1)]
        public void CadastrarAgendamento_HorarioSemVagas(int? id)
        {
            var data = DateTime.Now;
            var horario = new TimeSpan(8, 0, 0);

            var dataNasciemto = new DateTime(2000, 7, 19);
            var pacienteModel = new PacienteModel()
            {
                Id = id,
                DataNascimento = dataNasciemto,
                Nome = "Teste"
            };

            var agendamentoModel = new AgendamentoModel()
            {
                Data = data,
                Horario = horario
            };

            var cadastro = new CadastroAgendamentoModel()
            {
                Paciente = pacienteModel,
                Agendamento = agendamentoModel
            };

            var paciente = new Paciente()
            {
                Id = 1,
                DataNascimento = dataNasciemto,
                Nome = "Teste",
                DataCriacao = DateTime.Now
            };

            _mockPacienteRepository.Setup(e => e.Add(It.IsAny<Paciente>()))
                            .Returns(() => Task.FromResult(paciente));

            _mockPacienteRepository.Setup(e => e.GetById(paciente.Id))
                            .Returns(() => Task.FromResult(paciente));

            _mockAgendamentoRepository.Setup(e => e.IsPacienteByIdAgendadoByDiaAndHora(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .Returns(() => Task.FromResult(false));

            _mockAgendamentoRepository.Setup(e => e.IsHorarioVagoByDia(It.IsAny<TimeSpan>(), It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(false));

            _mockAgendamentoRepository.Setup(e => e.IsDiaVago(It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));


            var agendamento = new Agendamento()
            {
                PacienteId = 1,
                DataAgendamento = data,
                HoraAgendamento = horario,
                Status = "Pendente"
            };

            _mockAgendamentoRepository.Setup(e => e.Add(It.IsAny<Agendamento>()))
                .Returns(() => Task.FromResult(agendamento));


            async Task action() => await _business.CadastrarAgendamento(cadastro);


            var exception = Assert.ThrowsAsync<BadRequestException>(action);

            Assert.IsTrue(exception.Message == string.Format(AgendamentoMessages.HorarioCheio));
        }
        
        [TestCase(null)]
        [TestCase(1)]
        public void CadastrarAgendamento_DiaSemVagas(int? id)
        {
            var data = DateTime.Now;
            var horario = new TimeSpan(8, 0, 0);

            var dataNasciemto = new DateTime(2000, 7, 19);
            var pacienteModel = new PacienteModel()
            {
                Id = id,
                DataNascimento = dataNasciemto,
                Nome = "Teste"
            };

            var agendamentoModel = new AgendamentoModel()
            {
                Data = data,
                Horario = horario
            };

            var cadastro = new CadastroAgendamentoModel()
            {
                Paciente = pacienteModel,
                Agendamento = agendamentoModel
            };

            var paciente = new Paciente()
            {
                Id = 1,
                DataNascimento = dataNasciemto,
                Nome = "Teste",
                DataCriacao = DateTime.Now
            };

            _mockPacienteRepository.Setup(e => e.Add(It.IsAny<Paciente>()))
                            .Returns(() => Task.FromResult(paciente));

            _mockPacienteRepository.Setup(e => e.GetById(paciente.Id))
                            .Returns(() => Task.FromResult(paciente));

            _mockAgendamentoRepository.Setup(e => e.IsPacienteByIdAgendadoByDiaAndHora(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<TimeSpan>()))
                            .Returns(() => Task.FromResult(false));

            _mockAgendamentoRepository.Setup(e => e.IsHorarioVagoByDia(It.IsAny<TimeSpan>(), It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(true));

            _mockAgendamentoRepository.Setup(e => e.IsDiaVago(It.IsAny<DateTime>()))
                            .Returns(() => Task.FromResult(false));


            var agendamento = new Agendamento()
            {
                PacienteId = 1,
                DataAgendamento = data,
                HoraAgendamento = horario,
                Status = "Pendente"
            };

            _mockAgendamentoRepository.Setup(e => e.Add(It.IsAny<Agendamento>()))
                .Returns(() => Task.FromResult(agendamento));


            async Task action() => await _business.CadastrarAgendamento(cadastro);


            var exception = Assert.ThrowsAsync<BadRequestException>(action);

            Assert.IsTrue(exception.Message == string.Format(AgendamentoMessages.DiaCheio));
        }

    }
}

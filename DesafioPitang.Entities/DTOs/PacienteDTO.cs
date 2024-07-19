using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.DTOs
{
    public class PacienteDTO
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public PacienteDTO(Paciente paciente)
        {
            Id = paciente.Id;
            Nome = paciente.Nome;
            DataNascimento = paciente.DataNascimento;
        }

        public PacienteDTO(PacienteModel paciente)
        {
            Id = paciente.Id;
            Nome = paciente.Nome;
            DataNascimento = paciente.DataNascimento;
        }
    }
}

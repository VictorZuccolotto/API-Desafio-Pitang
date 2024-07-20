using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Model;
using DesafioPitang.Utils.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Validators.Manual
{
    public static class PacienteMatchingValidator
    {
        public static void Validar(PacienteModel pacienteModel, Paciente paciente)
        {
            List<string> messages = new List<string>();
            if (pacienteModel.DataNascimento.Date != paciente.DataNascimento.Date)
                messages.Add("Data de nascimento");
            if (pacienteModel.Nome != paciente.Nome)
                messages.Add("Nome");
            if (messages.Count > 0)
                throw new BadRequestException("Seus dados não coincidem com os registrados anteriormente: "+ string.Join(", ", messages));
        }
    }
}

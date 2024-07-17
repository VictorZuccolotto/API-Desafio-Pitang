using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.Model
{
    public class PacienteModel
    {
        public int? Id { get; set; } 
        public string Nome { get; set; } 
        public DateTime DataNascimento { get; set; }
    }
}

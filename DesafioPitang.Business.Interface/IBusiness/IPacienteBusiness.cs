using DesafioPitang.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Business.Interface.IBusiness
{
    public interface IPacienteBusiness
    {
        Task<List<AgendamentoDTO>> ListarAgentamentosDTOFromPacienteById(int pacienteId);
    }
}

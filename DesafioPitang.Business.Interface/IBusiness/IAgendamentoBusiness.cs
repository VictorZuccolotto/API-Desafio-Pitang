using DesafioPitang.Entities.DTOs;
using DesafioPitang.Entities.Entities;
using DesafioPitang.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Business.Interface.IBusiness
{
    public interface IAgendamentoBusiness
    {
        Task<CadastroAgendamentoDTO> CadastrarAgendamento(CadastroAgendamentoModel agendamento);
        Task<List<HorarioDisponivelDTO>> ListarHorariosDisponiveisByDia(DateTime dia);

    }
}

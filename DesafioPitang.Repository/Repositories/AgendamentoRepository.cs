using DesafioPitang.Entities.Entities;
using DesafioPitang.Repository.Interface.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Repository.Repositories
{
    public class AgendamentoRepository : CrudRepository<Agendamento>, IAgendamentoRepository
    {
        public AgendamentoRepository(Context context) : base(context)
        {
        }
    }
}

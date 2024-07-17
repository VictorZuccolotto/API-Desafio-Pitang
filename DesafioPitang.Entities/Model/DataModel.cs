using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioPitang.Entities.Model
{
    public class DataModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
    }
}

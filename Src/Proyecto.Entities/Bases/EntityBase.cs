using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Entities.Bases
{
    public class EntityBase
    {
        public int Id { get; set; }
        public bool IsEnable { get; set; } = false;
    }
}

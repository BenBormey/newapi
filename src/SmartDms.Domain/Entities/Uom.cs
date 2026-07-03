using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuJuBis.Domain.Entities
{
    public class Uom
    {
        public int UOMId { get; set; }
        public string UOMCode { get; set; } = string.Empty;   // ឧ. KG, PCS, BOX
        public string UOMName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}

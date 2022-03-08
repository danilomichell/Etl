using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class Carreiras
    {
        public Carreiras()
        {
            Cargos = new HashSet<Cargos>();
        }

        public int CodCarreira { get; set; }
        public string DscCarreira { get; set; } = null!;

        public virtual ICollection<Cargos> Cargos { get; set; }
    }
}

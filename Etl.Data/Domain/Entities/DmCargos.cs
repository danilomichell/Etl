using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class DmCargos
    {
        public DmCargos()
        {
            FtLancamentos = new HashSet<FtLancamentos>();
        }

        public int CodCargo { get; set; }
        public string DscCarreira { get; set; } = null!;
        public string DscCargo { get; set; } = null!;

        public virtual ICollection<FtLancamentos> FtLancamentos { get; set; }
    }
}

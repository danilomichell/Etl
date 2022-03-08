using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class DmRubricas
    {
        public DmRubricas()
        {
            FtLancamentos = new HashSet<FtLancamentos>();
        }

        public int CodRubrica { get; set; }
        public string DscGrupo { get; set; } = null!;
        public string DscRubrica { get; set; } = null!;
        public string TipoRubrica { get; set; } = null!;

        public virtual ICollection<FtLancamentos> FtLancamentos { get; set; }
    }
}

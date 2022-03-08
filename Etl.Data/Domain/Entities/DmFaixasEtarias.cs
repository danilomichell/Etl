using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class DmFaixasEtarias
    {
        public DmFaixasEtarias()
        {
            FtLancamentos = new HashSet<FtLancamentos>();
        }

        public int CodFaixa { get; set; }
        public string DscFaixa { get; set; } = null!;
        public int IdadeInicial { get; set; }
        public int IdadeFinal { get; set; }

        public virtual ICollection<FtLancamentos> FtLancamentos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class DmSetores
    {
        public DmSetores()
        {
            FtLancamentos = new HashSet<FtLancamentos>();
        }

        public int CodSetor { get; set; }
        public string DscUnidade { get; set; } = null!;
        public string CidadeUnidade { get; set; } = null!;
        public char UfUnidade { get; set; }
        public string DscSetor { get; set; } = null!;

        public virtual ICollection<FtLancamentos> FtLancamentos { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class DmTemposFolhas
    {
        public DmTemposFolhas()
        {
            FtLancamentos = new HashSet<FtLancamentos>();
        }

        public int IdAnoMes { get; set; }
        public int Ano { get; set; }
        public int Mes { get; set; }

        public virtual ICollection<FtLancamentos> FtLancamentos { get; set; }
    }
}

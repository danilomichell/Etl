using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class DmTemposServicos
    {
        public DmTemposServicos()
        {
            FtLancamentos = new HashSet<FtLancamentos>();
        }

        public int CodTempoServ { get; set; }
        public string DscTempoServ { get; set; } = null!;
        public int AnoInicial { get; set; }
        public int AnoFinal { get; set; }

        public virtual ICollection<FtLancamentos> FtLancamentos { get; set; }
    }
}

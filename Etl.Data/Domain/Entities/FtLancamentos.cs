using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class FtLancamentos
    {
        public int CodRubrica { get; set; }
        public int CodSetor { get; set; }
        public int CodCargo { get; set; }
        public int CodFaixa { get; set; }
        public int CodTempoServ { get; set; }
        public int IdAnoMes { get; set; }
        public int TotalLanc { get; set; }
        public decimal ValorBruto { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorLiquido { get; set; }

        public virtual DmCargos CodCargoNavigation { get; set; } = null!;
        public virtual DmFaixasEtarias CodFaixaNavigation { get; set; } = null!;
        public virtual DmRubricas CodRubricaNavigation { get; set; } = null!;
        public virtual DmSetores CodSetorNavigation { get; set; } = null!;
        public virtual DmTemposServicos CodTempoServNavigation { get; set; } = null!;
        public virtual DmTemposFolhas IdAnoMesNavigation { get; set; } = null!;
    }
}

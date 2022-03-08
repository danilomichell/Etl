using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class Cargos
    {
        public Cargos()
        {
            EvolucoesFuncionais = new HashSet<EvolucoesFuncionais>();
        }

        public int CodCargo { get; set; }
        public string DscCargo { get; set; } = null!;
        public int CodCarreira { get; set; }

        public virtual Carreiras CodCarreiraNavigation { get; set; } = null!;
        public virtual ICollection<EvolucoesFuncionais> EvolucoesFuncionais { get; set; }
    }
}

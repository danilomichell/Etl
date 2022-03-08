using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class Setores
    {
        public Setores()
        {
            EvolucoesFuncionais = new HashSet<EvolucoesFuncionais>();
        }

        public int CodSetor { get; set; }
        public string DscSetor { get; set; } = null!;
        public int CodUnd { get; set; }
        public int CodColabChefe { get; set; }

        public virtual Colaboradores CodColabChefeNavigation { get; set; } = null!;
        public virtual Unidades CodUndNavigation { get; set; } = null!;
        public virtual ICollection<EvolucoesFuncionais> EvolucoesFuncionais { get; set; }
    }
}

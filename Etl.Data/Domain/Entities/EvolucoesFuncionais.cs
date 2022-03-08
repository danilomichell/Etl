using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class EvolucoesFuncionais
    {
        public int CodColab { get; set; }
        public DateOnly DatIni { get; set; }
        public int CodSetor { get; set; }
        public int CodCargo { get; set; }

        public virtual Cargos CodCargoNavigation { get; set; } = null!;
        public virtual Colaboradores CodColabNavigation { get; set; } = null!;
        public virtual Setores CodSetorNavigation { get; set; } = null!;
    }
}

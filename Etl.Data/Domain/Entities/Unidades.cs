using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class Unidades
    {
        public Unidades()
        {
            Setores = new HashSet<Setores>();
        }

        public int CodUnd { get; set; }
        public string DscUnd { get; set; } = null!;
        public string CidUnd { get; set; } = null!;
        public string UfUnd { get; set; } = null!;

        public virtual ICollection<Setores> Setores { get; set; }
    }
}

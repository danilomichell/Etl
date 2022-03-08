using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class GruposRubricas
    {
        public GruposRubricas()
        {
            Rubricas = new HashSet<Rubricas>();
        }

        public int CodGrupo { get; set; }
        public string DscGrupo { get; set; } = null!;

        public virtual ICollection<Rubricas> Rubricas { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class Rubricas
    {
        public Rubricas()
        {
            Lancamentos = new HashSet<Lancamentos>();
        }

        public int CodRubrica { get; set; }
        public string DscRubrica { get; set; } = null!;
        public char TpoRubrica { get; set; }
        public int CodGrupo { get; set; }

        public virtual GruposRubricas CodGrupoNavigation { get; set; } = null!;
        public virtual ICollection<Lancamentos> Lancamentos { get; set; }
    }
}

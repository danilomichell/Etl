using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class FolhasPagamentos
    {
        public FolhasPagamentos()
        {
            Lancamentos = new HashSet<Lancamentos>();
        }

        public int Ano { get; set; }
        public int Mes { get; set; }
        public char TpoFolha { get; set; }
        public string DscFolha { get; set; } = null!;

        public virtual ICollection<Lancamentos> Lancamentos { get; set; }
    }
}

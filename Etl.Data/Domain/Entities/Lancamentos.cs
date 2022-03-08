using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class Lancamentos
    {
        public int Ano { get; set; }
        public int Mes { get; set; }
        public char TpoFolha { get; set; }
        public int CodRubrica { get; set; }
        public int CodColab { get; set; }
        public DateOnly DatLanc { get; set; }
        public decimal ValLanc { get; set; }

        public virtual Colaboradores CodColabNavigation { get; set; } = null!;
        public virtual Rubricas CodRubricaNavigation { get; set; } = null!;
        public virtual FolhasPagamentos FolhasPagamentos { get; set; } = null!;
    }
}

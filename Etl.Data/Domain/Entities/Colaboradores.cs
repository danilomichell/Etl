using System;
using System.Collections.Generic;

namespace Etl.Data.Domain.Entities
{
    public partial class Colaboradores
    {
        public Colaboradores()
        {
            EvolucoesFuncionais = new HashSet<EvolucoesFuncionais>();
            Lancamentos = new HashSet<Lancamentos>();
            Setores = new HashSet<Setores>();
        }

        public int CodColab { get; set; }
        public string NomColab { get; set; } = null!;
        public DateOnly DatNasc { get; set; }
        public DateOnly DatAdmissao { get; set; }

        public virtual ICollection<EvolucoesFuncionais> EvolucoesFuncionais { get; set; }
        public virtual ICollection<Lancamentos> Lancamentos { get; set; }
        public virtual ICollection<Setores> Setores { get; set; }
    }
}

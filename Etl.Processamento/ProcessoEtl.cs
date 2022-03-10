using System.Data;
using System.Transactions;
using Etl.Data.Context;
using Etl.Data.Domain.Entities;
using Etl.Processamento.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Etl.Processamento
{
    public class ProcessoEtl : AbstractResponse, IProcessoEtl
    {
        public readonly FolhaContext Context;

        public ProcessoEtl(FolhaContext context)
        {
            Context = context;
        }
        public void Processar()
        {
            Exclud();
            var lancamentos = Transform();
            if (!lancamentos.Success) Console.Write(lancamentos.Message);
            var load = Load(lancamentos.Value);
            if (!load.Success) Console.Write(load.Message);
        }

        private void Exclud()
        {
            var message = Truncate(TableName(Context.DmCargos));
            if (!string.IsNullOrEmpty(message.Message)) Console.Write(message.Message);

            message = Truncate(TableName(Context.DmFaixasEtarias));
            if (!string.IsNullOrEmpty(message.Message)) Console.Write(message.Message);

            message = Truncate(TableName(Context.DmRubricas));
            if (!string.IsNullOrEmpty(message.Message)) Console.Write(message.Message);

            message = Truncate(TableName(Context.DmSetores));
            if (!string.IsNullOrEmpty(message.Message)) Console.Write(message.Message);

            message = Truncate(TableName(Context.DmTemposFolhas));
            if (!string.IsNullOrEmpty(message.Message)) Console.Write(message.Message);

            message = Truncate(TableName(Context.DmTemposServicos));
            if (!string.IsNullOrEmpty(message.Message)) Console.Write(message.Message);

            message = Truncate(TableName(Context.FtLancamentos));
            if (!string.IsNullOrEmpty(message.Message)) Console.Write(message.Message);
        }

        private GenerateResponse<List<FtLancamentos>> Transform()
        {
            var lancamentosRelacional = Context.Lancamentos
                .Include(x => x.CodRubricaNavigation)
                .ThenInclude(x => x.CodGrupoNavigation)
                .Include(x => x.FolhasPagamentos)
                .Include(x => x.CodColabNavigation)
                .ThenInclude(x => x.EvolucoesFuncionais)
                .ThenInclude(x => x.CodSetorNavigation)
                .ThenInclude(x => x.CodUndNavigation)
                .Include(x => x.CodColabNavigation.EvolucoesFuncionais)
                .ThenInclude(x => x.CodCargoNavigation)
                .ThenInclude(x => x.CodCarreiraNavigation)
                .ToList();
            var lancamentos = new List<FtLancamentos>();
            foreach (var lancamento in lancamentosRelacional)
            {
                var ftLancamento = new FtLancamentos()
                {
                    CodCargoNavigation = new DmCargos
                    {
                        CodCargo = lancamento.CodColabNavigation.EvolucoesFuncionais.Last().CodCargo,
                        DscCargo = lancamento.CodColabNavigation.EvolucoesFuncionais.Last().CodCargoNavigation.DscCargo,
                        DscCarreira = lancamento.CodColabNavigation.EvolucoesFuncionais.Last().CodCargoNavigation
                            .CodCarreiraNavigation.DscCarreira
                    },
                    CodSetorNavigation = new DmSetores()
                    {
                        CidadeUnidade = lancamento.CodColabNavigation.EvolucoesFuncionais.Last().CodSetorNavigation
                            .CodUndNavigation.DscUnd,
                        CodSetor = lancamento.CodColabNavigation.EvolucoesFuncionais.Last().CodSetor,
                        DscSetor = lancamento.CodColabNavigation.EvolucoesFuncionais.Last().CodSetorNavigation.DscSetor,
                        DscUnidade = lancamento.CodColabNavigation.EvolucoesFuncionais.Last().CodSetorNavigation
                            .CodUndNavigation.DscUnd,
                        UfUnidade = Convert.ToChar(lancamento.CodColabNavigation.EvolucoesFuncionais.Last()
                            .CodSetorNavigation.CodUndNavigation.UfUnd)
                    },
                    CodRubricaNavigation = new DmRubricas()
                    {
                        CodRubrica = lancamento.CodRubrica,
                        DscRubrica = lancamento.CodRubricaNavigation.DscRubrica,
                        DscGrupo = lancamento.CodRubricaNavigation.CodGrupoNavigation.DscGrupo,
                        TipoRubrica = lancamento.CodRubricaNavigation.TpoRubrica.ToString()
                    },
                    ValorBruto = lancamento.ValLanc,
                    ValorLiquido = lancamento.ValLanc - lancamento.CodRubricaNavigation.Lancamentos
                        .Where(x => x.CodRubricaNavigation.TpoRubrica == 'D').Select(x => x.ValLanc).Sum(),
                    ValorDesconto = lancamento.CodRubricaNavigation.Lancamentos
                        .Where(x => x.CodRubricaNavigation.TpoRubrica == 'D').Select(x => x.ValLanc).Sum(),
                    TotalLanc = lancamento.CodRubricaNavigation.Lancamentos.Count
                };
                var tempServico = DateTime.Now.Year - lancamento.CodColabNavigation.DatAdmissao.Year;
                ftLancamento.CodTempoServNavigation = tempServico switch
                {
                    <= 21 => new DmTemposServicos()
                    {
                        AnoInicial = 18,
                        AnoFinal = 21,
                        DscTempoServ = $"{tempServico} anos"
                    },
                    <= 30 => new DmTemposServicos()
                    {
                        AnoInicial = 22,
                        AnoFinal = 30,
                        DscTempoServ = $"{tempServico} anos"
                    },
                    <= 45 => new DmTemposServicos()
                    {
                        AnoInicial = 31,
                        AnoFinal = 45,
                        DscTempoServ = $"{tempServico} anos"
                    },
                    _ => new DmTemposServicos()
                    {
                        AnoInicial = 46,
                        AnoFinal = tempServico,
                        DscTempoServ = $"{tempServico} anos"
                    }
                };

                lancamentos.Add(ftLancamento);
            }
            return GenerateSuccessResponse(lancamentos);
        }

        private GenerateResponse Load(IEnumerable<FtLancamentos> lancamentos)
        {
            try
            {
                using var scope = new TransactionScope();
                Context.FtLancamentos.AddRange(lancamentos);
                Context.SaveChanges();
                scope.Complete();
                return GenerateSuccessResponse();
            }
            catch (Exception e)
            {
                return GenerateErroResponse(e.Message);
            }
        }

        private GenerateResponse Truncate(string tableName)
        {
            try
            {
                var cmd = $"delete from {tableName}";
                using var command = Context.Database.GetDbConnection().CreateCommand();
                if (command.Connection!.State != ConnectionState.Open)
                {
                    command.Connection.Open();
                }

                command.CommandText = cmd;
                command.ExecuteNonQuery();
                return GenerateSuccessResponse();
            }
            catch (Exception ex)
            {
                return GenerateErroResponse(ex.Message);
            }
        }

        private static string GetName(IReadOnlyAnnotatable entityType, string defaultSchemaName = "folhadw")
        {
            var schema = entityType.FindAnnotation("Relational:Schema")!.Value;
            var tableName = entityType.GetAnnotation("Relational:TableName").Value!.ToString();
            var schemaName = schema == null ? defaultSchemaName : schema.ToString();
            var name = $"{schemaName}.{tableName}";
            return name;
        }

        private static string TableName<T>(DbSet<T> dbSet) where T : class
        {
            var entityType = dbSet.EntityType;
            return GetName(entityType);
        }
    }

    public interface IProcessoEtl
    {
        void Processar();
    }
}

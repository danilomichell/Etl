using System.Data;
using Etl.Data.Context;
using Etl.Data.Domain.Entities;
using Etl.Processamento.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

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
        }

        public void Exclud()
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
            var colaboradoresRelacional = Context.Colaboradores
                .Include(x => x.Lancamentos)
                .ThenInclude(x => x.CodRubricaNavigation)
                .ThenInclude(x => x.CodGrupoNavigation)
                .Include(x => x.Lancamentos)
                .ThenInclude(x => x.FolhasPagamentos)
                .Include(x => x.EvolucoesFuncionais)
                .ThenInclude(x => x.CodSetorNavigation)
                .ThenInclude(x => x.CodUndNavigation)
                .Include(x => x.EvolucoesFuncionais)
                .ThenInclude(x => x.CodSetorNavigation)
                .ThenInclude(x => x.CodUndNavigation)
                .Include(x => x.EvolucoesFuncionais)
                .ThenInclude(x => x.CodCargoNavigation)
                .ThenInclude(x => x.CodCarreiraNavigation)
                .Include(x => x.Setores)
                .ThenInclude(x => x.CodUndNavigation)
                .ToList();
            //var teste = lancamentosRelacional.Where(x => x.Setores.Any());
            return GenerateSuccessResponse(new List<FtLancamentos>());
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

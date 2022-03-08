using Etl.Processamento;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = Configurations.Inject();

serviceProvider.GetRequiredService<IProcessoEtl>().Processar();
using FichaCadastroAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FichaCadastroAPI.HealthCheck
{
    public class HealthCheckCustom : IHealthCheck
    {
        private readonly FichaCadastroContextDB fichaCadastroContextDB;
        private readonly IWebHostEnvironment hostEnvironment;

        public HealthCheckCustom(FichaCadastroContextDB fichaCadastroContextDB,
                                 IWebHostEnvironment hostEnvironment)
        {
            this.fichaCadastroContextDB = fichaCadastroContextDB;
            this.hostEnvironment = hostEnvironment;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            bool comunicaoSQL = false;
            bool healthyStatus = false;
            HealthCheckResult healthy = default;
            Exception? exHealthCheck = null;

            Dictionary<string, object> data = new Dictionary<string, object>()
            {
                {
                    "detalhes-app", new {
                                            ambiente = hostEnvironment.EnvironmentName,
                                            nomeApp = hostEnvironment.ApplicationName
                                        }
                },
                {
                    "ultima-consulta", DateTime.Now
                }
            };

            try
            {
                var returns = fichaCadastroContextDB.Database
                                                    .SqlQueryRaw<int>("SELECT 1 AS HealthCheck")
                                                    .ToList()
                                                    .FirstOrDefault();

                if (returns == 1)
                {
                    comunicaoSQL = true;
                    var dataBancoDados = new
                    {
                        instanciaIdDoEf = fichaCadastroContextDB.ContextId,
                        sqlComunicacaoSucesso = comunicaoSQL,
                        connectionString = fichaCadastroContextDB.Database.GetConnectionString()
                    };

                    data.Add("informacaoDB", dataBancoDados);
                    healthyStatus = true;
                }

            }
            catch (Exception ex)
            {
                exHealthCheck = ex;
            }

            if (healthyStatus == true)
            {
                healthy = HealthCheckResult.Healthy("Tudo certo com a aplicação", data: data);
            }
            else
            {
                healthy = HealthCheckResult.Unhealthy("Serviço indisponível.", data: data, exception: exHealthCheck);
            }

            return await Task.FromResult(healthy);
        }
    }
}

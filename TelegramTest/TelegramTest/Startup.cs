using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: FunctionsStartup(typeof(TelegramTest.Startup))]

namespace TelegramTest
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Register ILogger<T> and ILoggerFactory
            builder.Services.AddLogging();

            // Recogemos el token del bot que enviará las alertas
            Config._token = Environment
                .GetEnvironmentVariable("token", EnvironmentVariableTarget.Process)
                ?? throw new ArgumentException("No se encontró token. se debe configurar en las varialbles de entorno");

            // Recogemos la cadena de conexión a sql
            Config._sqlconexion = Environment
                .GetEnvironmentVariable("sqlconexion", EnvironmentVariableTarget.Process)
                ?? throw new ArgumentException("No se encontró la conexión a sql. se debe configurar en las varialbles de entorno");

            //Este paso es importante, es dónde se inicia el cliente de Telegram
            Config.Inicia();

            // Aquí ponemos un objeto de UpdateServices para DependencyInjection
            builder.Services.AddScoped<UpdateServices>();
            builder.Services.AddScoped<SqlServices>();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using Telegram.Bot;
using System;
using TelegramTest.Modelos;


#pragma warning disable CA1031


namespace TelegramTest
{
    public class TelegramFunc
    {
        private readonly ILogger<TelegramFunc> _logger;
        private readonly UpdateServices _updateService;
        public TelegramFunc(ILogger<TelegramFunc> logger
                , UpdateServices updateService)
        {
            _logger = logger;
            _updateService = updateService;
        }

        [FunctionName("EnvioTelegram")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation($"Empieza la función : {DateTime.Now.ToString("yyyyMMddHHmmssttt")}");
            try
            {
                var body = await req.ReadAsStringAsync();
                var _alerta = JsonSerializer.Deserialize<Alerta>(body, JsonBotAPI.Options);
                if (_alerta is null)
                {
                    _logger.LogWarning("No se pudo deserializar el objeto.");
                    return new OkResult();
                }
                await _updateService.TratarMsgTelegram(_alerta);
            }
            catch (Exception e)
            {
                _logger.LogError("Exception: " + e.Message);
            }
            return new OkResult();
        }
    }
}

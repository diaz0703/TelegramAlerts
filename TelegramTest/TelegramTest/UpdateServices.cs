using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramTest.Modelos;

namespace TelegramTest
{
    public class UpdateServices
    {
        private readonly ILogger _logger;
        private readonly ITelegramBotClient _botClient;
        private readonly SqlServices _sqlserv;

        public UpdateServices(ILogger<UpdateServices> logger, SqlServices sql)
        {
            _logger = logger;
            _botClient = Config._botClient;
            _sqlserv = sql;
        }

        public async Task TratarMsgTelegram(Alerta alerta)
        {
            int _res = 0;
            _res = await GuardarAlertaBD(alerta);
            await EnviaMsgTelegram(alerta, _res);
        }
        public async Task EnviaMsgTelegram(Alerta alerta, int alertaid)
        {
            _logger.LogInformation("Comienza el envío de mensajes", "");
            //Sacar de la base de datos los destinatarios
            List<TelegramBeneficiarios> _benef = _sqlserv.SelectBeneficiarios();
            foreach (TelegramBeneficiarios _item in _benef)
            {
                await _botClient.SendMessage(_item.TelegramId, alerta.Data.Essentials.AlertRule);
                TelegramMensajes _men = new TelegramMensajes()
                {
                    AlertaFilaId = alertaid,
                    TelegramId = _item.TelegramId,
                    FilaId = _item.FilaId,
                    MensaTexto = alerta.Data.Essentials.AlertRule,
                };
                _sqlserv.GuardaAlertaEnviada(_men);
            }
        }
        public async Task<int> GuardarAlertaBD(Alerta alerta)
        {
            TelegramAlertas _alert = new TelegramAlertas();
            _alert.TipoAlerta = alerta.Data.Essentials.AlertRule;
            _alert.Alerta = System.Text.Json.JsonSerializer.Serialize(_alert);
            int _res = _sqlserv.GuardaAlertaDisparada(_alert);
            _logger.LogInformation("Se guardó la alerta en la bd", "");
            return _res;
        }
    }
}

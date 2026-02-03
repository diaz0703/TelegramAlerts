using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;
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

        public async Task TratarMsgTelegram(Alerta alerta, string body )
        {
            int _res = 0;
            _res = await GuardarAlertaBD(alerta, body);
        }
        public async Task EnviaMsgTelegram(Alerta alerta, int alertaid, int alertaviva)
        {
            _logger.LogInformation("Comienza el envío de mensajes", "");
            //Sacar de la base de datos los destinatarios
            string _mensaje = alerta.Data.Essentials.AlertRule;
            if (alertaviva == 9)
                _mensaje += " - ALERTA RESUELTA";

            List<TelegramBeneficiarios> _benef = _sqlserv.SelectBeneficiarios();
            foreach (TelegramBeneficiarios _item in _benef)
            {
                try
                {
                    await _botClient.SendMessage(_item.TelegramId, _mensaje);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation($"Nos se envía mensaje: {ex.Message}");
                }

                TelegramMensajes _men = new TelegramMensajes()
                {
                    AlertaFilaId = alertaid,
                    TelegramId = _item.TelegramId,
                    FilaId = _item.FilaId,
                    MensaTexto = _mensaje,
                };
                _sqlserv.GuardaAlertaEnviada(_men);
            }
        }
        public async Task<int> GuardarAlertaBD(Alerta alerta, string body)
        {
            TelegramAlertas _alert = new TelegramAlertas();
            _alert.TipoAlerta = alerta.Data.Essentials.AlertRule;
            _alert.Alerta = System.Text.Json.JsonSerializer.Serialize(alerta);
            _alert.AzureAlertaId = alerta.Data.Essentials.AlertId;
            _alert.AlertaViva = alerta.Data.Essentials.MonitorCondition == "Fired" ? 1 : 9;
            if (alerta.Data.Essentials.MonitorCondition == "Fired")
            {
                _alert.FechaAlertaFire = alerta.Data.Essentials.FiredDateTime;
            }
            if (alerta.Data.Essentials.MonitorCondition == "Resolved")
            { 
                _alert.FechaAlertaSolve = alerta.Data.Essentials.ResolvedDateTime;
            }
            int _res = _sqlserv.GuardaAlertaDisparada(_alert, body, alerta);
            _logger.LogInformation("Se guardó la alerta en la bd", "");

            await EnviaMsgTelegram(alerta, _res, _alert.AlertaViva);

            return _res;
        }
    }
}

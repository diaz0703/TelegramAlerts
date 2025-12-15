using System.Net.Http;
using Telegram.Bot;

namespace TelegramTest
{
    internal static class Config
    {
        public static string _token = "";
        public static string _sqlconexion = "";
        public static ITelegramBotClient _botClient;
        public static void Inicia()
        {
            HttpClient _cl = new HttpClient();
            _botClient = new TelegramBotClient(_token, _cl);
        }

    }
}

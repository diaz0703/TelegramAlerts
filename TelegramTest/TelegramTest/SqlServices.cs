using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TelegramTest.Modelos;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TelegramTest
{
    public class SqlServices
    {
        private readonly ILogger<SqlServices> _logger;
        public SqlServices(ILogger<SqlServices> logger)
        {
            _logger = logger;
        }
        public List<TelegramBeneficiarios> SelectBeneficiarios()
        {
            string Consulta = "SELECT [filaid] ,[usuario]   ,[nombre]  ,[telegramid]  ,[situacion]  ,[usuarioinserta] ,[fechainserta] FROM [TelegramBeneficiarios]  where situacion = 1";
            List<TelegramBeneficiarios> _result = new List<TelegramBeneficiarios>();
            using (SqlConnection connection = new SqlConnection(Config._sqlconexion))
            {
                try
                {
                    connection.Open();
                    _logger.LogInformation("Conexión exitosa a SQL Server.");
                    using (SqlCommand command = new SqlCommand(Consulta, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Llenamos el objeto
                            TelegramBeneficiarios _fila = new TelegramBeneficiarios()
                            {
                                FilaId = reader.GetInt32(reader.GetOrdinal("filaid")),
                                Usuario = reader.IsDBNull(reader.GetOrdinal("usuario")) ? null : reader.GetString(reader.GetOrdinal("usuario")),
                                Nombre = reader.IsDBNull(reader.GetOrdinal("nombre")) ? null : reader.GetString(reader.GetOrdinal("nombre")),
                                TelegramId = reader.IsDBNull(reader.GetOrdinal("telegramid")) ? null : reader.GetString(reader.GetOrdinal("telegramid")),
                                Situacion = reader.IsDBNull(reader.GetOrdinal("situacion")) ? null : reader.GetInt32(reader.GetOrdinal("situacion")),
                                UsuarioInserta = reader.IsDBNull(reader.GetOrdinal("usuarioinserta")) ? null : reader.GetString(reader.GetOrdinal("usuarioinserta")),
                                FechaInserta = reader.IsDBNull(reader.GetOrdinal("fechainserta")) ? null : reader.GetDateTime(reader.GetOrdinal("fechainserta"))
                            };
                            //Llenamos el objeto
                            _result.Add(_fila);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                }
            }
            return _result;
        }

        public void GuardaAlertaEnviada(TelegramMensajes obj)
        {
            string Consulta = "INSERT INTO [TelegramMensajes]   ([filaid]  ,[AlertaFilaId]   ,[telegramid]  ,[MensaTexto]  )  VALUES  (@filaid  ,@AlertaFilaId  ,@telegramid   ,@MensaTexto ) ; ";
            using (SqlConnection connection = new SqlConnection(Config._sqlconexion))
            {
                try
                {
                    connection.Open();
                    _logger.LogInformation("Conexión exitosa a SQL Server.");
                    using (SqlCommand command = new SqlCommand(Consulta, connection))
                    {
                        command.Parameters.AddWithValue("@FilaId", obj.FilaId.ToString());
                        command.Parameters.AddWithValue("@AlertaFilaId", obj.AlertaFilaId.ToString());
                        command.Parameters.AddWithValue("@TelegramId", obj.TelegramId.ToString());
                        command.Parameters.AddWithValue("@MensaTexto", obj.MensaTexto.ToString());
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                }
            }
        }
        public int GuardaAlertaDisparada(TelegramAlertas obj , string body)
        {
            string Consulta = "INSERT INTO [TelegramAlertas]   ([TipoAlerta]  ,[Alerta], ALERTAVIVA, AzureAlertaId)  VALUES (@TipoAlerta ,@Alerta, @AlertaViva, @AzureAlertaId) ; select @@identity; ";
            string ConsultaUp = "UPDATE [TelegramAlertas]   SET  [AlertaRes] = @Alerta  , FechaResuelve = @FechaResuelve ,  alertaviva = @AlertaViva where AzureAlertaId  = @AzureAlertaId  ; select 1; ";
            string _consultaFinal = Consulta;
            DateTime _fechainserta = DateTime.Now;
            if (obj.AlertaViva == 9)
            {
                _consultaFinal = ConsultaUp;
            }
            int _res  = 0;
            using (SqlConnection connection = new SqlConnection(Config._sqlconexion))
            {
                try
                {
                    connection.Open();
                    _logger.LogInformation("Conexión exitosa a SQL Server.");
                    using (SqlCommand command = new SqlCommand(_consultaFinal, connection))
                    {
                        command.Parameters.AddWithValue("@TipoAlerta", obj.TipoAlerta.ToString());
                        command.Parameters.AddWithValue("@AzureAlertaId", obj.AzureAlertaId);
                        command.Parameters.AddWithValue("@Alerta", body);
                        command.Parameters.AddWithValue("@AlertaViva", obj.AlertaViva.ToString());
                        command.Parameters.AddWithValue("@FechaResuelve", _fechainserta.ToString("yyyy/MM/dd HH:mm:ss"));
                        _res = Convert.ToInt32(command.ExecuteScalarAsync().Result);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error: {ex.Message}");
                }
            }
            return  _res;
        }
    }
}

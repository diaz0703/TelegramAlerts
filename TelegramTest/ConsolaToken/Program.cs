
using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Identity;

class Program
{
    static async Task Main()
    {
        string tenantId = Environment.GetEnvironmentVariable("ENTRA_TENANT_ID")!;
        string clientId = Environment.GetEnvironmentVariable("ENTRA_CLIENT_ID")!;
        string clientSecret = Environment.GetEnvironmentVariable("ENTRA_CLIENT_SECRET")!;

        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        // Ejemplos de recursos
//        string[] scopes = { "https://management.azure.com/.default" };
        string[] scopes = { "api://bbed0ecb-92c6-4ef5-885f-e7a36cf04505/.default" };

        AccessToken token = await credential.GetTokenAsync(new TokenRequestContext(scopes));
        Console.WriteLine(token.Token);
        Console.ReadLine();
        
        HttpClient _h = new HttpClient();
        _h.BaseAddress = new Uri("https://localhost:7267/WeatherForecast");

        //agregar un bearer token al request _h
        _h.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Token);

        HttpResponseMessage _rm =  await        _h.GetAsync("");

        if (_rm.IsSuccessStatusCode)
        {
            Console.WriteLine("Llamada exitosa");
        }
        else
        {
            Console.WriteLine($"Llamada fallida : {(int)_rm.StatusCode}, Mensaje: {_rm.ReasonPhrase}");
        }
    }
}

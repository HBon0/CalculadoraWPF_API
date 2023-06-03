using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculadoraMath.BL
{
    public class CalculadoraAPI
    {
        HttpClient client = new HttpClient();
        string Url = "http://api.mathjs.org/v4/";

        public async Task<string> POST (string Expr)
        {
           var objetc = new { 
                expr = Expr,
                precision = 14
            };

            var JsonObject = JsonSerializer.Serialize(objetc);
            StringContent content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

            var httpResponse =  await client.PostAsync(Url, content);
            if (httpResponse.IsSuccessStatusCode)
            {
                var contentResponse = await httpResponse.Content.ReadAsStringAsync();
                var Res = JsonSerializer.Deserialize<Response>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true});
                return Res.result;
            }

            return "Error al calcular";
        }
    }
}

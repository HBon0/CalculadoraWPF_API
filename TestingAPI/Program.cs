// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Text;

Console.WriteLine("Hello, World!");

HttpClient client = new HttpClient();
string Url = "http://api.mathjs.org/v4/";

var result = await POST("5.08 cm in inch");
Console.WriteLine("Resultado de API es : " + result);

async Task<string> POST(string Expr)
{
    try
    {
        var objetc = new
        {
            expr = Expr,
            precision = 14
        };

        var JsonObject = JsonSerializer.Serialize(objetc);
        StringContent content = new StringContent(JsonObject, Encoding.UTF8, "application/json");

        var httpResponse = await client.PostAsync(Url, content);
        if (httpResponse.IsSuccessStatusCode)
        {
            var contentResponse = await httpResponse.Content.ReadAsStringAsync();
            var Res = JsonSerializer.Deserialize<Response>(contentResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return Res.result;
        }

        return "Error al calcular";
    }
    catch (Exception e)
    {
        throw;
    }
}

class Response
{
    public string? result { get; set; }
    public string? error { get; set; }
}

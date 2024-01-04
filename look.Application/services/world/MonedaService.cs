using look.Application.interfaces.world;
using look.domain.dto.admin;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.world;
using Microsoft.Office.SharePoint.Tools;
using Newtonsoft.Json;
using Serilog;
using System.Text.RegularExpressions;


namespace look.Application.services.world
{
    public class MonedaService : Service<Moneda>, IMonedaService
    {
        //instanciar repository si se requiere 
        private readonly IMonedaRepository _monedaRepository;

        public MonedaService(IMonedaRepository monedaRepository) : base(monedaRepository)
        {
            _monedaRepository = monedaRepository;
        }

        public async Task<string> consultaMonedaConvertida(string fromCurrency,string toCurrency,double amount)
        {
            string responseBody = "";

            var culture = System.Globalization.CultureInfo.InvariantCulture;
            var amountFormat = amount.ToString("0.###",culture);

            double MonedaConvertida = 0.0;
            string baseUrl = "https://api.exchangeratesapi.io/v1/convert";
            string apiKey = "7a2d122af2e9771c0dc8165fa0399598";
            string apiUrl = $"{baseUrl}?access_key={apiKey}&from={fromCurrency}&to={toCurrency}&amount={amountFormat}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        responseBody= await response.Content.ReadAsStringAsync();
                        ConversionResult conversionResult = JsonConvert.DeserializeObject<ConversionResult>(responseBody);
                        MonedaConvertida = conversionResult.result;
                        var resultJson = new
                        {
                            MonedaConvertida = conversionResult.result
                        };
                        responseBody = JsonConvert.SerializeObject(resultJson);
                        
                    }
                    else
                    {
                        responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }

            //var client = new RestClient("https://api.apilayer.com/exchangerates_data/convert?to=CLP&from=PEN&amount=0.70000000000000007");
            //client.Timeout = -1;

            //var request = new RestRequest(Method.GET);
            //request.AddHeader("apikey", "QsYlLVhYUKpIy3oqGgk8MmX6trh7rxrj");

            //IRestResponse response = client.Execute(request);
            //Console.WriteLine(response.Content);

            return responseBody;
        }
    }
}

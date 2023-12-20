using look.Application.interfaces.world;
using look.domain.dto.admin;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.world;
using Newtonsoft.Json;
using Serilog;


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

        public async Task<string> consultaMonedaConvertida(string idTo,string idFrom,int amount)
        {
            string responseBody = "";
            double MonedaConvertida = 0.0;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "https://api.exchangeratesapi.io/v1/convert?access_key=7a2d122af2e9771c0dc8165fa0399598&from="+idTo+"&to="+idFrom+"&amount="+amount;
                    HttpResponseMessage response = await client.GetAsync(url);
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
                        Console.WriteLine($"Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            
            return responseBody;
        }
    }
}

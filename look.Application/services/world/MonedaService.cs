using look.Application.interfaces.world;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.world;
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

        public async Task<Moneda> consultaMonedaConvertida(int id,string amount)
        {
            var clientId = await _monedaRepository.GetByIdAsync((int) id);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string url = "https://api.exchangeratesapi.io/v1/convert?access_key=7a2d122af2e9771c0dc8165fa0399598&from=CLP&to="+clientId.MonNombre+"&amount="+amount;
                    Log.Information("url que va a servicio"+url);
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(responseBody);
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
            return clientId;
        }
    }
}

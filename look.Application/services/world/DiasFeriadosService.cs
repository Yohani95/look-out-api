using look.Application.interfaces.world;
using look.domain.entities.admin;
using look.domain.entities.world;
using look.domain.interfaces;
using look.domain.interfaces.world;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.world
{
    public class DiasFeriadosService : Service<DiasFeriados>, IDiasFeriadosService
    {
        private readonly IDiasFeriadosRepository _repository;
        public DiasFeriadosService(IDiasFeriadosRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<List<DiasFeriados>> ConsultarYGuardarFeriados(string country, int year,int idPais)
        {
            string apiUrl = $"https://api.generadordni.es/v2/holidays/holidays?country={country}&year={year}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                List<FeriadoApiResponse> apiResponse = JsonConvert.DeserializeObject<List<FeriadoApiResponse>>(responseBody);

                List<DiasFeriados> feriados = new List<DiasFeriados>();

                foreach (var feriado in apiResponse)
                {
                    var diaFeriado = new DiasFeriados
                    {
                        Fecha = DateTime.Parse(feriado.Date),
                        Nombre = feriado.Name,
                        Pais = country,
                        Tipo = feriado.Type,
                        IdPais =idPais
                    };

                    await _repository.AddAsync(diaFeriado);
                    feriados.Add(diaFeriado);
                }

                return feriados;
            }
        }
        private class FeriadoApiResponse
        {
            public string? Date { get; set; }
            public string? End { get; set; }
            public string? Name { get; set; }
            public string? Rule { get; set; }
            public string? Start { get; set; }
            public string? Type { get; set; }
        }

    }
}

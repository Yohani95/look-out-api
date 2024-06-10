using look.Application.interfaces.oportunidad;
using look.domain.entities.oportunidad;
using look.domain.interfaces;
using look.domain.interfaces.oportunidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.oportunidad
{
    public class NovedadOportunidadService : Service<NovedadOportunidad>, INovedadOportunidadService
    {
        private readonly INovedadOportunidadRepository _oportunidadRepository;
        public NovedadOportunidadService(INovedadOportunidadRepository repository) : base(repository)
        {
            _oportunidadRepository = repository;
        }

        public async Task<List<NovedadOportunidad>> GetByIdOportunidad(int id)
        {
            try
            {
                return await _oportunidadRepository.GetByIdOportunidad(id);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}

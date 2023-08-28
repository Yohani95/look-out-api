using look.Application.interfaces.cuentas;
using look.domain.entities.cuentas;
using look.domain.interfaces;
using look.domain.interfaces.cuentas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.services.cuentas
{
    public class ClienteService : Service<Cliente>, IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IClientePersonaRepository _repositoryCliente;
        private readonly IClientePersonaService _clientePersonaService;
        public ClienteService(IClienteRepository repository, IClientePersonaService clientePersonaService) : base(repository)
        {
            _repository = repository;
            _clientePersonaService = clientePersonaService;
        }

        public async Task<ActionResult<T>> CreateWithEntities(Cliente cliente)
        {
            try
            {
                if (cliente == null)
                {
                    return BadRequest("El cliente proporcionado es nulo.");
                }

                await _repository.AddAsync(cliente);
                var personaCliente = new ClientePersona { CarId = 1 };
                await _clientePersonaService.AddAsync(personaCliente);

                // Realizar el commit de la transacción si es necesario...

                return Ok(); // O cualquier otro resultado exitoso
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        public async Task<List<Cliente>> GetAllWithEntities()
        {
            return await _repository.GetAllWithEntities();
        }
    }
}

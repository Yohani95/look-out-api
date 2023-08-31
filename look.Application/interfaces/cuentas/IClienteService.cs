using look.Application.services;
using look.domain.entities.Common;
using look.domain.entities.cuentas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.Application.interfaces.cuentas
{
    public interface IClienteService:IService<Cliente>
    {
        Task<List<Cliente>> GetAllWithEntities();
        Task<ServiceResult> CreateWithEntities(Cliente cliente,List<int> idPerson,int kamId);
        Task<ServiceResult> DeleteWithEntities(Cliente cliente);
        Task<ServiceResult> EditWithEntities(int clientId, Cliente cliente, List<int> idPersons, int kamId);
        Task<ResponseGeneric<List<int>>> GetAllIdWithContact(int clientId);
        Task<ResponseGeneric<ClienteWithIds>> GetByIdWithKamAndContact(int clientId);

    }
}

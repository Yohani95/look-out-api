using look.Application.services;
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
        Task<ServiceResult> CreateWithEntities(Cliente cliente,int idPerson);
    }
}

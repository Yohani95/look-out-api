﻿using look.domain.dto.cuentas;
using look.domain.entities.admin;
using look.domain.entities.cuentas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.interfaces.cuentas
{
    public interface IClienteRepository:IRepository<Cliente>
    {
        Task<List<Cliente>> GetAllWithEntities();
        Task<Boolean> CreateWithEntities(Cliente cliente);
        Task<List<CuentaDTO>> GetAllClientDTO();
    }
}

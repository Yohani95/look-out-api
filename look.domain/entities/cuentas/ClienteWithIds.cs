﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.cuentas
{
    public class ClienteWithIds
    {
        public Cliente Cliente { get; set; }
        public List<int>? IdPerson { get; set; }
        public int ? kamIdPerson { get; set; }

    }
}

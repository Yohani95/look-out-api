using look.domain.entities.proyecto;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.dto.NewFolder
{
    public class ProyectoDTO
    {
        public Proyecto proyecto { get;set; }
        public IFormFile file1 { get; set; }
        public IFormFile file2 { get; set; }
    }
}

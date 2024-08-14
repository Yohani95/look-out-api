using look.domain.entities.proyecto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace look.domain.entities.prospecto
{
    public class ContactoProspecto
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string? NombreCompleto { get; set; }
        [MaxLength(150)]
        public string? Email { get; set; }
        [MaxLength(20)]
        public string? Numero { get; set; }
        [MaxLength(200)]
        public string? PerfilLinkedin { get; set; }

        public ICollection<Prospecto>? Prospectos { get; set; }
    }
}

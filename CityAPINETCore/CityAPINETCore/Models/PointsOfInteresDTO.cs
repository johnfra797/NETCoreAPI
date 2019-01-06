using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore.Models
{
    public class PointsOfInteresDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Nombre es requerido")]
        [MaxLength(50, ErrorMessage = "Tamaño maximo es de 50")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Tamaño maximo es de 500")]
        public string Description { get; set; }
    }
}

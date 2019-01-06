using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore.Models
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberPointsOfInteres { get { return PointsOfInteres.Count; } }
        public ICollection<PointsOfInteresDTO> PointsOfInteres { get; set; }
    }
}

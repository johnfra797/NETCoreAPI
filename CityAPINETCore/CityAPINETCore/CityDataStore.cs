using CityAPINETCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore
{
    public class CityDataStore
    {
        public static CityDataStore Current { get; } = new CityDataStore();
        public List<CityDTO> Cities { get; set; }

        public CityDataStore()
        {
            Cities = new List<CityDTO>()
            {
                new CityDTO(){ Id=1, Name="Caracas", Description="D Caracas"},
                new CityDTO(){ Id=2, Name="Maracay", Description="D Maracay"},
                new CityDTO(){ Id=3, Name="Valencia", Description="D Valencia"}
            };
        }
    }
}

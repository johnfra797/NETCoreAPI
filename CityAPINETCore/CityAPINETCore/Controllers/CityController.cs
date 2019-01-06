using CityAPINETCore.Models;
using CityAPINETCore.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore.Controllers
{
    [Route("api/cities")]
    public class CityController : Controller
    {
        private CityInfoRepository _cityInfoRepository { get; set; }

        public CityController(CityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("")]
        public IActionResult GetCities()
        {
            // return Ok(CityDataStore.Current.Cities);

            var cities = _cityInfoRepository.GetCities();

            var result = AutoMapper.Mapper.Map<IEnumerable<CityWhithoutPointsOfInterestDTO>>(cities);

          

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInteres)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInteres);
            //var result = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);

            if (city == null)
                return NotFound();

            if (includePointsOfInteres)
            {

                var cityResult = AutoMapper.Mapper.Map<CityDTO>(city);
                
                return Ok(cityResult);


            }
            var cityWhithoutPointsOfInterestDTO = AutoMapper.Mapper.Map<CityWhithoutPointsOfInterestDTO>(city);
            
            return Ok(cityWhithoutPointsOfInterestDTO);
        }
    }
}

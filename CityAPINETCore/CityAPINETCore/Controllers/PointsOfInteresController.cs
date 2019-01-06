using CityAPINETCore.Entities;
using CityAPINETCore.Models;
using CityAPINETCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore.Controllers
{
    [Route("api/cities")]
    public class PointsOfInteresController : Controller
    {
        private CityInfoRepository _cityInfoRepository { get; set; }
        private ILogger<PointsOfInteresController> _logger;
        private IMailService _localMailService;
        public PointsOfInteresController(ILogger<PointsOfInteresController> logger, IMailService localMailService, CityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _localMailService = localMailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{idCity}/PointsOfInteres")]
        public IActionResult GetPointsOfInteres(int idCity)
        {

            var cityExist = _cityInfoRepository.CityExist(idCity);
            // var result = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == idCity);

            if (!cityExist)
                return NotFound();

            var pointsOfInteres = _cityInfoRepository.GetPointsOfInterestsForcity(idCity);

            var pointsOfInteresResult = AutoMapper.Mapper.Map<IEnumerable<PointsOfInteresDTO>>(pointsOfInteres);

            return Ok(pointsOfInteresResult);
        }

        [HttpGet("{idCity}/PointsOfInteres/{id}")]
        public IActionResult GetPointsOfInteres(int idCity, int id)
        {
            try
            {
                _localMailService.Send("Probando", "probando mensaje");

                var cityExist = _cityInfoRepository.CityExist(idCity);
                // var result = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == idCity);

                if (!cityExist)
                {
                    _logger.LogInformation($"No existe ciudad por el id {idCity}");
                    return NotFound();
                }
                else
                {
                    // var pointsOfInteres = result.ListPointsOfInteres.FirstOrDefault(x => x.Id == id);
                    var pointOfInteres = _cityInfoRepository.GetPointOfInterestForCity(idCity, id);
                    if (pointOfInteres == null)
                        return NotFound();


                    var pointsOfInteresResult = AutoMapper.Mapper.Map<PointsOfInteresDTO>(pointOfInteres);

                    return Ok(pointsOfInteresResult);
                }

            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex.ToString());
                return StatusCode(500, "Ocurrio un problema en la consulta.");
            }

        }

        [HttpGet("{idCity}/PointsOfInteres", Name = "CreationPointsOfInteres")]
        public IActionResult CreationPointsOfInteres(int idCity, [FromBody] PointsOfInteresDTO pointOfInteres)
        {
            if (pointOfInteres == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            // var city = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == idCity);
            var city = _cityInfoRepository.CityExist(idCity);


            if (!city)
                return NotFound();

            var maxPointOfInteres = CityDataStore.Current.Cities.SelectMany(x => x.PointsOfInteres).Max(p => p.Id);

            var finalPointOfInteres = AutoMapper.Mapper.Map<PointOfInterest>(pointOfInteres);

            _cityInfoRepository.AddPointOfInterestForCity(idCity, finalPointOfInteres);
            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500,"Ha ocurrido un problema al guardar.");
            }


            var finalPointOfInteresReturn  = AutoMapper.Mapper.Map<PointsOfInteresDTO>(finalPointOfInteres);

            return CreatedAtRoute("CreationPointsOfInteres", new { idCity = idCity, id = finalPointOfInteresReturn.Id }, finalPointOfInteresReturn);
        }

        [HttpPut("{idCity}/PointsOfInteres/{id}")]
        public IActionResult UpdatePointsOfInteres(int idCity, int id, [FromBody] PointsOfInteresDTO pointOfInteres)
        {
            if (pointOfInteres == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            // var city = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == idCity);

            var city = _cityInfoRepository.CityExist(idCity);
            if (!city)
                return NotFound();

            var pointsOfInteresObj = _cityInfoRepository.GetPointOfInterestForCity(idCity,id);

            if (pointsOfInteresObj == null)
                return NotFound();


            AutoMapper.Mapper.Map(pointOfInteres, pointsOfInteresObj);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Ha ocurrido un problema al guardar.");
            }

            return NoContent();
        }

        [HttpPut("{idCity}/PointsOfInteres/{id}")]
        public IActionResult DeletePointsOfInteres(int idCity, int id)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            // var city = CityDataStore.Current.Cities.FirstOrDefault(x => x.Id == idCity);

            var city = _cityInfoRepository.CityExist(idCity);
            if (!city)
                return NotFound();

            var pointsOfInteresObj = _cityInfoRepository.GetPointOfInterestForCity(idCity, id);

            if (pointsOfInteresObj == null)
                return NotFound();

            _cityInfoRepository.DeletePointOfInterest(pointsOfInteresObj);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Ha ocurrido un problema al guardar.");
            }

            return NoContent();
        }
    }
}

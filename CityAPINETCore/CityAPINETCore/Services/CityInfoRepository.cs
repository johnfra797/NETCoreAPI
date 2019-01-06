using CityAPINETCore.Entities;
using CityAPINETCore.Services.Definicion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId, false);
        }

        public bool CityExist(int cityId)
        {

            return _context.Cities.Where(o => o.Id == cityId).Any();
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterests.Remove(pointOfInterest);
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(x => x.Name).ToList();
        }

        public City GetCity(int cityId, bool includePointsOfInteres)
        {
            if (includePointsOfInteres)
            {
                return _context.Cities.Include(x => x.PointOfInteres)
                    .Where(o => o.Id == cityId).FirstOrDefault();
            }

            return _context.Cities.Where(o => o.Id == cityId).FirstOrDefault();

        }

        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInteresId)
        {
            return _context.PointOfInterests.Where(o => o.CityId == cityId && o.Id == pointOfInteresId).FirstOrDefault();

        }

        public IEnumerable<PointOfInterest> GetPointsOfInterestsForcity(int cityId)
        {
            return _context.PointOfInterests.Where(x => x.CityId == cityId).ToList();

        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}

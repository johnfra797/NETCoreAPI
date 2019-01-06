using CityAPINETCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore.Services.Definicion
{
    public interface ICityInfoRepository
    {
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointsOfInteres);
        IEnumerable<PointOfInterest> GetPointsOfInterestsForcity(int cityId);
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInteresId);
        bool CityExist(int cityId);
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        bool Save();
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityAPINETCore.Entities
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {

            if (context.Cities.Any())
            {
                return;
            }

            var cities = new List<City>()
            {
                new City(){
                    Name ="Caracas",
                    Description ="D Caracas",
                    PointOfInteres = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Name 1",
                            Description="Description 1"
                        }   ,
                        new PointOfInterest()
                        {
                            Name="Name 2",
                            Description="Description 2"
                        }
                    }
                },
                new City(){
                    Name ="Maracay",
                    Description ="D Maracay",
                    PointOfInteres = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Name 1",
                            Description="Description 1"
                        }   ,
                        new PointOfInterest()
                        {
                            Name="Name 2",
                            Description="Description 2"
                        }
                    }},
                new City(){
                    Name ="Valencia",
                    Description ="D Valencia",
                    PointOfInteres = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="Name 1",
                            Description="Description 1"
                        }   ,
                        new PointOfInterest()
                        {
                            Name="Name 2",
                            Description="Description 2"
                        }   ,
                    }
            }
        };

            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}

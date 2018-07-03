using System.Collections.Generic;

namespace SecurityWithIOT.API.Dtos
{
    public class CityForAddressDto
    {
        public int Id {get;set;}
        public string CityName {get;set;}
        public double Latitude {get;set;}
        public double Longitude {get;set;}
        public long Population {get;set;}
        public string Region {get;set;}
        public CountryForAddressDto Country {get;set;}
        public ICollection<DistrictForAddressDto> Districts {get;set;}        
    }
}
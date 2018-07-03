using System.Collections.Generic;

namespace SecurityWithIOT.API.Dtos
{
    public class CountryForAddressDto
    {
        public int Id {get;set;}
        public string CountryName { get; set; }

        public string CountryCode {get;set;}

        public ICollection<CityForAddressDto> Cities {get;set;}
    }
}
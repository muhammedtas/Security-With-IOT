namespace SecurityWithIOT.API.Dtos
{
    public class DistrictForAddressDto
    {
        public int Id {get;set;}
         public string DistrictName {get;set;}
         public CityForAddressDto City {get;set;}
    }
}
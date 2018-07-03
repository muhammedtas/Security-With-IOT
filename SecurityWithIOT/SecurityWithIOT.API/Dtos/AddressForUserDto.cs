namespace SecurityWithIOT.API.Dtos
{
    public class    AddressForUserDto
    {
        
        public int Id { get; set; }
        public string Description {get;set;}
        public CityForAddressDto City {get;set;}
        public  DistrictForAddressDto District {get;set;}

        public  CountryForAddressDto Country {get;set;}
        
    }
}
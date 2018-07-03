using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityWithIOT.API.Model
{
    [Table("Address")]
    public  class Address : BaseEntity
    {
        public string Description {get;set;}

        public int CityId {get;set;}
        public virtual City City {get;set;}

        public int DistrictId {get;set;}
        public virtual District District {get;set;}

        public int CountryId {get;set;}
        public virtual Country Country {get;set;}
        public int UserId {get;set;}
        public virtual User User {get;set;}
    }
}
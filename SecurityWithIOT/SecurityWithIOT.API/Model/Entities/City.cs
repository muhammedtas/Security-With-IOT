using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityWithIOT.API.Model
{
    [Table("City")]
    public class City: BaseEntity
    {
        public City() {
            Districts = new HashSet<District>();
        }
        public string CityName {get;set;}
        public double Latitude {get;set;}
        public double Longitude {get;set;}
        public long Population {get;set;}
        public string Region {get;set;}
        public int CountryId {get;set;}
        public virtual Country Country {get;set;}
        public virtual ICollection<District> Districts {get;set;}
    }
}
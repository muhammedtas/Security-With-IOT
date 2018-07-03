using System.Collections.Generic;

namespace SecurityWithIOT.API.Model
{
    public class Country : BaseEntity
    {
        public Country(){
            Cities = new HashSet<City>();
        }
        public string CountryName { get; set; }

        public string CountryCode {get;set;}

        public virtual ICollection<City> Cities {get;set;}
    }
}
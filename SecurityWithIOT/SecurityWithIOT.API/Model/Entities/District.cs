using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityWithIOT.API.Model
{
    [Table("District")]

    public class District : BaseEntity
    {
        public string DistrictName {get;set;}

        public int CityId {get;set;}
        public virtual City City {get;set;}
    }
}
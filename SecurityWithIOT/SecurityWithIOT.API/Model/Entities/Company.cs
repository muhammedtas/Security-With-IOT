using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityWithIOT.API.Model
{
    [Table("Company")]

    public class Company : BaseEntity
    {
        public Company() {
            Departments = new HashSet<Department>();
            Users = new HashSet<User>();
        }
        public string CompanyName {get;set;}

        public string Tel {get;set;}

        public string Fax {get;set;}

        public string Mail {get;set;}

        public virtual ICollection<Department> Departments {get;set;} 

        public virtual ICollection<User> Users {get;set;} 
    }
}
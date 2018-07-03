using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityWithIOT.API.Model
{
    public class Department : BaseEntity
    {
        public Department(){
            Users = new HashSet<User>();
        }
        public string DepartmentName { get; set; }

        public string Tel {get;set;}

        public string Fax {get;set;}

        public string Mail {get;set;}
        public  virtual ICollection<User> Users {get;set;}
        public int CompanyId{get;set;}
        public virtual Company Company {get;set;}
    }
}
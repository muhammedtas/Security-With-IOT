using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityWithIOT.API.Model
{
    [Table("User")]
    public class User : BaseEntity
    {

        public User()
        {
            Photos = new HashSet<Photo>();
            Addresses = new HashSet<Address>();
            Guid = System.Guid.Empty.ToString();
        }

        public string Username { get; set; }

        public string Firstname {get;set;}
        public string Lastname {get;set;}
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string NationalIdentificationNumber {get;set;}
        public string Gender { get; set; }
        public string Guid {get;set;}

        public string KnownAs { get; set; }
        
        public string Phone {get;set;}
        public string Email {get;set;}
        public DateTime DateOfBirth { get; set; }

        public DateTime LastEnterance { get; set; }

        public string Introduction {get;set;}

        public string Title {get;set;}

        public virtual ICollection<Address> Addresses {get;set;}

        public virtual ICollection<Photo> Photos {get;set;}
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int? CompanyId { get; set; }
        public virtual Company Company { get; set; }

    }
}
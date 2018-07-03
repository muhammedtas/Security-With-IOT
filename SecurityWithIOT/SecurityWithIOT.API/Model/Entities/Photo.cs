using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityWithIOT.API.Model
{
    [Table("Photo")]
    public class Photo : BaseEntity
    {

        public string Url {get;set;}

        public string Description {get;set;}
        public DateTime DateAdded {get;set;}

        public bool IsMain {get;set;}
        public string PublicId {get;set;}
        public int UserId {get;set;}
        public virtual User User {get;set;}

    }
}
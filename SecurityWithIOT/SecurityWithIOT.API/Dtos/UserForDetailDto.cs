using System;
using System.Collections.Generic;
using SecurityWithIOT.API.Model;

namespace SecurityWithIOT.API.Dtos
{
    public class UserForDetailDto
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Gender { get; set; }
        public string Guid {get;set;}

        public string KnownAs { get; set; }
        public string NationalIdentificationNumber { get; set; }
        public string Phone {get;set;}
        public string Email {get;set;}
        public int Age {get;set;}
        public DateTime LastEnterance { get; set; }
        public DateTime DateOfBirth {get;set;}

        public DateTime CreatedDate {get;set;}
        public string Introduction {get;set;}

        public string Title {get;set;}

        public string Adress {get;set;}
        public ICollection<AddressForUserDto> Addresses {get;set;}

        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailDto> Photos {get;set;}
        public string  DepartmentName { get; set; }
        public DepartmentDto Department { get; set; }
        public string CompanyName { get; set; }
        public CompanyDto Company { get; set; }
    }
}
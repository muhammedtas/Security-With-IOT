using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecurityWithIOT.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(10,MinimumLength = 5, ErrorMessage ="You must specify a password between 10 and 5 characters")]
        public string  Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string KnownAs { get; set; }
        //[Required]
        public ICollection<AddressForUserDto> Address { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public DateTime Created {get;set;}

        public DateTime LastActive { get; set; }

        public UserForRegisterDto() {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
            Address = new HashSet<AddressForUserDto>();
        }
    }
}
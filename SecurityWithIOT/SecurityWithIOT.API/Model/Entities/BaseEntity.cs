using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SecurityWithIOT.API.Data;

namespace SecurityWithIOT.API.Model
{
    public  abstract class BaseEntity
    {
        [Key]
        [Column(Order = 1)]
        public int Id { get; set; }

        [DefaultValue("0")]
        public bool IsDelete { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ModifiedUser { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
    }
}
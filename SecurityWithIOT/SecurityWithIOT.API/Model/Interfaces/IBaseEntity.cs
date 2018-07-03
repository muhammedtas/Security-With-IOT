using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecurityWithIOT.API.Data
{
    public interface IBaseEntity
    {
         
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("GETDATE()")]
         DateTime CreatedDate { get; set; }
         string ModifiedUser { get; set; }
         string CreatedUser { get; set; }
         DateTime? ModifiedDate { get; set; }

    }
}
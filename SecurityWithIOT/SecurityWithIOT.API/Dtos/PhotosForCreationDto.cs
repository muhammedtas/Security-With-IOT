using System;
using Microsoft.AspNetCore.Http;

namespace SecurityWithIOT.API.Dtos
{
    public class PhotosForCreationDto
    {
         public string Url {get;set;}

        public IFormFile File { get; set; }
        public string Description {get;set;}
        public DateTime DateAdded {get;set;}

        public bool IsMain {get;set;}

        public string PublicId {get;set;}

        public PhotosForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}
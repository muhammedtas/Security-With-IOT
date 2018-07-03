using System;

namespace SecurityWithIOT.API.Dtos
{
    public class PhotoForReturnDto
    {
        public string Url {get;set;}

        public string Description {get;set;}
        public DateTime DateAdded {get;set;}

        public bool IsMain {get;set;}

        public string PublicId {get;set;}

        public PhotoForReturnDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}
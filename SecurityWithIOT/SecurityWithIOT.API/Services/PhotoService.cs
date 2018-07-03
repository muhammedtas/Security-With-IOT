using SecurityWithIOT.API.Data;
using SecurityWithIOT.API.Model;
using SecurityWithIOT.API.Model.Interfaces;

namespace SecurityWithIOT.API.Services
{
    public class PhotoService: Repository<Photo> , IPhoto
    {
        public PhotoService(DataContext context) : base(context){

        }

    }
}
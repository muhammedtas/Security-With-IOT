using System.Linq;
using AutoMapper;
using SecurityWithIOT.API.Dtos;
using SecurityWithIOT.API.Model;

namespace SecurityWithIOT.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {

            AllowNullCollections = true;
            AllowNullDestinationValues = true;
            CreateMissingTypeMaps = true;

            CreateMap<Photo, PhotosForDetailDto>();

            CreateMap<Address, AddressForUserDto>();
            
            CreateMap<City, CityForAddressDto>();
            
            CreateMap<District, DistrictForAddressDto>();
            
            CreateMap<Country, CountryForAddressDto>();

            CreateMap<Company, CompanyDto>();

            CreateMap<Department, DepartmentDto>();
            
            CreateMap<User, UserForListDto>().ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.CompanyName, opt => {
                opt.MapFrom(src => src.Company.CompanyName);
            })
            .ForMember(dest => dest.DepartmentName, opt => {
                opt.MapFrom(src => src.Department.DepartmentName);
            })
            .ForMember(dest => dest.Age, opt => 
            { opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            });

            CreateMap<User, UserForDetailDto>().ForMember(dest => dest.PhotoUrl, opt => {
                opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
            })
            .ForMember(dest => dest.Company, opt => {
                opt.MapFrom(src => src.Company);
            })
            .ForMember(dest => dest.CompanyName, opt => {
                opt.MapFrom(src => src.Company.CompanyName);
            })
            .ForMember(dest => dest.Addresses, opt => {
                opt.MapFrom(src => src.Addresses);
            })
            .ForMember(dest => dest.Adress, opt => {
                opt.MapFrom(src => src.Addresses.FirstOrDefault(x=>!x.IsDelete).Description);
            })
             .ForMember(dest => dest.Department, opt => {
                opt.MapFrom(src => src.Department);
            })
            .ForMember(dest => dest.DepartmentName, opt => {
                opt.MapFrom(src => src.Department.DepartmentName);
            })
            .ForMember(dest => dest.Age, opt => 
            { opt.ResolveUsing(d => d.DateOfBirth.CalculateAge());
            });
            //  .ForMember(dest => dest.Addresses, opt => {
            //     opt.MapFrom(src => src.Addresses.FirstOrDefault(x=>!x.IsDelete).Description);
            // });
            CreateMap<UserForUpdateDto,User>();
            CreateMap<PhotosForCreationDto,Photo>();
            CreateMap<PhotoForReturnDto,Photo>();
            CreateMap<UserForRegisterDto,User>();

           
        }
        
    }
}
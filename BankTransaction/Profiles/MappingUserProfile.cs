using AutoMapper;
using BankTransaction.DTO.UserDTO;
using BankTransaction.Model.entities;

namespace BankTransaction.Profiles
{
    public class MappingUserProfile : Profile
    {
        public MappingUserProfile()
        {
            CreateMap<User, CreateUserDTO>().ReverseMap();
            CreateMap<User, UPDATEUserDTO>().ReverseMap();
            CreateMap<GETUserDTO, User>().ReverseMap();
        }
    }
}

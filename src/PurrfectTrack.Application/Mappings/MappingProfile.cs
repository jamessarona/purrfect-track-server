using AutoMapper;
using PurrfectTrack.Application.DTOs;

namespace PurrfectTrack.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pet, PetModel>();
        CreateMap<PetModel, Pet>();

        CreateMap<PetOwner, PetOwnerModel>()
            .ForMember(dest => dest.NumberOfPets, opt => opt.MapFrom(src => src.NumberOfPets))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<User, UserModel>();
    }
}

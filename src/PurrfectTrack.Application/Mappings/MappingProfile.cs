using AutoMapper;
using PurrfectTrack.Application.DTOs;

namespace PurrfectTrack.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ReverseMap();

        CreateMap<PetOwner, PetOwnerModel>()
            .ForMember(dest => dest.NumberOfPets, opt => opt.MapFrom(src => src.Pets.Count))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ReverseMap();

        CreateMap<Pet, PetModel>()
            .ForMember(dest => dest.PetOwnerId, opt => opt.MapFrom(src => src.PetOwnerId));
        CreateMap<PetModel, Pet>()
            .ForMember(dest => dest.PetOwnerId, opt => opt.MapFrom(src => src.PetOwnerId));
    }
}

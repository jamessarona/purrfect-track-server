using AutoMapper;
using PurrfectTrack.Application.DTOs;

namespace PurrfectTrack.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pet, PetModel>();
        CreateMap<PetModel, Pet>();

        CreateMap<PetOwner, PetOwnerModel>();
    }
}

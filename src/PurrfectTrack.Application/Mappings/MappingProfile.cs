// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        MappingProfile
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ReverseMap();

        CreateMap<Contact, ContactModel>().ReverseMap();

        CreateMap<PetOwner, PetOwnerModel>()
            .IncludeBase<Contact, ContactModel>()
            .ForMember(dest => dest.NumberOfPets, opt => opt.MapFrom(src => src.Pets.Count))
            .ReverseMap();

        CreateMap<Pet, PetModel>()
            .ForMember(dest => dest.PetOwnerId, opt => opt.MapFrom(src => src.PetOwnerId));
        CreateMap<PetModel, Pet>()
            .ForMember(dest => dest.PetOwnerId, opt => opt.MapFrom(src => src.PetOwnerId));

        CreateMap<Vet, VetModel>()
            .IncludeBase<Contact, ContactModel>()
            .ReverseMap();

        CreateMap<VetStaff, VetStaffModel>()
            .IncludeBase<Contact, ContactModel>()
            .ReverseMap();

        CreateMap<Appointment, AppointmentModel>();
        CreateMap<AppointmentModel, Appointment>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.PetOwner, opt => opt.Ignore())
            .ForMember(dest => dest.Pet, opt => opt.Ignore())
            .ForMember(dest => dest.Vet, opt => opt.Ignore())
            .ForMember(dest => dest.VetStaff, opt => opt.Ignore());

        CreateMap<Company, CompanyModel>();
    }
}
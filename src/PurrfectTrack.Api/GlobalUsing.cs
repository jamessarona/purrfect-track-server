// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GlobalUsing
//  Created:     5/16/2025 7:32:19 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

global using HealthChecks.UI.Client;

global using MediatR;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.OpenApi.Models;

global using PurrfectTrack.Api;
global using PurrfectTrack.Application;
global using PurrfectTrack.Application.Companies.Commands.CreateCompany;
global using PurrfectTrack.Application.Companies.Commands.DeleteCompany;
global using PurrfectTrack.Application.Companies.Commands.UpdateCompany;
global using PurrfectTrack.Application.Companies.Queries.GetCompanies;
global using PurrfectTrack.Application.Companies.Queries.GetCompanyById;
global using PurrfectTrack.Application.Mappings;
global using PurrfectTrack.Application.PetOwners.Commands.CreatePetOwner;
global using PurrfectTrack.Application.PetOwners.Commands.DeletePetOwner;
global using PurrfectTrack.Application.PetOwners.Commands.UpdatePetOwner;
global using PurrfectTrack.Application.PetOwners.Commands.UploadPetOwnerImage;
global using PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerById;
global using PurrfectTrack.Application.PetOwners.Queries.GetPetOwnerByPet;
global using PurrfectTrack.Application.PetOwners.Queries.GetPetOwners;
global using PurrfectTrack.Application.Pets.Commands.CreatePet;
global using PurrfectTrack.Application.Pets.Commands.DeletePet;
global using PurrfectTrack.Application.Pets.Commands.UpdatePet;
global using PurrfectTrack.Application.Pets.Commands.UploadPetImage;
global using PurrfectTrack.Application.Pets.Queries.GetPetById;
global using PurrfectTrack.Application.Pets.Queries.GetPets;
global using PurrfectTrack.Application.Pets.Queries.GetPetsByOwner;
global using PurrfectTrack.Application.Users.Commands.Login;
global using PurrfectTrack.Application.Users.Commands.Logout;
global using PurrfectTrack.Application.Users.Commands.RefreshTokenFeature;
global using PurrfectTrack.Application.Users.Queries.GetCurrentUser;
global using PurrfectTrack.Application.Users.Queries.GetUserById;
global using PurrfectTrack.Application.Users.Queries.GetUsers;
global using PurrfectTrack.Application.Users.Queries.GetUsersByRole;
global using PurrfectTrack.Application.Vets.Commands.CreateVet;
global using PurrfectTrack.Application.Vets.Commands.DeleteVet;
global using PurrfectTrack.Application.Vets.Commands.UpdateVet;
global using PurrfectTrack.Application.Vets.Queries.GetVetById;
global using PurrfectTrack.Application.Vets.Queries.GetVets;
global using PurrfectTrack.Application.Vets.Queries.GetVetsByCompany;
global using PurrfectTrack.Application.VetStaffs.Commands.CreateVetStaff;
global using PurrfectTrack.Application.VetStaffs.Commands.DeleteVetStaff;
global using PurrfectTrack.Application.VetStaffs.Commands.UpdateVetStaff;
global using PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffById;
global using PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffs;
global using PurrfectTrack.Application.VetStaffs.Queries.GetVetStaffsByCompany;

global using PurrfectTrack.Infrastructure;
global using PurrfectTrack.Infrastructure.Data.Extensions;

global using PurrfectTrack.Shared.Exceptions.Handler;
global using PurrfectTrack.Shared.Pagination;

global using System.Diagnostics;
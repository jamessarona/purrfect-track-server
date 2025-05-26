// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UserDetailModel
//  Created:     5/26/2025 9:49:48 PM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace PurrfectTrack.Application.DTOs;

public class UserDetailModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = default!;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PetOwnerModel? PetOwner { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VetModel? Vet { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public VetStaffModel? VetStaff { get; set; }
}
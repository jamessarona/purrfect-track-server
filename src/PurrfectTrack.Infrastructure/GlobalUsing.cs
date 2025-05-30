// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GlobalUsing
//  Created:     5/17/2025 12:54:12 AM
//
//  This file is part of the PurrfectTack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.IdentityModel.Tokens;
global using PurrfectTrack.Application.Abstractions;
global using PurrfectTrack.Application.Data;
global using PurrfectTrack.Domain.Abstractions;
global using PurrfectTrack.Domain.Entities;
global using PurrfectTrack.Domain.Enums;
global using PurrfectTrack.Infrastructure.Data;
global using PurrfectTrack.Infrastructure.Extensions;
global using PurrfectTrack.Infrastructure.Interceptors;
global using PurrfectTrack.Infrastructure.Security;
global using PurrfectTrack.Infrastructure.Services;
global using PurrfectTrack.Shared.Security;
global using System.Diagnostics;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;
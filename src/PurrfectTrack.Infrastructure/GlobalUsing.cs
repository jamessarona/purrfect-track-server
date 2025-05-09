﻿global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.ChangeTracking;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.IdentityModel.Tokens;
global using PurrfectTrack.Application.Abstractions;
global using PurrfectTrack.Application.Data;
global using PurrfectTrack.Domain.Abstractions;
global using PurrfectTrack.Domain.Entities;
global using PurrfectTrack.Infrastructure.Extensions;
global using PurrfectTrack.Infrastructure.Data;
global using PurrfectTrack.Infrastructure.Interceptors;
global using PurrfectTrack.Infrastructure.Security;
global using PurrfectTrack.Shared.Security;
global using System.Reflection;
global using System.Text;
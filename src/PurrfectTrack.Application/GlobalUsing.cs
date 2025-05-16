// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GlobalUsing
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

global using AutoMapper;
global using AutoMapper.QueryableExtensions;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Infrastructure;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.FeatureManagement;
global using PurrfectTrack.Application.Abstractions;
global using PurrfectTrack.Application.Data;
global using PurrfectTrack.Application.DTOs;
global using PurrfectTrack.Application.Exceptions;
global using PurrfectTrack.Application.Utils;
global using PurrfectTrack.Domain.Entities;
global using PurrfectTrack.Domain.Enums;
global using PurrfectTrack.Infrastructure.Caching;
global using PurrfectTrack.Shared.Behaviors;
global using PurrfectTrack.Shared.CQRS;
global using PurrfectTrack.Shared.Exceptions;
global using PurrfectTrack.Shared.Pagination;
global using PurrfectTrack.Shared.Security;
global using System.ComponentModel.DataAnnotations;
global using System.Reflection;
global using System.Security.Cryptography;
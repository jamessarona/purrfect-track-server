using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurrfectTrack.Application.Abstractions;

public interface IImageStorageService
{
    Task<string> SaveImageAsync(IFormFile file, string subFolder, CancellationToken cancellationToken = default);
}
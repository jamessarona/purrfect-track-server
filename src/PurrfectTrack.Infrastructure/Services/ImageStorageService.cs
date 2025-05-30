﻿// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        ImageStorageService
//  Created:     5/17/2025 12:54:12 AM
//
//  This file is part of the PurrfectTack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Infrastructure.Services;

public class ImageStorageService : IImageStorageService
{
    private readonly IWebHostEnvironment _env;

    public ImageStorageService(IWebHostEnvironment env)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
        if (string.IsNullOrEmpty(_env.WebRootPath))
        {
            _env.WebRootPath = Path.Combine(_env.ContentRootPath, "wwwroot");
        }
    }

    public async Task<string> SaveImageAsync(IFormFile file, string subFolder, CancellationToken cancellationToken = default)
    {
        EnsureUploadDirectory(subFolder);

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", subFolder);
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return $"/uploads/{subFolder}/{fileName}";
    }

    private void EnsureUploadDirectory(string folder)
    {
        var path = Path.Combine(_env.WebRootPath, "uploads", folder);
        Directory.CreateDirectory(path);

        if (OperatingSystem.IsMacOS() || OperatingSystem.IsLinux())
        {
            try
            {
                var chmod = new ProcessStartInfo
                {
                    FileName = "chmod",
                    Arguments = $"-R 775 \"{path}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(chmod);
                process?.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[WARN] Could not chmod uploads folder: {ex.Message}");
            }
        }
    }
}
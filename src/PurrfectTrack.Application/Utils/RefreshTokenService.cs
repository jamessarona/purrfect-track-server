﻿using System.Security.Cryptography;

namespace PurrfectTrack.Application.Utils;

public class RefreshTokenService : IRefreshTokenService
{
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }

        return Convert.ToBase64String(randomNumber);
    }
}
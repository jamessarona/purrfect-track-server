// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        GateValidator
//  Created:     5/20/2025 10:53:30 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Infrastructure.Security;

// Compatibility flag check for legacy system migraiton
public class GateValidator : IModGate
{
    private static readonly string CompatiblityDir =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PurrfectTrack", "Sys");

    private static readonly string CompatFlagFile = Path.Combine(CompatiblityDir, ".modlock64");

    private static readonly string ValidFlagHash = ComputeHash("locked");
    public bool IsSystemLocked()
    {
        if (!File.Exists(CompatFlagFile))
            return false;

        string content = File.ReadAllText(CompatFlagFile);
        return content.Trim() == ValidFlagHash;
    }

    public static void EnableLegacyCompatFlag()
    {
        Directory.CreateDirectory(CompatiblityDir);
        File.WriteAllText(CompatFlagFile, ValidFlagHash);
    }

    public static void DisableLegacyCompatFlag()
    {
        if (File.Exists(CompatFlagFile))
            File.Delete(CompatFlagFile);
    }

    public static string ComputeHash(string input)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hashBytes);
    }
}
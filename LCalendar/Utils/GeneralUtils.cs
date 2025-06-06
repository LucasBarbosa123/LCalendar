using System.Security.Cryptography;
using LCalendar.Dtos;

namespace LCalendar.Utils;

public static class GeneralUtils
{
    // Hash password and return the result as a combined string: {salt}:{hash}
    public static string HashPassword(string password)
    {
        // Generate a 128-bit salt using a secure PRNG
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        // Derive a 256-bit subkey (hash) using PBKDF2 with HMAC-SHA256
        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(32); // 256-bit hash

        // Return salt and hash as a base64 string, separated by ':'
        return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
    }

    // Verify a password against a stored hash
    public static bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split(':');
        if (parts.Length != 2)
            return false;

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] hash = Convert.FromBase64String(parts[1]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100000, HashAlgorithmName.SHA256);
        byte[] computedHash = pbkdf2.GetBytes(32);

        // Compare hashes in constant time
        return CryptographicOperations.FixedTimeEquals(hash, computedHash);
    }
    
    public static string GeneratePassword(int length = 8)
    {
        if (length < 8)
            throw new ArgumentException("Password length should be at least 8 characters.");

        const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lower = "abcdefghijklmnopqrstuvwxyz";
        const string digits = "0123456789";
        const string special = "!@#$%^&*()-_=+[]{}|;:,.<>?";
        string allChars = upper + lower + digits + special;

        var random = RandomNumberGenerator.Create();
        var result = new char[length];

        // Ensure at least one of each required character type
        result[0] = upper[GetRandomIndex(random, upper.Length)];
        result[1] = lower[GetRandomIndex(random, lower.Length)];
        result[2] = digits[GetRandomIndex(random, digits.Length)];
        result[3] = special[GetRandomIndex(random, special.Length)];
        
        for (int i = (length-4); i < length; i++)
        {
            result[i] = allChars[GetRandomIndex(random, allChars.Length)];
        }

        // Shuffle to avoid predictable character placement
        return new string(result.OrderBy(_ => GetRandomIndex(random, int.MaxValue)).ToArray());
    }

    private static int GetRandomIndex(RandomNumberGenerator rng, int maxExclusive)
    {
        byte[] data = new byte[4];
        int value;
        do
        {
            rng.GetBytes(data);
            value = BitConverter.ToInt32(data, 0) & int.MaxValue; // Ensure non-negative
        } while (value >= maxExclusive * (int.MaxValue / maxExclusive));

        return value % maxExclusive;
    }

    //basically we allways need some buttons for the datatables's tables so we generate them here
    public static string GetTableEditDeleteButtons(int id, string delFuncName, string edtFuncName)
    {
        return $"<div class=\"d-flex justify-content-end\">" +
               $"<button type=\"button\" class=\"btn bg-primary-subtle rounded-circle round-40 d-inline-flex align-items-center justify-content-center me-2\" onclick=\"{edtFuncName}('{id}')\"> <iconify-icon icon=\"solar:pen-new-round-broken\" class=\"nav-small-cap-icon fs-6\"></iconify-icon> </button>" +
               $"<button type=\"button\" class=\"btn bg-danger-subtle text-danger rounded-circle round-40 d-inline-flex align-items-center justify-content-center\" onclick=\"{delFuncName}('{id}')\"> <iconify-icon icon=\"solar:trash-bin-2-broken\" class=\"nav-small-cap-icon fs-6\"></iconify-icon> </button>" +
               $"</div>";
    }
    public static string ConvertToTableButtonsDivStr(this List<TableButton> tableButtons)
    {
        var strartDivTag = "<div class=\"d-flex justify-content-end\">";
        var endtDivTag = "</div>";

        var buttons = "";
        for (int i = 0; i < tableButtons.Count; i++)
        {
            var tableButton = tableButtons[i];
            bool isLast = (i == tableButtons.Count - 1);

            if (isLast)
            {
                buttons += $" {tableButton.ToString("me-2")} ";
            }
            else
            {
                buttons += $" {tableButton.ToString()} ";
            }
        }
        
        return $"{strartDivTag}{buttons}{endtDivTag}";
    }
}
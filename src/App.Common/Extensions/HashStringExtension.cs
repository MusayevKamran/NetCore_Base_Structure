using System.Security.Cryptography;
using System.Text;
using App.Common.Enums;

namespace App.Common.Extensions;

/// <summary>
/// Extension for working with hash string
/// </summary>
public static class HashStringExtension
{
    /// <summary>
    ///     Create HashData by <paramref name="hashType" />
    /// </summary>
    public static string? GetHash(this string text, HashType hashType)
    {
        if (string.IsNullOrEmpty(text))
            return null;

        switch (hashType)
        {
            case HashType.MD5:
            {
                using var algorithm = MD5.Create();
                return CalculateHashByAlgorithm(algorithm, text);
            }
            case HashType.SHA1:
            {
                using var algorithm = SHA1.Create();
                return CalculateHashByAlgorithm(algorithm, text);
            }
            case HashType.SHA256:
            {
                using var algorithm = SHA256.Create();
                return CalculateHashByAlgorithm(algorithm, text);
            }
            case HashType.SHA512:
            {
                using var algorithm = SHA512.Create();
                return CalculateHashByAlgorithm(algorithm, text);
            }
            default:
                return null;
        }
    }

    /// <summary>
    ///     Check HashData
    /// </summary>
    public static bool CheckHash(this string original, string hash, HashType hashType) => GetHash(original, hashType) == hash;

    /// <summary>
    ///     Get hash data by algorithm
    /// </summary>
    /// <param name="algorithm">Hash algoritm</param>
    /// <param name="text">Input string</param>
    private static string CalculateHashByAlgorithm(HashAlgorithm algorithm, string text)
    {
        var builder = new StringBuilder();
        var hash = algorithm.ComputeHash(Encoding.ASCII.GetBytes(text));

        foreach (var t in hash)
            builder.Append(t.ToString("X2"));

        return builder.ToString();
    }
}
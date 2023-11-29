using System.Diagnostics.CodeAnalysis;

namespace Application.Common.Helpers;

public static class Base64Helper
{
    /// <summary>
    /// Check if the string is in valid base64 format.
    /// </summary>
    /// <param name="messageBase64">The string in base64 format.</param>
    /// <returns><c>True</c> if the string successfully decoded, otherwise <c>false</c>.</returns>
    public static bool IsValidBase64String(string messageBase64)
    {
        var buffer = new Span<byte>(new byte[messageBase64.Length]);
        var isValid = Convert.TryFromBase64String(messageBase64, buffer, out _);
        return isValid;
    }

    /// <summary>
    /// Decodes the image and throws an exception if the string is in invalid format.
    /// </summary>
    /// <param name="imageBase64">Image in base64 string format.</param>
    /// <returns><c>null</c> if the image is empty or null, otherwise decoded image.</returns>
    /// <exception cref="ArgumentException">Image is not in valid base64 string format.</exception>
    [return: NotNullIfNotNull(nameof(imageBase64))]
    public static byte[]? ConvertImageFromBase64(string? imageBase64)
    {
        if (string.IsNullOrEmpty(imageBase64))
        {
            return null;
        }

        try
        {
            var image = Convert.FromBase64String(imageBase64);
            return image;
        }
        catch (FormatException)
        {
            throw new ArgumentException("Фото не в формате base64.");
        }
    }
}
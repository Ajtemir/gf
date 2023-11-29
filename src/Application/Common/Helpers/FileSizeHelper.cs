namespace Application.Common.Helpers;

public static class FileSizeHelper
{
    public const int Megabyte = 1048576;
    
    public static int ToMegabytes(int megabytes)
    {
        return megabytes * Megabyte;
    }
}
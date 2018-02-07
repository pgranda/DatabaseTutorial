using System.Text;
using System.Security.Cryptography;

public class HashHelper
{
    public static byte[] GetHash(string inputString)
    {
        HashAlgorithm algorithm = MD5.Create();
        return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
    }

    public static string GetHashString(string inputString)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (byte b in GetHash(inputString))
        {
            stringBuilder.Append(b.ToString("X2"));
        }
        return stringBuilder.ToString();
    }
}

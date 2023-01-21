using System.Text;

namespace Yeeter.Common;
public class IdGenerator
{
    private const int ID_GENERATOR_LENGTH = 7;

    public static string Generate()
    {
        var id = new StringBuilder(ID_GENERATOR_LENGTH);
        var random = new Random();
        string allOptions = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (int i = 0; i < 7; i++)
        {
            id.Append(allOptions[random.Next(allOptions.Length)]);
        }
        return id.ToString();
    }
}

using System.Text;

namespace Yeeter.Common;
public class IdGenerator
{
    private const int ID_GENERATOR_LENGTH = 7;
    private const string ALL_OPTIONS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    public static string Generate()
    {
        var id = new StringBuilder(ID_GENERATOR_LENGTH);
        var random = new Random();

        for (int i = 0; i < 7; i++)
        {
            id.Append(ALL_OPTIONS[random.Next(ALL_OPTIONS.Length)]);
        }
        return id.ToString();
    }

    public static bool IsValidId(string id)
    {
        if (id.Length != ID_GENERATOR_LENGTH)
            return false;
        if (!id.All(_ => ALL_OPTIONS.Contains(_)))
            return false;

        return true;
    }
}


namespace Yeeter.Common;

public static class InputSanitation
{
    public static int SanitizeCount(int? count, int minCount = 1, int maxCount = 20)
    {
        if (count.HasValue && count.Value >= minCount)
            count = count.Value > maxCount ? maxCount : count;
        else
            count = 5;

        return count.Value;
    }
}
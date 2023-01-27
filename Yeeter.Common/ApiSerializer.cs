using Newtonsoft.Json;

namespace Yeeter.Common;

public static class ApiSerializer
{
    public static string SerializeForMapGet<T>(T value)
    {
        var jsonSerializer = JsonSerializer.Create(new JsonSerializerSettings()
        {
            //PreserveReferencesHandling = PreserveReferencesHandling.Objects
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        });
        using (var sw = new StringWriter())
        using (var jw = new JsonTextWriter(sw))
        {
            jsonSerializer.Serialize(jw, value);
            return sw.ToString();
        }
    }
}
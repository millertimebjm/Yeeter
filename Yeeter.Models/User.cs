using Yeeter.Common;

namespace Yeeter.Models;

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Handle { get; set; }
    public DateTime CreatedDate { get; set; }
    public IEnumerable<Yeet> Yeets { get; set; }

    public User() { }

    public User(
        string name,
        string handle)
    {
        Id = IdGenerator.Generate();
        Name = name;
        Handle = handle;
    }
}
using Yeeter.Common;

namespace Yeeter.Models;
public class Yeet
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string UserId { get; set; }
    public DateTime CreatedDate { get; set; }
    public User User { get; set; }

    public Yeet() { }

    public Yeet(
        string text,
        string userId)
    {
        Id = IdGenerator.Generate();
        Text = text;
        UserId = userId;
        CreatedDate = DateTime.UtcNow;
    }
}

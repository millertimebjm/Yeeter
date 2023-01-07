namespace Yeeter.Models;
public class Yeet
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string Name { get; set; }
    public string Handle { get; set; }

    public Yeet(
        string text,
        string name,
        string handle)
    {
        var random = new Random();
        string allOptions = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (int i = 0; i < 7; i++)
        {
            Id += allOptions[random.Next(allOptions.Length)];
        }
        Text = text;
        Name = name;
        Handle = handle;
    }
}

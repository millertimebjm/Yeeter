namespace Yeeter.Models;
public class Yeet
{
    public string Id { get; set; }
    public string Text { get; set; }

    public Yeet(string text)
    {
        var random = new Random();
        string allOptions = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        for (int i = 0; i < 7; i++)
        {
            Id += allOptions[random.Next(allOptions.Length)];
        }
        Text = text;
    }
}

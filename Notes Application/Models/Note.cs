namespace Notes_Application.Models;

public class Note
{
    public Note(string initial)
    {
        this.Content = initial;
    }

    public string Content { get; set; }
}

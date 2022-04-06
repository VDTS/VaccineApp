namespace Core.Models;

public class AnnouncementModel
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public DateTime MessageDateTime { get; set; }
    public AnnouncementModel()
    {
        Id = Guid.NewGuid();
        MessageDateTime = DateTime.UtcNow;
    }
}

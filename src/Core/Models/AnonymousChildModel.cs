namespace Core.Models;

public class AnonymousChildModel
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public DateTime DOB { get; set; }
    public string Gender { get; set; }
    public string ChildType { get; set; }
    public bool IsVaccined { get; set; }

    public AnonymousChildModel()
    {
        Id = Guid.NewGuid();
    }
}

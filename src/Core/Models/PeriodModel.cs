namespace Core.Models;

public class PeriodModel
{
    public Guid Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string? PeriodName { get; set; }
    public bool IsActive { get; set; }

    public PeriodModel()
    {
        Id = Guid.NewGuid();
        StartDate = DateTime.UtcNow;
        EndDate = DateTime.UtcNow;
        IsActive = true;
    }
}

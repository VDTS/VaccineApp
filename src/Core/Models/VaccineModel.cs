namespace Core.Models;

public class VaccineModel
{
    public string FId { get; set; }
    public Guid Id { get; set; }
    public string Period { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }
    public VaccineModel()
    {
        Id = Guid.NewGuid();
    }
}

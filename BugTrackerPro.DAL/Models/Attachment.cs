namespace BugTrackerPro.DAL;

public class Attachment
{
    public Guid Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public int BugId { get; set; }
    public Bug Bug { get; set; }

}
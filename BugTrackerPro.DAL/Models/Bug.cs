
namespace BugTrackerPro.DAL;

public class Bug
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid ProjectId { get; set; }
    public Project Project { get; set; }
    public ICollection<BugAssignee> Assignees { get; set; }
    public ICollection<Attachment> Attachments { get; set; }

}
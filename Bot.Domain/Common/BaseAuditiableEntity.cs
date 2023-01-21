namespace Bot.Domain.Common;

public abstract class BaseAuditiableEntity : BaseEntity
{
    public DateTime Created { get; set; }
    public string CreateBy { get; set; }
    public DateTime LastModified { get; set; }
    public string LastModifiedBy { get; set; }
}
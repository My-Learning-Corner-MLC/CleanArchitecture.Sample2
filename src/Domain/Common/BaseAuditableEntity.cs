namespace Sample2.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; } = DateTime.Now;

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; } = DateTime.Now;

    public string? LastModifiedBy { get; set; }

    public bool IsDeleted { get; set; } = false;
}

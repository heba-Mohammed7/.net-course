namespace cleanArchitecture.Domain.Models.Base;

public interface ISoftDeletableEntity
{
    public bool IsDeleted { get; set; }
}
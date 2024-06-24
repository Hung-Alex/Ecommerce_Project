namespace Domain.Common
{
    public interface IDatedModification
    {
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
    }
}

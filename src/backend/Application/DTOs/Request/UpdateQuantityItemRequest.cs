namespace Application.DTOs.Request
{
    public record UpdateQuantityItemRequest(Guid CartItemId,int quantity);
}

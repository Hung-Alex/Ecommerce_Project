

namespace Application.DTOs.Request
{
    public record CartItemRequest(Guid ProductId,Guid ProductSkusId,int quantity);
}

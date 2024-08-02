using Domain.Enums;

namespace Application.DTOs.Request
{
    public class InfoOrderRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}

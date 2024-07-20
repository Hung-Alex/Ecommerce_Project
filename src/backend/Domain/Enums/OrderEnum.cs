namespace Domain.Enums
{
    public static class OrderEnum
    {
        public enum OrderStatus
        {
            Unknown = 0,
            Pending = 1,
            Processing = 2,
            Shipping = 3,
            Shipped = 4,
            Delivered = 5,
            Cancelled = 6,
            ShippingFailed = 7,
        }
    }
}

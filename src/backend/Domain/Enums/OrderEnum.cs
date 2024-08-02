namespace Domain.Enums
{
    public static class OrderEnum
    {
        public enum OrderStatus
        {
            Pending ,
            TransactionSuccess,
            Processing,
            Shipping,
            Shipped,
            Delivered,
            Cancelled,
            ShippingFailed,
        }
    }
}



namespace Domain.Constants
{
    public static class StateConstants
    {
        public const string OrderType = "Order";
        public const string PaymentType = "Payment";

        public static class OrderState
        {
            public const string Pending = "PENDING";
            public const string Processing = "PROCESSING";
            public const string Completed = "COMPLETED";
            public const string Cancelled = "CANCELLED";
        }

        public static class PaymentState
        {
            public const string Pending = "PENDING";
            public const string Processing = "PROCESSING";
            public const string Completed = "COMPLETED";
            public const string Cancelled = "CANCELLED";
        }
    }
}

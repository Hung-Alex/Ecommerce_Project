
namespace Infrastructure.PaymentService.VnPay.Request
{

    //Url : Hướng dẫn tích hợp , Cái củ nợ này  => ^_^ https://sandbox.vnpayment.vn/apis/docs/thanh-toan-pay/pay.html
    /// <summary>
    /// Lười nên ghi trong này cho dỡ mở web lên xem
    /// </summary>
    public class VnPayRequest
    {
        public string Vnp_Version { get; set; } = "2.1.0";//chữ và số  => Phiên bản api mà merchant kết nối. Phiên bản hiện tại là : 2.1.0
        public string Vnp_Command { get; set; } = "pay";//chữ => Mã API sử dụng, mã cho giao dịch thanh toán là: pay
        public string Vnp_TmnCode { get; set; }//chữ và số => Mã website của merchant trên hệ thống của VNPAY. Được cấp khi merchant đăng ký tích hợp  Ví dụ: 2QXUI4J4
        public string Vnp_Amount { get; set; }//số => Số tiền thanh toán. Số tiền không mang các ký tự phân tách thập phân, phần nghìn, ký tự tiền tệ. Để gửi số tiền thanh toán là 10,000 VND (mười nghìn VNĐ) thì merchant cần nhân thêm 100 lần (khử phần thập phân), sau đó gửi sang VNPAY là: 1000000
        public long Vnp_CreateDate { get; set; }//số => Là thời gian phát sinh giao dịch định dạng yyyyMMddHHmmss (Time zone GMT+7) Ví dụ: 20220101103111
        public string Vnp_CurrCode { get; set; } = "VND";// chữ => Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
        public string Vnp_IpAddr { get; set; }//chữ và số => Địa chỉ IP của máy khách hàng thực hiện giao dịch
        public string Vnp_Locale { get; set; }//chữ => Ngôn ngữ hiển thị trên cổng thanh toán. Hiện tại hỗ trợ 2 ngôn ngữ: tiếng Việt (vn) và tiếng Anh (en)
        public string Vnp_OrderInfo { get; set; }//chữ => Thông tin mô tả nội dung thanh toán quy định dữ liệu gửi sang VNPAY (Tiếng Việt không dấu và không bao gồm các ký tự đặc biệt)Ví dụ: Nap tien cho thue bao 0123456789. So tien 100,000 VND
        public string Vnp_OrderType { get; set; }//chữ => Mã danh mục hàng hóa. Mỗi hàng hóa sẽ thuộc một nhóm danh mục do VNPAY quy định. Xem thêm bảng Danh mục hàng hóa
        public string Vnp_ReturnUrl { get; set; }//chữ => Địa chỉ trả kết quả giao dịch về cho website của merchant. Địa chỉ này phải là địa chỉ tuyệt đối (URL) Ví dụ: https://abc.com/vnpay_return
        public long Vnp_ExpireDate { get; set; }//số => Thời gian hết hạn của giao dịch. Định dạng yyyyMMddHHmmss (Time zone GMT+7) Ví dụ: 20220101103111
        public string Vnp_TxnRef { get; set; }//chữ và số => Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày. Ví dụ: 23554
        public string Vnp_SecureHash { get; set; }//Mã kiểm tra (checksum) để đảm bảo dữ liệu của giao dịch không bị thay đổi trong quá trình chuyển từ merchant sang VNPAY. Việc tạo ra mã này phụ thuộc vào cấu hình của merchant và phiên bản api sử dụng. Phiên bản hiện tại hỗ trợ SHA256, HMACSHA512.
    }
}
//http://sandbox.vnpayment.vn/paymentv2/vpcpay.html?vnp_Amount=100000&vnp_Command=pay&vnp_CreateDate=20240731220312&vnp_CurrCode=VND&vnp_ExpireDate=20240731221812&vnp_IpAddr=127.0.0.1&vnp_Locale=vn&vnp_OrderInfo=thanh toan don hang&vnp_OrderType=other&vnp_ReturnUrl=https://localhost:5001/api/payment/vnpay-return&vnp_SecureHash=a2e437dbfda227b08780776c7acede49298fa9ef46a0d4b9b04da1ffeb4ad9588d1f5661e32931d597177b6cbecce6298a02da9842b830898b7ec8151b82eb0e&vnp_TmnCode=&vnp_TxnRef=e410bab1-a9f7-40be-9fcd-1e5286de460f&vnp_Version=2.1.0

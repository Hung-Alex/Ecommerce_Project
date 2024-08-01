using Application.Common.Interface.Payment;
using Application.DTOs.Responses.Payments;
using Domain.Entities.Orders;
using Domain.Shared;
using Infrastructure.PaymentService.VnPay.Config;
using Infrastructure.PaymentService.VnPay.Lib;
using Infrastructure.PaymentService.VnPay.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Infrastructure.PaymentService.VnPay.Handle
{
    public class VnPayStrategy : IPaymentStrategy
    {
        private readonly VnPaySetting _vnpaySetting;
        private readonly IConfiguration _configuration;
        public VnPayStrategy(IConfiguration configuration)
        {
            _configuration = configuration;
            _vnpaySetting = _configuration.GetSection("vnPay").Get<VnPaySetting>();
        }

        public async Task<Result<PaymentsResultDTO>> CreatePaymentUrl(Order order, CancellationToken cancellationToken)
        {
            VnPayRequest vnPayRequest = new VnPayRequest
            {
                Vnp_TmnCode = _vnpaySetting.Vnp_TmnCode,
                Vnp_Amount = (10000*100).ToString(), // Chuyển đổi số tiền sang đơn vị nhỏ nhất (đồng)
                Vnp_CreateDate = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmss")),
                Vnp_IpAddr = "127.0.0.1",
                Vnp_Locale = "vn",
                Vnp_OrderInfo = "thanhtoandonhang".ToString(),
                Vnp_OrderType = "other",
                Vnp_ReturnUrl = _vnpaySetting.Vnp_Returnurl,
                Vnp_ExpireDate = long.Parse(DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss")),
                Vnp_TxnRef = Guid.NewGuid().ToString()
            };
            var pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.1.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", _vnpaySetting.Vnp_TmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", vnPayRequest.Vnp_Amount); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", vnPayRequest.Vnp_IpAddr); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", vnPayRequest.Vnp_OrderInfo); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "other"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", _vnpaySetting.Vnp_Returnurl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", vnPayRequest.Vnp_TxnRef); //mã hóa đơn
            string paymentUrl = pay.CreateRequestUrl(_vnpaySetting.Vnp_Url, _vnpaySetting.Vnp_HashSecret);
            return Result<PaymentsResultDTO>.ResultSuccess(new PaymentsResultDTO
            {
                PaymentUrl = paymentUrl
            }); 
        }
    }
}







using Application.Features.Payments.Commands;
using Domain.Entities.Posts;
using Infrastructure.PaymentService.VnPay.Config;
using Infrastructure.PaymentService.VnPay.Lib;
using Infrastructure.PaymentService.VnPay.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly VnPaySetting _vnPaySetting;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
            _vnPaySetting = _configuration.GetSection("vnPay").Get<VnPaySetting>();
        }
        /// <summary>
        /// Handles IPN (Instant Payment Notification) from VnPay
        /// </summary>
        /// <param name="vnpayResponse">The VnPay response</param>
        /// <returns>An empty result indicating success</returns>
        [HttpGet("ipn-url-vnpay")]
        public async Task<IActionResult> IPNVnPay([FromQuery] VnpayResponse vnpayResponse)
        {
            var queryString = Request.QueryString.Value;
            var pos = Request.QueryString.Value.IndexOf("&vnp_SecureHash");
            bool checkSignature = PayLib.ValidateSignature(Request.QueryString.Value.Substring(1, pos - 1), vnpayResponse.vnp_SecureHash, _vnPaySetting.Vnp_HashSecret); //check chữ ký đúng hay không?
            if (!checkSignature)
            {
                await Console.Out.WriteLineAsync("sai chu ki");
            }
            var result = await _mediator.Send(new IPNVnPayCommand() { Code = vnpayResponse.vnp_ResponseCode, OrderId = Guid.Parse(vnpayResponse.vnp_TxnRef), TransactionId = vnpayResponse.vnp_TransactionNo, Message = vnpayResponse.vnp_TransactionStatus });
            return Ok();
        }


    }
}

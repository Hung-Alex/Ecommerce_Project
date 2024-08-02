using Application.Features.Payments.Commands;
using Infrastructure.PaymentService.VnPay.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController:ControllerBase
    {
        private readonly IMediator mediator;
        public PaymentController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpGet("ipn-url-vnpay")]
        public async Task<IActionResult> IPNVnPay([FromQuery] VnpayResonse vnpayResponse)
        {
            var result = await mediator.Send(new IPNVnPayCommand() { Code=vnpayResponse.vnp_ResponseCode,OrderId=Guid.Parse(vnpayResponse.vnp_TxnRef),TransactionId=vnpayResponse.vnp_TransactionNo,Message=vnpayResponse.vnp_TransactionStatus});
            return Ok();
        }
    }
}

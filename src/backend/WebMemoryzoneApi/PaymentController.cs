using Infrastructure.PaymentService.VnPay.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebMemoryzoneApi
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PaymentController(IMediator mediator)
        {
            _mediator = mediator;

        }
        [HttpGet("ipn-vnpay")]
        public async Task<IActionResult> CreatePayment([FromQuery] VnpayResonse vnpayResonse)
        {
            await Console.Out.WriteLineAsync("da vao");
            return Ok();
            //var result = await _mediator.Send(command);
            //if (!result.IsSuccess)
            //{
            //    return BadRequest(result);
            //}
            //return Ok(result);
        }
    }
}

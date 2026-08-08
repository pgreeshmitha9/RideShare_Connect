using Microsoft.AspNetCore.Mvc;
using RideShare_Connect.DTOs;
using RideShare_Connect.Services;
using System.Security.Claims;

namespace RideShare_Connect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentProcessDto dto)
        {
            if (string.IsNullOrEmpty(dto.PaymentMode))
            {
                dto.PaymentMode = "Razor Pay";
            }

            try
            {
                var result = await _paymentService.ProcessPaymentAsync(dto);
                return Ok(new 
                { 
                    id = result.Id, 
                    amount = result.Amount, 
                    currency = "INR", 
                    key = "", // The frontend uses the key from ViewBag, but we can return it if needed. 
                              // Current PaymentService returns a Payment entity which contains status and providerTransactionId
                    orderId = result.ProviderTransactionId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmPayment([FromBody] PaymentConfirmDto dto)
        {
            var result = await _paymentService.ConfirmPaymentAsync(dto);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}

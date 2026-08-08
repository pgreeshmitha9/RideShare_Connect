using Microsoft.AspNetCore.Mvc;
using RideShare_Connect.DTOs;
using RideShare_Connect.Services;

namespace RideShare_Connect.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public WalletController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("add-money")]
        public async Task<IActionResult> AddMoney([FromBody] AddMoneyDto dto)
        {
            try
            {
                var result = await _paymentService.AddMoneyAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance(int userId)
        {
             var balance = await _paymentService.GetWalletBalanceAsync(userId);
             return Ok(new { balance });
        }
    }
}

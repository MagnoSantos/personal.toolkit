using Microsoft.AspNetCore.Mvc;
using Personal.RateLimit.Middleware._2___RateLimiter;

namespace Personal.RateLimit.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        [HttpGet(Name = "GetCustomer")]
        [RateLimitDecorator(StrategyType = StrategyTypeEnum.IpAddress)]
        public IActionResult Get()
        {
            return Ok(new
            {
                Nome = "Magno",
                SobreNome = "Santos"
            });
        }
    }
}
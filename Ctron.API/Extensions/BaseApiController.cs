using Microsoft.AspNetCore.Mvc;

namespace Ctron.API.Extensions
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller { }
}
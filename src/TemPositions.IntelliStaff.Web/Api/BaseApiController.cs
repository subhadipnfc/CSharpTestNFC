using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace TemPositions.IntelliStaff.Web.Api;

/// <summary>
/// If your API controllers will use a consistent route convention and the [ApiController] attribute (they should)
/// then it's a good idea to define and use a common base controller class like this one.
/// </summary>
/// 
[Authorize]
[Route("api/[controller]")]
[ApiController]
[EnableCors]
public abstract class BaseApiController : Controller
{
  
}

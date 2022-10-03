using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemPositions.IntelliStaff.Core.ApplicantAggregate;
using TemPositions.IntelliStaff.Core.Interfaces;
using TemPositions.IntelliStaff.Web.ApiModels;
namespace TemPositions.IntelliStaff.Web.Api;
public class DivisionsController : BaseApiController
{
  private readonly IApplicantRepository _ApplicantRepository;
  public DivisionsController(IApplicantRepository repository)
  {
    _ApplicantRepository = repository;
  }

  /// <summary>
  /// GetDivisions api method used to get the all divisions
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Route("GetDivisions")]
  [ProducesResponseType(typeof(IEnumerable<GetDivisions>), (int)HttpStatusCode.OK)]
  public async Task<IActionResult> GetDivisions()
  {
    try
    {
      var divisions = await _ApplicantRepository.GetDivisions();
      if (divisions == null)
      {
        return NotFound();
      }

      return Ok(divisions);
    }
    catch (Exception)
    {
      return BadRequest();
    }

  }
}

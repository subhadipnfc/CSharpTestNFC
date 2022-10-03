
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemPositions.IntelliStaff.Core.ApplicantAggregate;
using TemPositions.IntelliStaff.Core.Interfaces;
using TemPositions.IntelliStaff.Web.ApiModels;
namespace TemPositions.IntelliStaff.Web.Api;
public class JobBoardDetailsController : BaseApiController
{
  private readonly IApplicantRepository _ApplicantRepository;
  public JobBoardDetailsController(IApplicantRepository repository)
  {
    _ApplicantRepository = repository;
  }

  /// <summary>
  /// GetDivisions api method used to get the all divisions
  /// </summary>
  /// <returns></returns>
  [HttpGet]
  [Route("GetJobBoardDetails")]
  [ProducesResponseType(typeof(IEnumerable<JobBoardDetails>), (int)HttpStatusCode.OK)]
  public async Task<IActionResult> GetJobBoardDetails()
  {
    try
    {
      var jobBoardDetails = await _ApplicantRepository.GetJobBoardDetails();
      if (jobBoardDetails == null)
      {
        return NotFound();
      }

      return Ok(jobBoardDetails);
    }
    catch (Exception)
    {
      return BadRequest();
    }

  }

  ///// <summary>
  ///// GetJobdetails api method used to get JobId
  ///// </summary>
  ///// <returns></returns>
  //[HttpGet]
  //[Route("GetJobBoardDetails")]
  //public async Task<IActionResult> GetJobBoardDetails(int JobId)
  //{
  //  try
  //  {
  //    var jobBoardDetails = await _ApplicantRepository.GetJobBoardDetails(JobId);
  //    if (jobBoardDetails == null)
  //    {
  //      return NotFound();
  //    }

  //    return Ok(jobBoardDetails);
  //  }
  //  catch (Exception)
  //  {
  //    return BadRequest();
  //  }

  //}
}

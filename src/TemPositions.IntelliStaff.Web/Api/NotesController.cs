using Microsoft.AspNetCore.Mvc;
using TemPositions.IntelliStaff.Core.ApplicantAggregate;
using TemPositions.IntelliStaff.Core.Interfaces;

namespace TemPositions.IntelliStaff.Web.Api;
public class NotesController : BaseApiController
{
  private readonly IApplicantRepository _ApplicantRepository;

  public NotesController(IApplicantRepository repository)
  {
    _ApplicantRepository = repository;
  }


  /// <summary>
  /// GetDivisions api method used to get the all divisions
  /// </summary>
  /// <returns></returns>
  [HttpPost]
  [Route("GetNotes")]
  public async Task<IActionResult> GetNotes([FromBody] ApplicantNotesRequest objRequest)
  {
    try
    {
      var notesDetails = await _ApplicantRepository.GetNotesDetailsByApplicantId(objRequest);
      if (notesDetails == null)
      {
        return NotFound();
      }

      return Ok(notesDetails);
    }
    catch (Exception)
    {
      return BadRequest();
    }

  }

}

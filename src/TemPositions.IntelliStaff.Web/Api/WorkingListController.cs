using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TemPositions.IntelliStaff.Core.Interfaces;
using TemPositions.IntelliStaff.Core.WorkListAggregate;
namespace TemPositions.IntelliStaff.Web.Api;


public class WorkingListController : BaseApiController
{
  private readonly IWorkingListRepository _WorkingListRepository;
  public WorkingListController(IWorkingListRepository repository)
  {
    _WorkingListRepository = repository;
  }

  [HttpPost]

  [Route("AddWorkingList")]
  public async Task<IActionResult> AddWorkingList([FromBody] AddWorkingList objWorkList)
  {
    try
    {
      var WorkingListID = _WorkingListRepository.AddWorkingList(objWorkList);
      if (WorkingListID > 0)
      {
        return Ok(WorkingListID) ;
      }
      else
      {
        return NotFound();
      }
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }

  }
  [HttpGet]

  [Route("RemoveWorkingList")]
  public async Task<IActionResult> RemoveWorkingList(string WorkingListItemID)
  {
    try
    {
      bool result = _WorkingListRepository.RemoveWorkingList(WorkingListItemID);
      if (result == true)
      {
        return Ok(result);
      }
      else
      {
        return NotFound(false);
      }
    }
    catch (Exception ex)
    {
      return BadRequest(ex);
    }

  }

  [HttpPost]


  [Route("GetWorkingList")]
  public async Task<IActionResult> GetWorkingList(int UserId)
  {
    try
    {
      var applicants = await _WorkingListRepository.GetWorkingList(UserId);
      if (applicants == null)
      {
        return NotFound("No Applicants Found");
      }

      return Ok(applicants);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get JobPost Applicant Serach Details");
    }

  }


  [HttpPost]


  [Route("GetAllWorkingList")]
  public async Task<IActionResult> GetAllWorkingList(SearchWorkingList objSearchWorkingList)
  {
    try
    {
      var applicants = await _WorkingListRepository.GetAllWorkingList(objSearchWorkingList);     
      return Ok(applicants);
    }
    catch (Exception)
    {
      return BadRequest("Failed to Get JobPost Applicant Serach Details");
    }

  }
  
  
}

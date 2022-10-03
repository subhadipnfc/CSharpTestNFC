using System.Collections;
using Ardalis.Specification.EntityFrameworkCore;
using TemPositions.IntelliStaff.SharedKernel.Interfaces;
using TemPositions.IntelliStaff.Core.WorkListAggregate;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TemPositions.IntelliStaff.Core.Interfaces;
using TemPositions.IntelliStaff.Core.ApplicantAggregate;

namespace TemPositions.IntelliStaff.Infrastructure.Data;
public class WorkingListRepository : IAggregateRoot, IWorkingListRepository
{
  private readonly IntelliStaffContext intelliStaffContext;
  public WorkingListRepository(IntelliStaffContext context)
  {
    this.intelliStaffContext = context;
  }
  public async Task<IEnumerable<Core.WorkListAggregate.WorkingListNew>> GetWorkingList(int UserID)
  {
    try
    {
      
      if (intelliStaffContext != null)
      {      

        return await (from d in intelliStaffContext.WorkingListNew
                      join r in intelliStaffContext.WorkingListUser on d.WorkingListId equals r.WorkingListId into Workinglistgroup
                      from objworkinglistnew in Workinglistgroup.DefaultIfEmpty()                       
                        where (d.WorkingListAccessTypeId == Convert.ToInt32(EnumWorkingListAccessType.Public)) || (d.WorkingListAccessTypeId == Convert.ToInt32(EnumWorkingListAccessType.Shared) && objworkinglistnew.UserId == UserID)
                         || (d.WorkingListAccessTypeId == Convert.ToInt32(EnumWorkingListAccessType.Private) && objworkinglistnew.UserId == UserID)

                      select new Core.WorkListAggregate.WorkingListNew
                      {
                        WorkingListId = d.WorkingListId,
                        WorkingListName = d.WorkingListName,
                       
                      }).Distinct().ToListAsync();

      }
      return null;
    }
    catch (Exception ex)
    {
      return null;
    }
  }
  public int AddWorkingList(AddWorkingList objworkingList)
  {
    IntelliStaff.Infrastructure.WorkingListNew _obj= new IntelliStaff.Infrastructure.WorkingListNew();
    try
    {
      if (intelliStaffContext != null)
      {
        _obj.WorkingListName = objworkingList.WorkingListName;
        _obj.WorkingListCategoryTypeId = objworkingList.WorkingListCategoryTypeId;
        _obj.WorkingListAccessTypeId = objworkingList.WorkingListAccessTypeId;
        _obj.Status = objworkingList.Status;


        intelliStaffContext.WorkingListNew.Add(_obj);
        intelliStaffContext.SaveChanges();
        var id = _obj.WorkingListId;

        

        foreach (AddWorkingListUser objworkinglistuser in objworkingList.WorkingListUser)
        {
          IntelliStaff.Infrastructure.WorkingListUser _objUser = new IntelliStaff.Infrastructure.WorkingListUser();
          _objUser.WorkingListId = id;
          _objUser.UserId = objworkinglistuser.UserId;
          intelliStaffContext.WorkingListUser.Add(_objUser);
          intelliStaffContext.SaveChanges();
        }

      }



      return _obj.WorkingListId;
    }
    catch (Exception ex)
    {
      return 0;
    }
  }

  public bool RemoveWorkingList(string WorkingListItemId)
  {
    IntelliStaff.Infrastructure.WorkingListNew _obj = new IntelliStaff.Infrastructure.WorkingListNew();
    try
    {
      if (intelliStaffContext != null)
      {
        string[] subworklistitem = WorkingListItemId.Split(',');
        foreach (var element in subworklistitem)
        {
          
          string itemid=element.Replace("status: [", "").Replace("]","");
         
          WorkingListItem listuser = intelliStaffContext.workinglistitem.FirstOrDefault(s => s.WorkingListItemId.Equals(int.Parse(itemid)));
          if (listuser != null)
          {
            intelliStaffContext.workinglistitem.Remove(listuser);
            intelliStaffContext.SaveChanges();
          }
        }


      }

      return true;
    }
    catch (Exception ex)
    {
      return false;
    }
  }

  public async Task<IEnumerable<SearchWorkingListResult>> GetAllWorkingList(SearchWorkingList objSearchWorkingList)
  {
    try
    {
      if (intelliStaffContext != null)
      {
        List<int> ids = null;
        List<int> objStages = null;
        List<int?> objStages1 = null;
        List<int> objwork = null;
        
        if (!string.IsNullOrEmpty(objSearchWorkingList.ApplicantStatus))
        {
          ids = objSearchWorkingList.ApplicantStatus?.Split(',')?.Select(int.Parse).ToList();
          objStages = (from s11 in ids
                       select s11).ToList();
          objStages1 = (from a in intelliStaffContext.RecuritmentStageDetail
                        from b in intelliStaffContext.RecruitingSubStageDetail
                        .Where(o => o.RecruitingStageId == a.Id)
                        where ids.Contains(b.RecruitingSubStageId)
                        select a.ApplicantId
                      ).Distinct().ToList();

          if (objStages1 == null)
          {
            return null;
          }
          else
          {
            if (objStages1.Count() == 0)
            {
              objStages1 = null;
              return null;
            }
          }

        }
        if ((objSearchWorkingList.RecruitingSubStageTypeId == null) || (objSearchWorkingList.RecruitingSubStageTypeId == 0))
        {
          objSearchWorkingList.RecruitingSubStageTypeId = 12;
        }

        //get working list status applicant id
        if (objSearchWorkingList.WorkingListId != null && objSearchWorkingList.WorkingListId != 0)
        {

          objwork = (from a in intelliStaffContext.Applicant
                     from b in intelliStaffContext.workinglistitem
                       .Where(o => o.CandidateID == a.CandidateId)
                     from c in intelliStaffContext.WorkingListNew
                     .Where(j => j.WorkingListId == b.WorkingListId)
                     where b.CandidateID == a.CandidateId && b.WorkingListId == objSearchWorkingList.WorkingListId
                     select a.Id
                      ).Distinct().ToList();
          if (objwork == null)
          {
            return null;
          }
          else
          {
            if (objwork.Count() == 0)
            {
              objwork = null;
              return null;
            }
          }

        }


        return await (from d in intelliStaffContext.workinglistitem
                      from r in intelliStaffContext.Applicant
                      .Where(o => o.CandidateId == d.CandidateID && o.CandidateId!=0)
                       where ((r.FirstName.Contains((string.IsNullOrEmpty(objSearchWorkingList.FirstName) ? r.FirstName : objSearchWorkingList.FirstName)))
                   && r.LastName.StartsWith((string.IsNullOrEmpty(objSearchWorkingList.LastName) ? r.LastName : objSearchWorkingList.LastName))
                   && r.Email.StartsWith(string.IsNullOrEmpty(objSearchWorkingList.EmailAddress) ? r.Email : objSearchWorkingList.EmailAddress)
                    && (!string.IsNullOrEmpty(objSearchWorkingList.ApplicantStatus) ? objStages1.Contains(r.Id) : r.Id == r.Id))
                    && ((objwork != null) ? objwork.Contains(r.Id) : (r.Id == r.Id))
                      orderby d.CreatedOn, d.CandidateID descending
                       select new Core.WorkListAggregate.SearchWorkingListResult
                      {
                        CandidateID = d.ItemId,
                        status = (from a1 in intelliStaffContext.RecuritmentStageDetail
                                  from b in intelliStaffContext.RecruitingSubStageDetail
                                  .Where(o => o.RecruitingStageId == a1.Id)
                                  from j1 in intelliStaffContext.RecruitingStageType
                                   .Where(j => j.RecruitingStageTypeId == a1.RecruitingStageTypeId)
                                  where a1.CandidateId == d.ItemId
                                  orderby b.Id descending
                                  select j1.StageName).SingleOrDefault(),

                        CampaignComplete = "",

                        CampaignStatus = (from a1 in intelliStaffContext.RecuritmentStageDetail
                                          from b in intelliStaffContext.RecruitingSubStageDetail
                                          .Where(o => o.RecruitingStageId == a1.Id)
                                          from j1 in intelliStaffContext.CandidateStatus
                                           .Where(j => j.Id == b.RecruitingSubStageId)
                                          where a1.CandidateId == d.ItemId && b.RecruitingSubStageId == objSearchWorkingList.RecruitingSubStageTypeId
                                          orderby b.Id descending
                                          select j1.Name).SingleOrDefault(),
                        AddedDate = Convert.ToDateTime(d.CreatedOn).ToShortDateString(),
                        home_Phone = r.HomePhoneNumber,
                        e_mail = r.Email,
                        workingListIteamId = d.WorkingListItemId,
                        workingListId = d.WorkingListId,
                        last_name = r.LastName,
                        first_name = r.FirstName,
                        div_id = r.DivisionId,
                        address = r.Address,
                        city = r.City,
                        state = r.State,
                        zip = r.ZipCode,
                        itemId = d.ItemId,
                        applicantID = r.Id,
                        tenantId = r.TenantId,
                        
                        ApplicantColorCode = String.IsNullOrEmpty(d.CreatedOn.ToString()) ? "red" : ((DateTime.Now - Convert.ToDateTime(d.CreatedOn.Value)).Days == 0 ? "green" : (DateTime.Now - Convert.ToDateTime(d.CreatedOn.Value)).Days > 7 ? "red" : "orange"),

                      }).ToListAsync();

      }
      return null;
    }
    catch (Exception ex)
    {
      return null;
    }
  }


}

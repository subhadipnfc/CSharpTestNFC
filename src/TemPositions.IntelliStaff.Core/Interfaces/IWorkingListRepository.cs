using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemPositions.IntelliStaff.SharedKernel.Interfaces;
using TemPositions.IntelliStaff.Core.WorkListAggregate;

namespace TemPositions.IntelliStaff.Core.Interfaces;
public interface IWorkingListRepository
{
  public Task<IEnumerable<Core.WorkListAggregate.WorkingListNew>> GetWorkingList(int UserId);
  public int AddWorkingList(AddWorkingList objworkingList);
  public bool RemoveWorkingList(string WorkingListItemID);

  public Task<IEnumerable<Core.WorkListAggregate.SearchWorkingListResult>> GetAllWorkingList(SearchWorkingList objSearchWorkingList);
}

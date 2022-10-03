using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TemPositions.IntelliStaff.Core.ApplicantAggregate;
using TemPositions.IntelliStaff.Core.Response;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace TemPositions.IntelliStaff.Infrastructure;
public  class IntelliStaffContext : DbContext
{
  public IntelliStaffContext()
  {
  }

  public IntelliStaffContext(DbContextOptions<IntelliStaffContext> options)
      : base(options)
  {
  }
  public virtual DbSet<Divisions> Divisions { get; set; } = null!;
  public virtual DbSet<JobBoardDetails> JobBoardDetails { get; set; } = null!;
  public virtual DbSet<RecruitingStageType> RecruitingStageType { get; set; } = null!;
  public virtual DbSet<RecruitingSubStageType> RecruitingSubStageType { get; set; } = null!;
  public virtual DbSet<Applicant> Applicant { get; set; }
  public virtual DbSet<ApplicantSearchMeta> ApplicantSearchMeta { get; set; }
  public virtual DbSet<WorkingList> WorkingList { get; set; }
  public virtual DbSet<RecuritmentStageDetail> RecuritmentStageDetail { get; set; }
  public virtual DbSet<JobPostDetails> JobPostDetails { get; set; }

  public virtual DbSet<JobPostDetailsIntermediary> JobPostDetailsIntermediary { get; set; }
  public virtual DbSet<ICUResumeQueue> ICUResumeQueue { get; set; }
  public virtual DbSet<NoteCategoriesMaster> NoteCategoriesMaster { get; set; }
  public virtual DbSet<NoteCategoriesChild> NoteCategoriesChild { get; set; }
  public virtual DbSet<WorkingListNew> WorkingListNew { get; set; }
  public virtual DbSet<WorkingListUser> WorkingListUser { get; set; }
  public virtual DbSet<WorkingListCategoryType> WorkingListCategoryType { get; set; }
  public virtual DbSet<ApplicantNotes> ApplicantNotes { get; set; }
  public virtual DbSet<AppNotes> AppNotes { get; set; }
  public virtual DbSet<CandNotes> CandNotes { get; set; }
  public virtual DbSet<CanProfGen> CanProfGen { get; set; }
  public virtual DbSet<Interview> Interview { get; set; }
  public virtual DbSet<ProfileContacted> ProfileContacted { get; set; }
  public virtual DbSet<ProfileReview> ProfileReview { get; set; }
  public virtual DbSet<Campaign> Campaign { get; set; }
  public virtual DbSet<CampaignInvitations> Campaign_Invitations { get; set; }
  public virtual DbSet<ApplicantQueue> ApplicantQueue { get; set; }
  public virtual DbSet<Qualification> Qualification { get; set; }
  public virtual DbSet<OrderGen> Order_Gen { get; set; }

  public virtual DbSet<OrderGenHis> Order_Gen_His { get; set; }

  public virtual DbSet<OrdCallResults> Ord_Call_Results { get; set; }

  public virtual DbSet<WorkingListItem> workinglistitem { get; set; }

  public virtual DbSet<JobCategorys> JobCategory { get; set; }
  public virtual DbSet<JobSubCategory> JobSubCategory { get; set; }
  public virtual DbSet<JobLevels> JobLevel { get; set; }
  public virtual DbSet<EducationLevels> EducationLevel { get; set; }
  public virtual DbSet<Locations> Locations { get; set; }
  public virtual DbSet<Industries> Industries { get; set; }
  public virtual DbSet<UserNames> User_Names { get; set; }
  public virtual DbSet<EmailTemplates> EmailTemplates { get; set; }
  public virtual DbSet<CandidateEmailList> CandidateEmailList { get; set; }
  public virtual DbSet<CandidateOnBoarding> CandidateOnBoarding { get; set; }
  public virtual DbSet<ApplicantSubSource> ApplicantSubSource { get; set; }
  public virtual DbSet<RecuritmentSubStageDetail> RecruitingSubStageDetail { get; set; }
  public virtual DbSet<ApplicantLogo> ApplicantLogo { get; set; }

  //NON TABLE PROPERTIES
  public virtual DbSet<EmailMessageData> EmailMessageData { get; set; }

  public virtual DbSet<ApplicantResumes> ApplicantResumes { get; set; }
  public virtual DbSet<AppResumes> AppResumes { get; set; }
  public virtual DbSet<ApplicantQueueAudit> ApplicantQueueAudit { get; set; }

  public virtual DbSet<DivisionTheme> DivisionTheme { get; set; }

  public virtual DbSet<JobPostCategoryMapping> JobPostCategoryMapping { get; set; }
  public virtual DbSet<JobPostSkill> JobPostSkill { get; set; }
  public virtual DbSet<SkillSet> SkillSet { get; set; }
  public virtual DbSet<JobPostDescription> JobPostDescription { get; set; }
  public virtual DbSet<CandidateStatus> CandidateStatus { get; set; }
  public virtual DbSet<CandidateSubStatus> CandidateSubStatus { get; set; }
  public virtual DbSet<IcuResumeQueueFile> IcuResumeQueueFile { get; set; }

  public virtual DbSet<UserLoginInfo> UserLoginInfo { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
        optionsBuilder.UseSqlServer(ConfigurationManager.AppSettings["DefaultConnection"].ToString());
    }
  }

  #region Required
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<EmailMessageData>()
        .HasNoKey();

    modelBuilder.Entity<ApplicantSearchMeta>()
      .HasNoKey();
  }
  #endregion



  public override int SaveChanges()
  {
    UpdateDates();
    return base.SaveChanges();
  }
 
  private void UpdateDates()
  {
    foreach (var change in ChangeTracker.Entries<CanProfGen>())
    {
      var values = change.CurrentValues;
      foreach (var name in values.Properties)
      {
        var value = values[name];
        if (value is DateTime)
        {
          var date = (DateTime)value;
          if (date < SqlDateTime.MinValue.Value)
          {
            values[name] = SqlDateTime.MinValue.Value;
          }
          else if (date > SqlDateTime.MaxValue.Value)
          {
            values[name] = SqlDateTime.MaxValue.Value;
          }
        }
      
      }
    }
  }

}

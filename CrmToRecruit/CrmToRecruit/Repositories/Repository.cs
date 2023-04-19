using CrmToRecruit.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmToRecruit.Repositories
{
    public class Repository : IRepository
    {
        private readonly MyDbContext _dbContext;

        public Repository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveCrmToRecruitList(List<CrmToRecruitDto> crmToRecruitList)
        {
            var entities = crmToRecruitList.Select(crmToRecruit => new CrmToRecruitEntity
            {
                RecordId = crmToRecruit.RecordId,
                AccountName = crmToRecruit.AccountName,
                DealName = crmToRecruit.DealName,
                DealOwner = crmToRecruit.DealOwner,
                NumberOfResources = crmToRecruit.NumberOfResources,
                StageOfOpen = crmToRecruit.StageOfOpen,
                StageOfClosed = crmToRecruit.StageOfClosed,
                JobOpeningCreationDate = crmToRecruit.JobOpeningCreationDate,
                LastActivityTime = crmToRecruit.LastActivityTime,
                MustHaveSkills = crmToRecruit.MustHaveSkills,
                NiceToHaveSkills = crmToRecruit.NiceToHaveSkills,
                ProjectDescription = crmToRecruit.ProjectDescription,
                RmOwnership = crmToRecruit.RmOwnership,
                NotesPriority = crmToRecruit.NotesPriority,
                SubmittedST = crmToRecruit.SubmittedSt,
                SubmittedVendor = crmToRecruit.SubmittedVendor,
                InterviewedST = crmToRecruit.InterviewedSt,
                InterviewedVendor = crmToRecruit.InterviewedVendor,
                ConfirmedST = crmToRecruit.ConfirmedSt,
                ConfirmedVendor = crmToRecruit.ConfirmedVendor,
                RejectedST = crmToRecruit.RejectedSt,
                RejectedVendor = crmToRecruit.RejectedVendor
            });

            await _dbContext.CrmToRecruitEntities.AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }
    }
}


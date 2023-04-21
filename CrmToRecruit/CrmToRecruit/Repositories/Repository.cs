using CrmToRecruit.Domain;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
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

        public async Task SaveClosedDealsList(List<ClosedDealsDto> closedDealsList)
        {
            var closedDealsEntities = new List<ClosedDealsEntity>();

            foreach (var closedDeal in closedDealsList)
            {
                var closedDealEntity = new ClosedDealsEntity
                {
                    RecordId = closedDeal.RecordId,
                    DealOwner = closedDeal.DealOwner,
                    DealName = closedDeal.DealName,
                    AccountName = closedDeal.AccountName,
                    Stage = closedDeal.Stage,
                    ModifiedTime = closedDeal.ModifiedTime,
                    JobOpeningCreationDate = closedDeal.JobOpeningCreationDate,
                    ClosingDate = closedDeal.ClosingDate,
                    NumberOfRoles = closedDeal.NumberOfRoles,
                    NumberOfResources = closedDeal.NumberOfResources,
                    LossReason = closedDeal.LossReason,
                    LossDescription = closedDeal.LossDescription
                };

                closedDealsEntities.Add(closedDealEntity);
            }

            _dbContext.ClosedDeals.AddRange(closedDealsEntities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ClosedDealsEntity>> GetAll()
        {
            return await _dbContext.ClosedDeals.ToListAsync();
        }

        public async Task<List<ClosedDealsReportDto>> GenerateMonthlyReport()
        {
            var startDate = new DateTime(DateTime.Now.Year, 1, 1);
            var endDate = startDate.AddYears(1);

            var reportData = new List<ClosedDealsReportDto>();

            for (var date = startDate; date < endDate; date = date.AddMonths(1))
            {
                var monthYear = date.ToString("MMM. yy");

                var closedWonCount = await _dbContext.ClosedDeals
                    .Where(cd => cd.Stage == "Closed (Won)")
                    .Where(cd => cd.ClosingDate.HasValue && cd.ClosingDate.Value.Year == date.Year && cd.ClosingDate.Value.Month == date.Month)
                    .CountAsync();

                var closedLostCount = await _dbContext.ClosedDeals
                    .Where(cd => cd.Stage == "Closed (Lost)")
                    .Where(cd => cd.ClosingDate.HasValue && cd.ClosingDate.Value.Year == date.Year && cd.ClosingDate.Value.Month == date.Month)
                    .CountAsync();

                var reportItem = new ClosedDealsReportDto
                {
                    MonthYear = monthYear,
                    ClosedWonCount = closedWonCount,
                    ClosedLostCount = closedLostCount
                };

                reportData.Add(reportItem);
            }

            return reportData;
        }
    }
}


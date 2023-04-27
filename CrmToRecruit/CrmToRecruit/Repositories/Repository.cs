using AutoMapper;
using CrmToRecruit.Domain;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CrmToRecruit.Repositories
{
    public class Repository : IRepository
    {
        private readonly MyDbContext _dbContext;
        private readonly IMapper _mapper;
        public Repository(MyDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
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

        private (DateTime startDate, DateTime endDate) GetStartAndEndDateForWeek(int weekNumber)
        {
            // Calculate the first day of the year
            var jan1 = new DateTime(2023, 1, 1);

            // Calculate the day of the week that January 1 falls on
            var jan1DayOfWeek = (int)jan1.DayOfWeek;

            // Calculate the number of days between January 1 and the first day of the first week
            var daysToFirstWeekDay = ((int)DayOfWeek.Monday - jan1DayOfWeek + 7) % 7;

            // Calculate the date of the first day of the first week
            var firstWeekDay = jan1.AddDays(daysToFirstWeekDay);

            // Calculate the start date and end date of the requested week number
            var startDate = firstWeekDay.AddDays((weekNumber - 1) * 7);
            var endDate = startDate.AddDays(6);

            return (startDate, endDate);
        }

        public async Task<List<CrmToRecruitExtendedEntity>> GetOpenDealsByWeek(int weekNumber)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            Calendar cal = ci.Calendar;

            var (startDate, endDate) = GetStartAndEndDateForWeek(weekNumber);

            var openDeals = await _dbContext.CrmToRecruitEntities
                .Where(c => c.JobOpeningCreationDate.HasValue && c.JobOpeningCreationDate.Value <= endDate)
                .ToListAsync();

            var closedDeals = await _dbContext.ClosedDeals
                .Where(c => c.ClosingDate.HasValue && c.ClosingDate.Value <= startDate)
                .ToListAsync();

            var closedDealsThisWeek = await _dbContext.ClosedDeals
                .Where(c => c.ClosingDate.HasValue && c.ClosingDate.Value >= startDate && c.ClosingDate.Value <= endDate)
                .ToListAsync();

            var openCrmToRecruitList = _mapper.Map<List<CrmToRecruitExtendedEntity>>(openDeals);

            var openCrmToRecruitListResult = openCrmToRecruitList
                .Where(c => !closedDeals.Any(cd => cd.RecordId == c.RecordId))
                .Select(c =>
                {
                    var matchingClosedDeal = closedDealsThisWeek.FirstOrDefault(cdw => cdw.RecordId == c.RecordId);
                    if (matchingClosedDeal != null)
                    {
                        c.StageOfClosed = matchingClosedDeal.Stage;
                        c.ClosingDate = matchingClosedDeal.ClosingDate;
                    }
                    return c;
                }).ToList();

            return openCrmToRecruitListResult;
        }


        public async Task<List<CrmToRecruitEntity>> GetOpenDealsByWeek2(int weekNumber)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            Calendar cal = ci.Calendar;

            var openDeals = await _dbContext.CrmToRecruitEntities
                .Where(c => c.JobOpeningCreationDate.HasValue)
                .Where(c => cal.GetWeekOfYear(c.JobOpeningCreationDate.Value, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek) >= weekNumber)
                .ToListAsync();

            var closedDeals = await _dbContext.ClosedDeals
                .Where(c => c.ClosingDate.HasValue)
                .Where(c => cal.GetWeekOfYear(c.ClosingDate.Value, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek) >= weekNumber)
                .ToListAsync();

            var openCrmToRecruitList = openDeals.Where(c => !closedDeals.Any(cd => cd.RecordId == c.RecordId)).ToList();

            return openDeals;
        }

        public async Task<List<int>> GetClosedDealsLossReasons()
        {
            List<int> lossReasonCounts = new List<int>();

            var year = DateTime.Now.Year;
            var lossReasons = _dbContext.ClosedDeals
                .Where(cd => cd.Stage == "Closed (Lost)")
                .Where(cd => cd.ClosingDate.HasValue && cd.ClosingDate.Value.Year == year)
                .Select(cd => cd.LossReason);

            int canceledCount = 0, competitorCount = 0, costCount = 0, couldntFindCount = 0, diffDirectionCount = 0, filledByClientCount = 0;

            foreach (var reason in lossReasons)
            {
                if (reason.Contains("Canceled"))
                    canceledCount++;
                if (reason.Contains("Competitor"))
                    competitorCount++;
                if (reason.Contains("Cost"))
                    costCount++;
                if (reason.Contains("Couldn’t find"))
                    couldntFindCount++;
                if (reason.Contains("Diff direction"))
                    diffDirectionCount++;
                if (reason.Contains("Filled by client"))
                    filledByClientCount++;
            }

            lossReasonCounts.Add(canceledCount);
            lossReasonCounts.Add(competitorCount);
            lossReasonCounts.Add(costCount);
            lossReasonCounts.Add(couldntFindCount);
            lossReasonCounts.Add(diffDirectionCount);
            lossReasonCounts.Add(filledByClientCount);

            return lossReasonCounts;
        }

        public async Task<Dictionary<string, int>> GetCompaniesRecruitInfo()
        {
            var vendorTotals = new Dictionary<string, int>();

            var crmToRecruitList = await _dbContext.CrmToRecruitEntities.ToListAsync();

            foreach (var crm in crmToRecruitList)
            {
                // Process Submitted vendors
                if (crm.SubmittedVendor != null)
                {
                    var splitValues = crm.SubmittedVendor.Split('-');
                    var vendorName = splitValues[0];
                    if (vendorTotals.ContainsKey(vendorName))
                    {
                        vendorTotals[vendorName] += int.Parse(splitValues[1]);
                    }
                    else
                    {
                        vendorTotals.Add(vendorName, int.Parse(splitValues[1]));
                    }
                }

                // Process Interviewed vendors
                if (crm.InterviewedVendor != null)
                {
                    var splitValues = crm.InterviewedVendor.Split('-');
                    var vendorName = splitValues[0];
                    if (vendorTotals.ContainsKey(vendorName))
                    {
                        vendorTotals[vendorName] += int.Parse(splitValues[1]);
                    }
                    else
                    {
                        vendorTotals.Add(vendorName, int.Parse(splitValues[1]));
                    }
                }

                // Process Confirmed vendors
                if (crm.ConfirmedVendor != null)
                {
                    var splitValues = crm.ConfirmedVendor.Split('-');
                    var vendorName = splitValues[0];
                    if (vendorTotals.ContainsKey(vendorName))
                    {
                        vendorTotals[vendorName] += int.Parse(splitValues[1]);
                    }
                    else
                    {
                        vendorTotals.Add(vendorName, int.Parse(splitValues[1]));
                    }
                }

                // Process Rejected vendors
                if (crm.RejectedVendor != null)
                {
                    var splitValues = crm.RejectedVendor.Split('-');
                    var vendorName = splitValues[0];
                    if (vendorTotals.ContainsKey(vendorName))
                    {
                        vendorTotals[vendorName] += int.Parse(splitValues[1]);
                    }
                    else
                    {
                        vendorTotals.Add(vendorName, int.Parse(splitValues[1]));
                    }
                }
            }

            return vendorTotals;
        }
    }
}


﻿using CrmToRecruit.Domain;

namespace CrmToRecruit.Repositories
{
    public interface IRepository
    {
        Task<List<ClosedDealsReportDto>> GenerateMonthlyReport();
        Task SaveClosedDealsList(List<ClosedDealsDto> dealsList);
        Task SaveCrmToRecruitList(List<CrmToRecruitDto> crmToRecruitList);
        Task<List<int>> GetClosedDealsLossReasons();
        Task<List<CrmToRecruitExtendedEntity>> GetOpenDealsByWeek(int weekNumber);
        Task<Dictionary<string, int>> GetCompaniesRecruitInfo();
    }
}
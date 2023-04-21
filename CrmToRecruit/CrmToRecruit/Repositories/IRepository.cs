using CrmToRecruit.Domain;

namespace CrmToRecruit.Repositories
{
    public interface IRepository
    {
        Task<List<ClosedDealsReportDto>> GenerateMonthlyReport();
        Task SaveClosedDealsList(List<ClosedDealsDto> dealsList);
        Task SaveCrmToRecruitList(List<CrmToRecruitDto> crmToRecruitList);
    }
}
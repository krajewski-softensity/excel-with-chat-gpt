using CrmToRecruit.Domain;

namespace CrmToRecruit.Services
{
    public interface IService
    {
        public Task<List<CrmToRecruitDto>> ReadExcelFileCrmToRecruit(Stream stream);
        public Task<List<ClosedDealsDto>> ReadExcelFileClosedDeals(Stream stream);
        public Task<List<ClosedDealsReportDto>> GenerateMonthlyReport();
        public Task<List<int>> GetClosedDealsLossReasons();
        public Task<List<CrmToRecruitExtendedEntity>> GetOpenDealsByWeek(int weekNumber);
        public Task<Dictionary<string, int>> GetCompaniesRecruitInfo();
    }
}
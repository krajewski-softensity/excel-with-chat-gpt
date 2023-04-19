using CrmToRecruit.Domain;

namespace CrmToRecruit.Services
{
    public interface IService
    {
        public Task<List<CrmToRecruitDto>> ReadExcelFile(Stream stream);
    }
}
using CrmToRecruit.Domain;

namespace CrmToRecruit.Repositories
{
    public interface IRepository
    {
        Task SaveCrmToRecruitList(List<CrmToRecruitDto> crmToRecruitList);
    }
}
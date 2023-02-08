using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ICompetencyService
    {
        public Task<CompetencyListData> GetCompetencyDetails(AdminCompetency AdminCompetency);
        public Task<IEnumerable<CompetencyListData>> GetCompetencyList(AdminCompetency AdminCompetency);
        public Task<int> AddEditCompetency(Competency competency, List<string> errorsMessages);
        public Task<int> DeleteCompetency(DeleteCompetency competency, List<string> errorsMessages);
        public Task<IEnumerable<CompetencyLevel>> GetCompetencyLevel(AdminCompetency AdminCompetency);
        public Task<IEnumerable<CompetencyType>> GetCompetencyType(AdminCompetency AdminCompetency);
    }
}
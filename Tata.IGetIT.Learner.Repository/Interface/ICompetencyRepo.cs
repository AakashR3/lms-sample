using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ICompetencyRepo
    {
        public Task<CompetencyListData> GetCompetencyDetails(AdminCompetency AdminCompetency);
        public Task<IEnumerable<CompetencyListData>> GetCompetencyList(AdminCompetency AdminCompetency);
        public Task<int> AddEditCompetency(Competency competency);
        public Task<int> DeleteCompetency(DeleteCompetency competency);
        public Task<IEnumerable<CompetencyLevel>> GetCompetencyLevel(AdminCompetency AdminCompetency);
        public Task<IEnumerable<CompetencyType>> GetCompetencyType(AdminCompetency AdminCompetency);
    }
}

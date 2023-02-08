using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class HeroSectionRepo : IHeroSectionRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public HeroSectionRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<CareerPath> CareerPath(int UserID)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetCareerPath,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<CareerPath>(queryInfo);
        }

        public Task<CurrentLevel> CurrentLevel(int UserID)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetCurrentLevel,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<CurrentLevel>(queryInfo);
        }

        public Task<CurrentRole> CurrentRole(int UserID)
        {
            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetCurrentRole,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<CurrentRole>(queryInfo);
        }

        public Task<Skillset> Skillset(int UserID)
        {

            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetSkillset,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<Skillset>(queryInfo);
        }

        public Task<TargetRoles> TargetRole(int UserID)
        {

            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetTargetRole,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<TargetRoles>(queryInfo);
        }

        public Task<TargetRoleCareerPath> TargetRoleCareerPath(int UserID)
        {

            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetTargetRoleCareerPath,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<TargetRoleCareerPath>(queryInfo);
        }

        public Task<TargetRoleCareerPathPercentage> TargetRoleCareerPathPercentage(int UserID)
        {

            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetTargetRoleCareerPathPercentage,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<TargetRoleCareerPathPercentage>(queryInfo);
        }

        public Task<TargetRoleCurrentLevel> TargetRoleCurrentLevel(int UserID)
        {

            var queryParameters = new Dictionary<string, object>
            {
                { "@UserID", UserID }
            };

            QueryInfo queryInfo = new()
            {
                queryType = QueryType.StoredProcedure,
                QueryText = StoredProcedures.GetTargetRoleCurrentLevel,
                Parameters = queryParameters
            };

            return _databaseOperations.GetFirstRecordAsync<TargetRoleCurrentLevel>(queryInfo);
        }
    }
}

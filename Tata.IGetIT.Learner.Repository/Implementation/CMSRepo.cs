using Microsoft.SqlServer.Server;
using System.Numerics;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class CMSRepo : ICMSRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public CMSRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<IEnumerable<V2_CMSFormType>> GetCMSFormTypes()
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                   
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetCMS_FormTypes,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<V2_CMSFormType>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> InsertCMSFormData(V2_CMSFormDataInsert v2_CMSFormDataInsert)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    {"@UserID",v2_CMSFormDataInsert.UserID},
                    {"@FormId",v2_CMSFormDataInsert.FormId},
                    {"@FirstName",v2_CMSFormDataInsert.FirstName},
                    {"@LastName",v2_CMSFormDataInsert.LastName},
                    {"@Company",v2_CMSFormDataInsert.Company},
                    {"@email",v2_CMSFormDataInsert.email},
                    {"@Phone",v2_CMSFormDataInsert.Phone},
                    {"@Job",v2_CMSFormDataInsert.Job},
                    {"@Message",v2_CMSFormDataInsert.Message},
                    {"@Option",v2_CMSFormDataInsert.Option},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.InsertCMS_FormData,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<bool>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

    }
}

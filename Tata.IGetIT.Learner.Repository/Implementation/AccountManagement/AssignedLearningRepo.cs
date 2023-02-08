using System.Text.RegularExpressions;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface.AccountManagement;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.AccountManagement;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation.AccountManagement
{
    public class AssignedLearningRepo : IAssignedLearningRepo
    {
        private readonly IDatabaseManager _databaseOperations;
        public AssignedLearningRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<bool> AssignUserToLearningPath(AssignedLearningInputs assignedLearningInputs)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", assignedLearningInputs.UserID },
                    { "@PathID", assignedLearningInputs.PathID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.AssignUserToLP,
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

        public Task<IEnumerable<AssessmentsForLearningPath>> GetAssessmentsForLearningPath(int AccountID, int SubcategoryID, int CategoryID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                    { "@SubcategoryID", SubcategoryID },
                    { "@CategoryID", CategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.AssessmentsForLP,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<AssessmentsForLearningPath>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<AssignedUsersForLearningPath>> GetAssignedUsersForLearningPath(string UserIDs, int PathID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserIDs", UserIDs },
                    { "@PathID", PathID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.AssignedUsersForLP,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<AssignedUsersForLearningPath>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<ConditionalUsers>> GetConditionalUsersForLearningPath(int UserID, int PathID)
        {


            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@PathID", PathID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetConditionalUsers,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<ConditionalUsers>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<UnAssignedGroupByPathID>> GetUnAssignedGroupByPathID(int UserID, int PathID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@PathID", PathID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetUnAssignedGroupByPathID,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<UnAssignedGroupByPathID>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<CoursesForLearningPath>> GetCoursesForLearningPath(int AccountID, int SubcategoryID, int CategoryID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                    { "@SubcategoryID", SubcategoryID },
                    { "@CategoryID", CategoryID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.CoursesForLearningPath,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<CoursesForLearningPath>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<GroupListByManager>> GetGroupListByManagerForLearningPath(int UserID, bool ShowAll)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@ShowAll", ShowAll }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GroupListByManager,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<GroupListByManager>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<LearningAssignments>> GetLearningAssignments(int UserID, int ShowPredefined)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@ShowPredefined", ShowPredefined }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.AssingnedLearning,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<LearningAssignments>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
        public Task<IEnumerable<PreDefinedLearningPaths>> GetPreDefinedLearningPaths(int UserID, int ShowPredefined)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@ShowPredefined", ShowPredefined }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.PredefinedLearningPath,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<PreDefinedLearningPaths>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<SubcategoriesForLearningPath>> GetSubcategories(int UserID, int CategoryID, int Type)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@CategoryID", CategoryID },
                    { "@Type", Type }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetSubcategories,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<SubcategoriesForLearningPath>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<AssignedGroupByPathID>> GetAssignedGroupByPathID(int UserID, int PathID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@PathID", PathID },
                    { "@UserID", UserID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetAssignedGroupByPathID,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<AssignedGroupByPathID>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<NotificationEmail> GetNotificationEmail(int PathID)
        {

            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@PathID", PathID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetNotificationEmail,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<NotificationEmail>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<DynamicFieldOptions>> GetDynamicFieldOptions(int UserID, int FieldID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID },
                    { "@FieldID", FieldID }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetDynamicFieldOptions,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<DynamicFieldOptions>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<UserListByAccountIDOrGroupID>> GetUserListByAccountIDOrGroupID(int AccountID, int GroupID, DateTime? StartDate, DateTime? EndDate)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@AccountID", AccountID },
                    { "@GroupID", GroupID },
                    { "@StartDate", StartDate },
                    { "@EndDate", EndDate }
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetUserListByAccountIDOrGroupID,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<UserListByAccountIDOrGroupID>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> SetDynamicGroupAttribute(SetDynamicGroupAttribute setDynamicGroupAttribute)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@PathID", setDynamicGroupAttribute.PathID },
                    { "@FieldID", setDynamicGroupAttribute.FieldID },
                    { "@Filter", setDynamicGroupAttribute.Filter },
                    { "@ConditionID", setDynamicGroupAttribute.ConditionID },
                    { "@MainCondID", setDynamicGroupAttribute.MainCondID },
                    { "@AttrID", setDynamicGroupAttribute.AttrID },
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.SetDynamicGroupAttribute,
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

        public Task<IEnumerable<LearningPathItems>> GetLearningPathItems(int UserID, int PathID, int GroupID, DateTime? StartDate, DateTime? EndDate)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID},
                    { "@PathID", PathID },
                    { "@GroupID", GroupID },
                    { "@StartDate", StartDate},
                    { "@EndDate", EndDate},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetLearningPathItems,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<LearningPathItems>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> LPItemAction(LearningPathInput learningPathInput)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", learningPathInput.UserID},
                    { "@Action", learningPathInput.Action},
                    { "@PathID", learningPathInput.PathID},
                    { "@ItemID", learningPathInput.ItemID},
                    { "@ItemType", learningPathInput.ItemType},
                    { "@DueDate", learningPathInput.DueDate},
                    { "@StartDate", learningPathInput.StartDate},
                    { "@ItemSequence", learningPathInput.ItemSequence},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetLearningPathItemAction,
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

        public Task<int> LearningPathActions(LearningPathActionsInput learningPathActionsInput)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@Name", learningPathActionsInput.Name},
                    { "@UserID", learningPathActionsInput.UserID},
                    { "@AccountID", learningPathActionsInput.AccountID},
                    { "@Action", learningPathActionsInput.Action},
                    { "@PathID", learningPathActionsInput.PathID},
                    { "@ForceOrder", learningPathActionsInput.ForceOrder},
                    { "@Description", learningPathActionsInput.Description},
                    { "@Subcategory", learningPathActionsInput.Subcategory},
                    { "@Filters", learningPathActionsInput.Filters},
                    { "@TypeID", learningPathActionsInput.TypeID},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.SetLearningPathItemActions,
                    Parameters = param
                };

                var result = _databaseOperations.GetFirstRecordAsync<int>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<IntegrationsForLearningPath>> GetIntegrationsForLearningPath()
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetLearningPlatformCoursesForLP,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<IntegrationsForLearningPath>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> LPIntegrationsAction(LearningPlatformCourseAction LearningPlatformCourseAction)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@Action", LearningPlatformCourseAction.Action},
                    { "@UserID", LearningPlatformCourseAction.UserID},
                    { "@ID", LearningPlatformCourseAction.ID},
                    { "@Title", LearningPlatformCourseAction.Title},
                    { "@Description", LearningPlatformCourseAction.Description},
                    { "@AuthorName", LearningPlatformCourseAction.AuthorName},
                    { "@Link", LearningPlatformCourseAction.Link},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.IntegrationsForLearningPath,
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

        public Task<bool> InsertAssignGroupToLP(AssignGroupToLP AssignGroupToLP)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@GroupID", AssignGroupToLP.GroupID},
                    { "@PathID", AssignGroupToLP.PathID},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.InsertAssignGroupToLP,
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

        public Task<bool> RemoveGroupFromLP(RemoveGroupFromLP RemoveGroupFromLP)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@GroupID", RemoveGroupFromLP.GroupID},
                    { "@PathID", RemoveGroupFromLP.PathID},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DeleteAssignGroupToLP,
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

        public Task<IEnumerable<UserListByManager>> GetUserListByManager(int UserID, int GroupID)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID},
                    { "@GroupID", GroupID},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetUserListByManager,
                    Parameters = param
                };

                var result = _databaseOperations.GetMultipleRecords<UserListByManager>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<bool> RemoveUserFromLP(RemoveUserFromLP RemoveUserFromLP)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", RemoveUserFromLP.UserID},
                    { "@PathID", RemoveUserFromLP.PathID},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.DeleteUserFromLP,
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

        public Task<IEnumerable<AssignedLearningCatagories>> GetCatagories(int UserID, int Type)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID},
                    { "@Type", Type},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.AssingnedLearning_GetCategories,
                    Parameters = param
                };
                var result = _databaseOperations.GetMultipleRecords<AssignedLearningCatagories>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<IEnumerable<AssignedLearningSubCatagories>> GetSubCatagories(int UserID, int CategoryID, int Type)
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    { "@UserID", UserID},
                    { "@Type", Type},
                    { "@CategoryID", CategoryID},
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.AssingnedLearning_GetSubCategories,
                    Parameters = param
                };
                var result = _databaseOperations.GetMultipleRecords<AssignedLearningSubCatagories>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }

        public Task<int> GetPathIDtoCreateLP()
        {
            try
            {
                var param = new Dictionary<string, object>
                {
                    
                };
                QueryInfo queryInfo = new QueryInfo()
                {
                    queryType = QueryType.StoredProcedure,
                    QueryText = StoredProcedures.GetPathIDtoCreateLP,
                    Parameters = param
                };
                var result = _databaseOperations.GetFirstRecordAsync<int>(queryInfo);

                return result;
            }
            catch
            {
                throw;
            }
        }
    }
}

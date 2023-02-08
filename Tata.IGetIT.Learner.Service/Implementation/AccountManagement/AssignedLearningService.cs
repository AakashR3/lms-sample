using Tata.IGetIT.Learner.Repository.Interface.AccountManagement;
using Tata.IGetIT.Learner.Repository.Models.AccountManagement;
using Tata.IGetIT.Learner.Service.Interfaces.AccountManagement;

namespace Tata.IGetIT.Learner.Service.Implementation.AccountManagement
{
    public class AssignedLearningService : IAssignedLearningService
    {
        private readonly IAssignedLearningRepo _assignedLearningRepo;
        ILogger logger = LogManager.GetCurrentClassLogger();
        public AssignedLearningService(IAssignedLearningRepo assignedLearningRepo)
        {
            if (assignedLearningRepo == null)
            {
                new ArgumentNullException("assignedLearningRepo cannot be null");
            }
            _assignedLearningRepo = assignedLearningRepo;
        }

        public async Task<bool> AssignUserToLearningPath(AssignedLearningInputs assignedLearningInputs)
        {
            try
            {
                return await _assignedLearningRepo.AssignUserToLearningPath(assignedLearningInputs);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AssessmentsForLearningPath>> GetAssessmentsForLearningPath(int AccountID, int SubcategoryID, int CategoryID)
        {

            try
            {
                return await _assignedLearningRepo.GetAssessmentsForLearningPath(AccountID, SubcategoryID, CategoryID);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AssignedUsersForLearningPath>> GetAssignedUsersForLearningPath(string UserIDs, int PathID)
        {
            try
            {
                return await _assignedLearningRepo.GetAssignedUsersForLearningPath(UserIDs, PathID);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<ConditionalUsers>> GetConditionalUsersForLearningPath(int UserID, int PathID)
        {
            try
            {
                return await _assignedLearningRepo.GetConditionalUsersForLearningPath(UserID, PathID);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<UnAssignedGroupByPathID>> GetUnAssignedGroupByPathID(int UserID, int PathID)
        {
            try
            {
                return await _assignedLearningRepo.GetUnAssignedGroupByPathID(UserID, PathID);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<CoursesForLearningPath>> GetCoursesForLearningPath(int AccountID, int SubcategoryID, int CategoryID)
        {

            try
            {
                return await _assignedLearningRepo.GetCoursesForLearningPath(AccountID, SubcategoryID, CategoryID);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<GroupListByManager>> GetGroupListByManagerForLearningPath(int UserID, bool ShowAll)
        {

            try
            {
                return await _assignedLearningRepo.GetGroupListByManagerForLearningPath(UserID, ShowAll);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<LearningAssignments>> GetLearningAssignments(int UserID, int ShowPredefined)
        {

            try
            {
                return await _assignedLearningRepo.GetLearningAssignments(UserID, ShowPredefined);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<PreDefinedLearningPaths>> GetPreDefinedLearningPaths(int UserID, int ShowPredefined)
        {

            try
            {
                return await _assignedLearningRepo.GetPreDefinedLearningPaths(UserID, ShowPredefined);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<SubcategoriesForLearningPath>> GetSubcategories(int UserID, int CategoryID, int Type)
        {
            try
            {
                return await _assignedLearningRepo.GetSubcategories(UserID, CategoryID, Type);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AssignedGroupByPathID>> GetAssignedGroupByPathID(int UserID, int PathID)
        {

            try
            {
                return await _assignedLearningRepo.GetAssignedGroupByPathID(UserID, PathID);

            }
            catch
            {
                throw;
            }
        }

        public async Task<NotificationEmail> GetNotificationEmail(int PathID)
        {
            try
            {
                return await _assignedLearningRepo.GetNotificationEmail(PathID);

            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<DynamicFieldOptions>> GetDynamicFieldOptions(int UserID, int FieldID)
        {

            try
            {
                return await _assignedLearningRepo.GetDynamicFieldOptions(UserID, FieldID);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserListByAccountIDOrGroupID>> GetUserListByAccountIDOrGroupID(int AccountID, int GroupID, DateTime? StartDate, DateTime? EndDate)
        {
            try
            {
                return await _assignedLearningRepo.GetUserListByAccountIDOrGroupID(AccountID, GroupID, StartDate, EndDate);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> SetDynamicGroupAttribute(SetDynamicGroupAttribute setDynamicGroupAttribute)
        {
            try
            {
                return await _assignedLearningRepo.SetDynamicGroupAttribute(setDynamicGroupAttribute);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<LearningPathItems>> GetLearningPathItems(int UserID, int PathID, int GroupID, DateTime? StartDate, DateTime? EndDate)
        {
            try
            {
                return await _assignedLearningRepo.GetLearningPathItems(UserID, PathID, GroupID, StartDate, EndDate);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> LPItemAction(LearningPathInput learningPathInput)
        {
            try
            {
                return await _assignedLearningRepo.LPItemAction(learningPathInput);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> LearningPathActions(LearningPathActionsInput learningPathInput)
        {
            try
            {
                return await _assignedLearningRepo.LearningPathActions(learningPathInput);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<IntegrationsForLearningPath>> GetIntegrationsForLearningPath()
        {
            try
            {
                return await _assignedLearningRepo.GetIntegrationsForLearningPath();

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> LPIntegrationsAction(LearningPlatformCourseAction LearningPlatformCourseAction)
        {
            try
            {
                return await _assignedLearningRepo.LPIntegrationsAction(LearningPlatformCourseAction);

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> InsertAssignGroupToLP(AssignGroupToLP AssignGroupToLP)
        {
            try
            {
                return await _assignedLearningRepo.InsertAssignGroupToLP(AssignGroupToLP);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RemoveGroupFromLP(RemoveGroupFromLP RemoveGroupFromLP)
        {
            try
            {
                return await _assignedLearningRepo.RemoveGroupFromLP(RemoveGroupFromLP);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserListByManager>> GetUserListByManager(int UserID, int GroupID)
        {
            try
            {
                return await _assignedLearningRepo.GetUserListByManager(UserID, GroupID);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RemoveUserFromLP(RemoveUserFromLP removeUserFromLP)
        {
            try
            {
                return await _assignedLearningRepo.RemoveUserFromLP(removeUserFromLP);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AssignedLearningCatagories>> GetCatagories(int UserID, int Type)
        {
            try
            {
                return await _assignedLearningRepo.GetCatagories(UserID, Type);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IEnumerable<AssignedLearningSubCatagories>> GetSubCatagories(int UserID, int CategoryID, int Type)
        {
            try
            {
                return await _assignedLearningRepo.GetSubCatagories(UserID, CategoryID, Type);
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> GetPathIDtoCreateLP()
        {
            try
            {
                return await _assignedLearningRepo.GetPathIDtoCreateLP();
            }
            catch
            {
                throw;
            }
        }
    }
}

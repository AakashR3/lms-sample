using Tata.IGetIT.Learner.Repository.Models.AccountManagement;

namespace Tata.IGetIT.Learner.Repository.Interface.AccountManagement
{
    public interface IAssignedLearningRepo
    {
        public Task<IEnumerable<AssignedLearningCatagories>> GetCatagories(int UserID, int Type);
        public Task<int> GetPathIDtoCreateLP();
        public Task<IEnumerable<AssignedLearningSubCatagories>> GetSubCatagories(int UserID, int CategoryID, int Type);
        public Task<IEnumerable<LearningAssignments>> GetLearningAssignments(int UserID, int UserIDShowPredefined);
        public Task<IEnumerable<PreDefinedLearningPaths>> GetPreDefinedLearningPaths(int UserID, int ShowPredefined);
        public Task<IEnumerable<CoursesForLearningPath>> GetCoursesForLearningPath(int AccountID, int SubcategoryID, int CategoryID);
        public Task<IEnumerable<IntegrationsForLearningPath>> GetIntegrationsForLearningPath();
        public Task<IEnumerable<AssessmentsForLearningPath>> GetAssessmentsForLearningPath(int AccountID, int SubcategoryID, int CategoryID);
        public Task<IEnumerable<AssignedUsersForLearningPath>> GetAssignedUsersForLearningPath(string UserIDs, int PathID);
        public Task<IEnumerable<GroupListByManager>> GetGroupListByManagerForLearningPath(int UserID, bool ShowAll);
        public Task<IEnumerable<ConditionalUsers>> GetConditionalUsersForLearningPath(int UserID, int PathID);
        public Task<IEnumerable<AssignedGroupByPathID>> GetAssignedGroupByPathID(int UserID, int PathID);
        public Task<IEnumerable<UnAssignedGroupByPathID>> GetUnAssignedGroupByPathID(int UserID, int PathID);
        public Task<IEnumerable<SubcategoriesForLearningPath>> GetSubcategories(int UserID, int CategoryID, int Type);
        public Task<IEnumerable<DynamicFieldOptions>> GetDynamicFieldOptions(int UserID, int FieldID);
        public Task<IEnumerable<UserListByAccountIDOrGroupID>> GetUserListByAccountIDOrGroupID(int AccountID, int GroupID, DateTime? StartDate, DateTime? EndDate);
        public Task<IEnumerable<UserListByManager>> GetUserListByManager(int UserID, int GroupID);
        public Task<IEnumerable<LearningPathItems>> GetLearningPathItems(int UserID, int PathID, int GroupID, DateTime? StartDate, DateTime? EndDate);
        public Task<NotificationEmail> GetNotificationEmail(int PathID);
        public Task<bool> AssignUserToLearningPath(AssignedLearningInputs assignedLearningInputs);
        public Task<bool> InsertAssignGroupToLP(AssignGroupToLP AssignGroupToLP);
        public Task<bool> RemoveGroupFromLP(RemoveGroupFromLP RemoveGroupFromLP);
        public Task<bool> RemoveUserFromLP(RemoveUserFromLP RemoveUserFromLP);

        public Task<bool> SetDynamicGroupAttribute(SetDynamicGroupAttribute setDynamicGroupAttribute);
        public Task<bool> LPItemAction(LearningPathInput learningPathInput);
        public Task<bool> LPIntegrationsAction(LearningPlatformCourseAction LearningPlatformCourseAction);
        public Task<int> LearningPathActions(LearningPathActionsInput learningPathInput);
    }
}

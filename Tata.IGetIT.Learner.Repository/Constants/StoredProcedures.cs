namespace Tata.IGetIT.Learner.Repository.Constants
{
    public static class StoredProcedures
    {

        #region Admin Dashboard
        public const string GetTopAssessments = "rp_GetTopAssessments";
        public const string GetTopCourses = "rp_GetTopCourses";
        public const string GetTopUsers = "rp_GetTopUsers";
        public const string GetLoginMonths = "V2_rp_GetLoginMonth";
        public const string GetScoreCard = "V2_GetAdminDashboardDetails";
        public const string GetCurrentSubscription = "V2_admin_GetAccountSubscriptionDetails";
        public const string DownloadUserReport = "V2_rp_DownloadUserReport";

        #endregion

        #region Authentication and Authorization
        public const string AUTH_USER_REGISTRATION = "login_Register_V2";
        public const string CHECK_LOGIN = "login_CheckLogin_V2";
        public const string PASSWORD_RECOVERY_REQUEST = "usr_PasswordRecoveryRequest_V2";
        public const string PASSWORD_RECOVERY_RESET = "usr_PasswordRecoveryReset_V2";
        public const string RESET_USER_PASSWORD_BY_USERID = "usr_ChangePasswordByUserId_V2";
        public const string SOCIAL_VALIDATION = "social_Validation_V2";
        public const string REGISTRATION_OTP = "registration_OTP_Generation_V2";
        public const string LOGIN_LOGOUT = "login_Logout_V2";
        public const string RESET_PASSWORD_LINK_VALIDATION_V2 = "usr_Reset_Password_Link_Validation_V2";
        public const string REGISTRATION_OTP_VERIFICATION_V2 = "registration_OTP_Verification_V2";
        public const string IS_USER_EXISTS_V2 = "IsUserExists_V2";
        public const string AUTHENTICATION_SERVICE = "V2_login_igetit_authentication";
        #endregion

        #region Common
        public const string GetCategories = "wwwGetMasterCategoryList_V2";
        public const string GetSubCategoriesBasedOnCategoryID = "V2_wwwGetReleaseList";
        public const string GetTopicsBasedOnCategoryID = "wwwGetTopicList_V2";
        public const string GetPlanDetails = "V2_GetPlanDetails";
        public const string GetCourseTitles = "V2_GetCourseTitle";
        public const string GetIndividualPlans = "wwwGetIndividualPlans_V2";
        #endregion

        #region CMS
        public const string GetCMS_FormTypes = "V2_wwwGetCMS_FormTypes";//"V2_GetCMS_FormTypes";
        public const string InsertCMS_FormData = "V2_wwwInsertCMS_FormData";//"V2_InsertCMS_FormData";
        #endregion

        #region SSO
        public const string GET_ACCOUNTID_BY_DOMAIN_NAME = "app_Get_AccountID_ByDomain_V2";
        public const string APP_SSO_INFO = "app_SSOInfo";
        public const string APP_SSO_LOGGING = "app_SSOLogging";
        public const string LOGIN_CHECK_AD_LOGIN = "login_CheckADLogin";
        public const string LOGIN_SSO_REGISTER = "V2_login_SSORegister";
        public const string APP_SSO_UPDATE_USERDETAILS = "app_SSOUpdateUserDetails_V2";
        public const string SSO_LOGIN_VALIDATION = "app_LoginValidation_V2";
        public const string UpdateAddress = "User_UpdateAddress_V2";
        public const string VerifySession = "login_VerifySession";
        #endregion

        #region Dashboard
        public const string DASHBOARD_GETTRENDINGSUBSCRIPTION = "learn_GetTrendingSubscription_V2";
        public const string DASHBOARD_GETNEWCOURSELIST = "learn_GetNewCoursesList_V2";
        public const string DASHBOARD_GETCATALOG = "learn_GetCatalog_V2";
        public const string DASHBOARD_GETSCORECARD = "app_GetDashboardMetrics_V2";
        public const string DASHBOARD_GETINPROGRESSCOURSELIST = "learn_GetInProgressCourseList_V2";
        public const string DASHBOARD_GETLEARNINGPATH = "V2_lp_GetMyLearningPath";
        public const string DASHBOARD_GETRECOMMENDEDCOURSELIST = "learn_GetRecommendedCourseList_V2";
        public const string DASHBOARD_GETPEERSCOURSELIST = "learn_GetPeersCourseList_V2";
        public const string DASHBOARD_GETUPCOMINGEVENTLIST = "learn_GetUpcomingEventList_V2";
        public const string DASHBOARD_GETPROFILEDATA = "learn_GetProfileData_V2";
        public const string DASHBOARD_GETTRANSCRIPTLIST = "learn_GetTranscriptList_V2";
        public const string DASHBOARD_GETPOPULARROLES = "learn_GetPopularRoles_V2";
        public const string GET_LEADING_USERS = "rp_GetLeaderBoard_V2";
        public const string DASHBOARD_GETTIMESPENTMETRICS = "learn_GetTimeSpentMetrics_V2";
        public const string DASHBOARD_GETTIMESPENTGRAPH = "learn_GetTimeSpentGraph_V2";
        public const string DASHBOARD_GET_POPULAR_ROLE_GRAPH = "learn_GetPopularRoleGraph_V2";
        public const string DASHBOARD_GETHEROSECTIONDETAILS = "V2_GetDashboardHeroSection";
        #endregion Dashboard

        #region Headers and Menus
        public const string GetMenuItems = "GetMenuItems_V2";
        public const string GetPoints = "GetUserPoints_V2";
        public const string GetUserCartCount = "GetUserCartCount_V2";
        public const string GetUserNotifications = "GetUserNotifications_v2";
        public const string GetFavorites = "V2_lp_GetUserFavorites";
        public const string CheckTrialUser = "usr_IsPaidTrialUser_V2";
        public const string GetSubscriptions = "sub_GetUsersSubscriptions_V2";
        #endregion

        #region Hero Section
        public const string GetCurrentRole = "GetCurrentRole_V2";
        public const string GetSkillset = "GetSkillset_V2";
        public const string GetCurrentLevel = "GetCurrentLevel_V2";
        public const string GetCareerPath = "GetCareerPath_V2";
        public const string GetTargetRoleCareerPath = "GetTargetRoleCareerPath_V2";
        public const string GetTargetRoleCareerPathPercentage = "GetTargetRoleCareerPathPercentage_V2";
        public const string GetTargetRole = "GetTargetRole_V2";
        public const string GetTargetRoleCurrentLevel = "GetTargetRoleCurrentLevel_V2";
        #endregion

        #region Payment
        public const string GET_CURRENCY = "plan_GetCurrency_V2";
        public const string PLAN_GET_PLANID = "plan_GetPlanID";
        public const string PLAN_SAVEPURCHASED_PLANINFO = "plan_SavePurchasedPlanInfo_V2";
        public const string SUB_INSERT_SALESFULFILLMENT = "sub_InsertSalesFulfillment";
        public const string SUB_INSERT_SALESFULFILLMENT_V2 = "sub_InsertSalesFulfillment_V2";
        public const string SUB_CANCEL_SUBSCRIPTION = "sub_InsertInvoiceLog";
        public const string PLAN_UPDATE_PAYMENT = "V2_plan_UpdatePayment";
        public const string PLAN_GET_SUBSCRIPTIONID = "plan_Get_SubscriptionID_V2";
        public const string SUB_UPDATE_FULFILLMENT = "sub_UpdateFulfillment";
        public const string PLAN_GETACTIVE_PLANS = "plan_GetActiveplans_V2";
        public const string SUB_VERIFYAUTORENEW = "sub_VerifyAutoRenew";
        public const string SUB_EXPIREFULFILLMENT = "sub_ExpireFulfillment";
        public const string GET_PAID_TRIAL_SUBID = "wwww_GetPaidTrialSubID";
        #endregion

        #region Cart
        public const string GET_COUNTRY_LIST = "plan_GetCountryList_V2";
        public const string GET_CART_ITEMS = "plan_GetCartItems_V2";
        public const string GET_USER_SHIPPING_INFORMATION = "plan_GetUserShipping_V2";
        public const string ADD_CART_ITEM = "plan_AddToCart_V2";
        public const string DELETE_CART_ITEM = "plan_DeleteCartItem_V2";
        public const string UPDATE_CART_PURCHASE = "plan_UpdateCartPurchase_V2";
        public const string GET_PLAN_SUBSCRIPTION_DETAIL = "PlanSubscriptionInfo_V2";
        public const string VALIDATE_TRIAL_ACCOUNT = "plan_Validate_TrialAccount_V2";
        public const string GetProfessionalBundle = "plan_ProfessionalBundle_V2";
        #endregion

        #region UserSubscription
        public const string GET_SUBSCRIPTION_DETAILS = "GetUserSubscriptions_V2";
        public const string GET_USERS_PURCHASED_HISTORY = "sub_GetUsersPurchasedHistory_V2";
        public const string GetAvailableSubscription = "V2_GetAvlblSubscription";
        public const string BILLING_INFO_FOR_INVOICE = "V2_Sub_BillingInfo_Invoice";
        public const string SubscriptionDetail = "V2_SubscriptionDetail";
        #endregion

        #region TechTips
        public const string GET_TECH_TIPS = "kb_GetArticleList_V2";
        public const string GET_TOPICS = "app_GetTopics";
        public const string GET_TOPICSByCategory = "wwwGetTopicList_V2";
        #endregion

        #region Course Catalog
        public const string GET_ALL_COURSE_PROPERTIES = "lp_GetAllCourseProperties_v2";
        public const string GET_COMMON_COURSE_PROPERTIES = "V2_wwwGetCommonCourseProperties";//"V2_GetCommonCourseProperties";
        public const string GET_CATALOG_COURSES = "wwwGetCourseList_v2";
        public const string GET_COURSE_FOR_LearningPath = "lp_GetCoursesBySubcategory";
        public const string GET_COURSE_Table_OF_CONTENTS = "crs_GetTableOfContent_V2";
        public const string GET_ASSESSMENTS = "V2_wwwGetAssessmentList";
        public const string GetAssessmentProperties = "rp_GetAssessmentProperties_V2";
        #endregion

        #region LearningPaths
        public const string GET_LEARNING_PATHS_BY_MANAGER = "lp_GetLPByManager";
        public const string GET_LEARNING_PATHS_BY_ACCOUNTID = "V2_admin_GetLPByAccountID";

        #endregion

        #region QuickStart
        public const string QUICKSTART_GETQUICKSTARTGRIDDATA = "live_LiveYouTubeTrackingData_V2";
        public const string QUICKSTART_GETCATEGORY = "app_GetCategories_V2";
        public const string QUICKSTART_GETSUBCATEGORY = "app_GetSubCategories_V2";
        public const string QUICKSTART_GETQUICKSARTNEWRELEASENOTIFICATION = "acc_GetNotificationFlagValue_V2";
        public const string QUICKSTART_UPDATEQUICKSARTNEWRELEASENOTIFICATION = "acc_UpdateJunctionUserNotification_V2";
        #endregion QuickStart

        #region SkillAdvisor
        public const string GetSkillAdvisorCategories = "V2_wwwGetSkillAdvisorCategories";
        public const string GetUserTypeRoles = "V2_wwwGetUserTypeRoles";//"V2_con_GetUserTypeRoles";
        public const string GetSoftwaresList = "V2_GetMasterCategories";
        public const string GetSubscriptionList = "V2_wwwGetPersonalPlans";// "V2_SkillAdvisor_SubscriptionFilter";
        public const string GetSkillAdvisorCourses = "V2_wwwGetSkillAdvisorCourses";//"V2_GetSkillAdvisorCourses";
        public const string GetSkillAdvisorAssessments = "V2_wwwGetSkillAdvisorAssessments";//"V2_GetSkillAdvisorAssessments";
        #endregion QuickStart

        #region Transcript
        public const string GET_TRANSCRIPT_USER_DETAILS = "rp_GetMyReportInfo_V2";
        public const string GET_TRANSCRIPT_COURSE_HISTORY = "rp_GetMyReportCourse_V2";
        public const string GET_TRANSCRIPT_ASSESSMENT_HISTORY = "rp_GetMyReportAssessment_V2";
        public const string GET_TRANSCRIPT_Public_URL = "V2_rp_GetUserTranscriptForPublic";
        #endregion

        #region UserProfile
        public const string USERPROFILE_GETPERSONALPROFILE = "usr_GetUserProfile_V2";
        public const string USERPROFILE_GETBUSINESSPROFILE = "usr_GetUserProfile_V2";
        public const string USERPROFILE_GETNOTIFICATIONSETTINGS = "usr_GetUserProfileSettings_V2";
        public const string USERPROFILE_UPDATENOTIFICATIONSETTINGS = "usr_UpdateNotificationSetting_V2";
        public const string USERPROFILE_UPDATEPERSONALPROFILE = "usr_UpdatePersonalProfile_V2";
        public const string USERPROFILE_UPDATEBUSINESSPROFILE = "usr_UpdateBusinessProfile_V2";
        public const string USERPROFILE_GETBUSINESSMANAGER = "usr_GetBusinessManagerByUserID_V2";
        public const string USERPROFILE_GETCOUNTRY = "app_GetCountryList_V2";
        public const string USERPROFILE_GETSTATEBYCOUNTRY = "app_GetStateList_V2";
        public const string USERPROFILE_GETINDUSTRYINFO = "app_GetIndustryInfo_V2";
        public const string USERPROFILE_GETCADAPPLICATIONLIST = "app_GetCadApplicationList_V2";
        public const string USERPROFILE_GETUSERGROUPS = "acc_GetGroupNamesForUser_V2";
        #endregion UserProfile

        #region Learning
        public const string LEARNING_GETHISTORY = "lp_GetUserHistory_V2";
        public const string LEARNING_GETLEARNINGPATH = "lp_GetMyLearningPath_V2";
        public const string LEARNING_GETMYLEARNING = "lp_GetMyLearning_V2";
        public const string LEARNING_GETMYLEARNINGCOURSE = "V2_lp_GetCoursesForTraining";
        public const string LEARNING_GETMYLEARNINGASSESSMENT = "V2_lp_GetAssessmentsForTraining";
        public const string LEARNING_ADDFAVORITEITEM = "lp_AddFavoriteItem_V2";
        public const string LEARNING_REMOVEFAVORITEITEM = "lp_RemoveFavoriteItem_V2";
        public const string LEARNING_DOWNLOADCERTIFICATE = "rp_GetDownloadCertificateInfo_V2";
        #endregion Learning

        #region Roles , Skill, Competency

        public const string ROLESKILLCOMPETENCY_GETROLESLIST = "V2_admin_GetRolesList";
        public const string ROLESKILLCOMPETENCY_GETROLEDETAILS = "V2_admin_GetRole";
        public const string ROLESKILLCOMPETENCY_CREATEROLE = "V2_admin_AddEditRole";
        public const string ROLESKILLCOMPETENCY_UPDATEROLE = "V2_admin_AddEditRole";
        public const string ROLESKILLCOMPETENCY_DELETEROLE = "V2_admin_DeleteRole";
        public const string COMPETENCY_GETCOMPETENCYLIST = "V2_admin_GetCompetencyList";
        public const string COMPETENCY_GETCOMPETENCYDETAILS = "V2_admin_GetCompetency";
        public const string COMPETENCY_CREATECOMPETENCY = "V2_admin_AddEditCompetency";
        public const string COMPETENCY_UPDATECOMPETENCY = "V2_admin_AddEditCompetency";
        public const string COMPETENCY_DELETECOMPETENCY = "V2_admin_DeleteCompetency";
        public const string COMPETENCY_GETCOMPETENCYLEVELLIST = "V2_admin_GetCompetencyLevelList";
        public const string COMPETENCY_GETCOMPETENCYTYPE = "V2_admin_GetCompetencyType";
        public const string ROLESSTRUCTURE_GETSTRUCTURELIST = "V2_admin_GetStructureList";
        public const string ROLESSTRUCTURE_GETSTRUCTUREDETAILS = "V2_admin_GetStructure";
        public const string ROLESSTRUCTURE_ADDSTRUCTURE = "V2_admin_AddStructure";
        public const string ROLESSTRUCTURE_CREATESTRUCTURE = "V2_admin_AddEditStructure";
        public const string ROLESSTRUCTURE_UPDATESTRUCTURE = "V2_admin_AddEditStructure";
        public const string ROLESSTRUCTURE_DELETESTRUCTURE = "V2_admin_DeleteStructure";
        public const string ROLESSTRUCTURE_GETROLESTRUCTURELEVELMAP = "V2_admin_GetRoleStructureLevelMap";
        public const string ROLESSTRUCTURE_GETROLECOMPETENCYLEVELMAP = "V2_admin_GetRoleCompetencyLevelMap";
        public const string ROLESSTRUCTURE_GETROLECOMPETENCYLPMAP = "V2_admin_GetRoleCompetencyLPMap";
        public const string USERPROFILE_ADDCOMPETENCYMAP = "V2_usr_AddEditUserRoleCompetencyMap";
        public const string USERPROFILE_EDITCOMPETENCYMAP = "V2_usr_AddEditUserRoleCompetencyMap";
        public const string USERPROFILE_GETUSERROLECOMPETENCYMAP = "V2_usr_GetUserRoleCompetency";
        public const string USERPROFILE_GETLPBYROLEID = "V2_admin_GetLPByRoleId";
        
        #endregion Roles, Skill, Competency

        #region AccountManagement

        #region AssignedLearing

        public const string AssingnedLearning_GetCategories = "app_GetCategories_V2";
        public const string AssingnedLearning_GetSubCategories = "app_GetSubCategories_V2";
        public const string AssingnedLearning = "lp_GetLPByManager";
        public const string GetSubcategories = "app_GetSubcategories_V2";
        public const string PredefinedLearningPath = "lp_GetLPByManager";
        public const string CoursesForLearningPath = "lp_GetCoursesForLP";
        public const string IntegrationsForLearningPath = "V2_lp_LPIntegrationAction";
        public const string InsertAssignGroupToLP = "V2_lp_AssignGroupToLP";
        public const string DeleteAssignGroupToLP = "V2_lp_RemoveGroupFromLP";
        public const string DeleteUserFromLP = "V2_lp_RemoveUserFromLP";
        public const string GetPathIDtoCreateLP = "V2_GetPathIDtoCreateLP";
        public const string GetLearningPlatformCoursesForLP = "V2_lp_GetLearningPlatformCoursesForLP";
        public const string GetUserListByManager = "V2_rp_GetUserListByManager";
        public const string AssessmentsForLP = "lp_GetAssessmentsForLP";
        public const string AssignedUsersForLP = "lp_GetAssignedUsersForLP_v2";
        public const string GroupListByManager = "acc_GetGroupListByManager";
        public const string GetConditionalUsers = "lp_GetConditionalUsers_V2";
        public const string AssignUserToLP = "lp_AssignUserToLP_V2";
        public const string GetUnAssignedGroupByPathID = "lp_GetUnAssignedGroupByPathID";
        public const string GetAssignedGroupByPathID = "lp_GetAssignedGroupByPathID";
        public const string GetNotificationEmail = "lp_GetNotificationEmail";
        public const string GetDynamicFieldOptions = "V2_acc_GetDynamicFieldOptions";
        public const string GetUserListByAccountIDOrGroupID = "usr_GetUserListByAccountIDOrGroupID";
        public const string SetDynamicGroupAttribute = "V2_lp_SetDynamicGroupAttribute";
        public const string GetLearningPathItems = "V2_rp_GetLearningPathItems";
        public const string GetLearningPathItemAction = "V2_lp_LPItemAction";
        public const string SetLearningPathItemActions = "lp_LearningPathActions";
        #endregion

        #endregion

        #region "Trainings"
        public const string ListTrainings = "V2_event_list_trainings";
        public const string GetTrainings = "V2_event_get_trainings";
        public const string CreateTraining = "V2_event_create_training";
        public const string UpdateTraining = "V2_event_update_training";
        public const string DeleteTraining = "V2_event_delete_training";

        public const string CreateMeetingSession = "V2_event_create_session";
        public const string UpdateMeetingSession = "V2_event_update_session";
        public const string DeleteMeetingSession = "V2_event_delete_session";
        public const string GetSession = "V2_event_get_session";
        public const string GetPlatforms = "V2_event_platforms";
        public const string ListSessions = "V2_event_list_sessions";

        public const string CalendarSessions = "V2_event_list_calendar_sessions";

        #endregion

        #region Discussion Forum
        public const string FORUM_CREATE_POSTING = "V2_Forum_Create_Posting";
        public const string FORUM_UPDATE_POSTING = "V2_Forum_Update_Posting";
        public const string FORUM_DELETE_POSTING = "V2_Forum_Delete_Posting";
        public const string FORUM_CREATE_REPLY = "V2_Forum_Create_Reply";
        public const string FORUM_UPDATE_REPLY = "V2_Forum_Update_Reply";
        public const string FORUM_DELETE_REPLY = "V2_Forum_Delete_Reply";
        public const string FORUM_HIDE_REPLY = "V2_Forum_Hide_Reply";
        public const string FORUM_LIST = "V2_Forum_List";

        #endregion

        #region Notification

        public const string GET_USERNOTIFICATION_EMAILCONTENT = "V2_usr_GetUserNotificationEmailContent";

        #endregion Notification




    }

    public static class Tables
    {
        public const string USER = "User_Sample";
    }
}
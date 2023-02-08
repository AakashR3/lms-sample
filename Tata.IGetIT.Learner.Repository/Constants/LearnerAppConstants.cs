using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Constants
{
    public static class LearnerAppConstants
    {
        public const string INVALID_CONFIGURATION = "Invaid configuration!";
        public const string ACCOUNT_DISABLED = "Your account is in disabled state. Please contact the administrator or the support team.";
        public const string ACCOUNT_EXPIRED = "Your account got expired!";
        public const string BAD_REQUEST = "Bad Request!";
        public const string OK_MESSAGE = "{0}";
        public const string OK = "OK";
        public const string EMAILID_REQUIRED = "Email ID is required!";
        public const string EXCEPTION_MESSAGE = "The application has encountered an unknown error in the method";
        public const string FIRSTNAME_REQUIRED = "First name is required!";
        public const string FORGOT_PASSWORD_TEMPLATE_FILE_MISSING = "Forgot password email template does not exist!";
        public const string GENERIC_EXCEPTION_MESSAGE = "An internal error occurred in the system. Please contact the technical support team!";
        public const string INVALID_ACCOUNTID = "Invalid Account ID!";
        public const string EmptyToAddress = "Email To address is empty!";
        public const string EmptyEmailSubject = "Email subject is empty!";
        public const string EmptyEmailContent = "Email content is empty!";
        public const string InvalidToAddress = "Invalid To Address!";
        public const string INVALID_CREDENTIALS = "Invalid credentials!";
        public const string INVALID_DOMAINNAME = "Invalid domain name!";
        public const string INVALID_EmailID = "Invalid Email ID!";
        public const string INVALID_EMAILSESSIONID = "Invalid email session ID!";
        public const string INVALID_EMAILSESSIONID_OR_PASSWORD = "Invalid email session ID or password!";
        public const string INVALID_LOGINTYPE = "Login type is invalid!";
        public const string INVALID_OTP = "Invalid OTP!";
        public const string INVALID_REQUEST = "Invalid request!.";
        public const string TRIAL_USER = "Your 7 days trial account is expiring in {0} days";
        public const string INVALID_SESSIONID = "Invalid session ID!";
        public const string INVALID_TOKENID = "Invalid token ID!";
        public const string SESSIONID_REQUIRED = "Sessionid is required!";
        public const string INVALID_USER = "Invalid User!";
        public const string INVALID_USERNAME = "Invalid username!";
        public const string INVALID_USERTYPE = "Invalid User Type!";
        public const string INVALID_AccountTypeId = "Invalid Account Type!";
        public const string INVALID_TimeOutMin = "Invalid Time Out Min!";
        public const string USERNAME_REQUIRED = "Username is required!";
        public const string CURRENT_PASSWORD_REQUIRED = "Current password is required!";
        public const string NEW_PASSWORD_REQUIRED = "New password is required!";

        public const string LASTNAME_REQUIRED = "Last name is required";
        public const string LINKED_IN_CODE = "Valid Linked-in code is required";
        public const string LOGIN_SUCCESS_MESSAGE = "Login successful.";
        public const string LOGOUT_SESSION = "Invalid session ID!";
        public const string LOGOUT_SUCCESSFUL = "Logged out successfully!";
        public const string MODEL_VALIDATION_FAILED = "Model validation failed!";
        public const string OTP_EXPIRED = "OTP got expired!";
        public const string OTP_LIMIT_EXCEEDED = "Maximum OTP limit exceeded for the day!";
        public const string OTP_VERIFICATION_SUCCESS = "OTP verification is successfully completed!";
        public const string PASSWORD_INACTIVE_USER = "Password cannot be sent for an inactive user!";
        public const string PASSWORD_REQUIRED = "Password is required!";
        public const string PASSWORD_RESET_EMAIL_SUCCESS_MESSAGE = "Password reset email has been sent successfully!";
        public const string PASSWORD_UPDATE_SUCCESS = "Password is updated successfully!";
        public const string REGISTERED_USER_UNABLE_TO_ACCESSS = "You're a registered user. However, you will not be able to access this application!. Please contact the administrator or the support team.";
        public const string REGISTRATION_SUCCESS_MESSAGE = "Congratulations! You are successfully registered to use i Get IT Application!";
        public const string RESET_LINK_EXPIRED = "Password reset link is expired!";
        public const string SOCIAL_LOGIN_SUCCESS_MESSAGE = "Social login is successful";
        public const string UNABLE_TO_LOGOUT = "Unable to logout!";
        public const string UNABLE_TO_REGISTER = "Unable to register, email or username is already exists!";
        public const string UNABLE_TO_RETRIEVE_EMAILID = "Unable to retrieve the email ID!";
        public const string UNABLE_TO_RETRIEVE_SOCIALID = "Unable to retrieve social ID!";
        public const string UNABLE_TO_RETRIEVE_EMAILID_OR_SOCIALID = "Unable to retrieve the email ID or social Id!";
        public const string USER_ALREADY_EXISTS = "User is already exists!";
        public const string USER_ALREADY_REGISTERED = "User is already registered";
        public const string USER_NAME_AND_PASSWORD_REQUIRED = "User name and password are required!";
        public const string USER_NAME_OR_PASSWORD_REQUIRED = "User name or password is required!";
        public const string USEREPO_NULL = "UsersRepo cannot be null!";
        public const string USERID_PASSWORD_REQUIRED = "Valid user ID and password are required!";
        public const string VALID_PASSWORD_LINK = "Password reset link is valid!";
        public const string VERIFICATION_CODE_SENT = "The verification code has been sent to your email address!";
        public const string VERIFY_EMAIL_TEMPLATE_FILE_MISSING = "Verify email OTP template does not exist!";
        public const string VALID_TOKEN = "Token is valid!";
        public const string INVALID_TOKEN = "Token is invalid!";
        public const string RECURLY_CONFIG_NOT_FOUND = "Recurly payment gateway config is null or not found.";
        public const string RECURLY_KEY_IS_NULL_OR_EMPTY = "Recurly payment api key is null or empty.";
        public const string UNABLE_TO_GENERATE_TOKEN = "Unable to generate token!";
        public const string PASSWORD_CHANGED_SUCCESSFULLY = "Password changed successfully!";
        public const string CURRENT_PASSWORD_NOT_MATCH = "Your current password does not match!";
        public const string CURRENT_AND_NEW_PASSWORD_MATCH = "Your current password and new password should not be same!";
        public const string INVALID_SESSIONID_OR_EXPIRED = "Invalid session id or expired!";
        public const string USER_AUTHENTICATED = "User authenticated successfully!";

        #region SSO   
        public const string DOMAIN_NAME_DOES_NOT_EXIST = "Domain name does not exist!";
        public const string UNABLE_TO_RETRIEVE_ACCOUNT = "Unable to retrieve your account!";
        public const string SSO_URL_GENERATED = "SSO URL generated!";
        public const string INVALID_DOMAIN_NAME = "Invalid domain name!";
        public const string UNABLE_TO_REGISTER_SSO_DETAILS = "Unable to register SSO details!";
        public const string UNABLE_TO_UPDATE_USER_DETAILS = "Unable to update user details!";
        public const string UNABLE_TO_ADD_SSO_LOG = "Unable to add SSO log!";
        public const string USER_DOES_NOT_EXISTS = "User does not exists!";
        public const string SSO_AUTHENTICATION_SUCCESSFUL = "SSO authentication successful!";
        public const string SSO_INVALID_RESPONSE = "Invalid response received!";
        public const string SSO_REDIRECT_URL_QUERY_STRING_FAILURE = "?status=failure&message=";///Not a message//
        public const string SSO_REDIRECT_URL_QUERY_STRING_SUCCESS = "?status=success&message=";///Not a message//

        #endregion

        #region Payment

        public const string CURRENCY_FOUND = "Currency found!";
        public const string CURRENCY_NOT_FOUND = "Currency not found!";
        public const string CURRENCY_CODE_REQUIRED = "Currency code is required!";
        public const string EMAIID_REQUIRED = "Emain id is required!";
        public const string CONTACTNO_REQUIRED = "Contact number is required!";
        public const string ADDRESS_REQUIRED = "Address is required!";
        public const string ADDRESS1_REQUIRED = "Address1 is required!";
        public const string CITY_REQUIRED = "City is required!";
        public const string STATE_REQUIRED = "State is required!";
        public const string POSTALCODE_REQUIRED = "PostalCode is required!";
        public const string COUNTRY_REQUIRED = "Country is required!";
        public const string COUNTRYCODE_REQUIRED = "Country code is required!";
        public const string INVALID_PLANCODE = "Invalid plancode!";
        public const string INVALID_PLANNAME = "Invalid planname!";
        public const string UNABLE_TO_GENERATE_SIGNATURE = "Unable to generate signature!";
        public const string PAYMENT_INITIATED = "Payment initiated!";
        public const string WEBHOOK_PROCESSED = "Webhook processed succesfully!";
        public const string UNABLE_PROCESSED_WEBHOOK = "Unable to processed!";

        public const string WEBHOOK_INVALID_DATA = "Invalid signature or data!";

        public const string RAZORPAY_PLAN_CODE_REQUIRED = "Plan code is required!";
        public const string RAZORPAY_CURRENCY_CODE_REQUIRED = "Currency code is required!";
        public const string RAZORPAY_ISTRIAL_REQUIRED = "IsTrial is required!";
        public const string RAZORPAY_PAYMENT_INITIATED = "Payment initiated!";
        public const string RAZORPAY_FNAME_REQUIRED = "First name is required!";
        public const string RAZORPAY_LNAME_REQUIRED = "Last name is required!";
        public const string RAZORPAY_EMAIID_REQUIRED = "Emain id is required!";
        public const string RAZORPAY_CONTACTNO_REQUIRED = "Contact number is required!";
        public const string RAZORPAY_ADDRESS_REQUIRED = "Address is required!";
        public const string RAZORPAY_SIGNATURE_REQUIRED = "Signature is required!";
        public const string RAZORPAY_PAYMENTID_REQUIRED = "Payment id is required!";
        public const string RAZORPAY_PAYMENT_SUCCESS = "Payment is successful!";
        public const string RAZORPAY_PAYMENT_FAILURE = "Payment failure!";
        public const string RAZORPAY_UNABLE_TO_COMPLETE_PAYMENT = "Unable to complete payment process!";

        public const string RAZORPAY_PAYMENT_TYPE_INVALID = "Payment type is invalid!";
        public const string RAZORPAY_QUANTITY_REQUIRED = "Quantity is required!";
        public const string RAZORPAY_NO_OF_USER_REQUIRED = "No of user is required!";
        public const string RAZORPAY_USERID_REQUIRED = "User id is required!";
        public const string RAZORPAY_ACCOUNTID_REQUIRED = "Account id is required!";
        public const string RAZORPAY_INVALID_PLAN = "Invalid Plan!";
        public const string RAZORPAY_UNABLE_TO_CREATE_ORDER = "Unable to create order!";
        public const string RAZORPAY_UNABLE_TO_CREATE_SUBSCRIPTION = "Unable to create subscription!";
        public const string RAZORPAY_CART_ID_REQUIRED = "Cart id is required!";
        public const string RAZORPAY_SUBSCRIPTION_ID_REQUIRED = "Subscription id is required!";
        public const string UNABLE_TO_CANCEL_SUBSCRIPTION = "Unable to cancel subscription!";
        public const string RAZORPAY_SUBSCRIPTION_CANCELLED = "Subscription cancelled!";
        public const string PURCHASETYPE_REQUIRED = "PurchaseType is required!";


        public const string RECURLY_INVALID_COUPON_CODE = "Couldn't find Coupon with code";
        public const string INVALID_COUPON_CODE = "Invalid coupon code";

        public const string RECURLY_GENERAL_EXCEPTION = "There was an error validating your request";
        public const string GENERAL_EXCEPTION = "Kindly enter card/bank details details to proceed further!";

        public const string RECURLY_ADDRESS_CITY_COUNTRY_EMPTY = "Address1 can't be empty, City can't be empty, Country can't be empty, and Postal code can't be empty";
        public const string ADDRESS_CITY_COUNTRY_EMPTY = "Please fill the address details before proceed!";

        public const string COUPON_CODE_REQUIRED = "Coupon code is required!";

        #endregion

        #region Cart
        public const string COUNTRIES_FOUND = "Country listed!";
        public const string COUNTRY_NOT_FOUND = "Country not found!";
        public const string NO_CART_ITEMS_FOUND = "No cart items found!";
        public const string INVALID_CARTID = "Invalid Cart ID!";
        public const string INVALID_USERID = "Invalid User ID!";
        public const string CART_ITEMS_FOUND = "Cart items found!";
        public const string SHIPPING_INFORMATION_FOUND = "Shipping information found!";
        public const string INVALID_PRURCHASE_TYPE = "Invalid purchase type!";
        public const string INVALID_ARGUMENTS = "Invalid inputs!";
        public const string ISSUE_CART_INSERT = "There is an issue adding cart item!";
        public const string ISSUE_CART_DELETE = "There is an issue deleting cart item!";
        public const string CART_ITEM_DOESNOT_EXIST = "Cart item does not exist or already purchased!";
        public const string USER_ALREADY_SUBSCRIBED = "User has already subscribed the same course!";
        public const string USER_ALREADY_SAME_ITEM = "User already has the same course in the cart!";
        public const string UNABLE_INSERT_CART_ITEM = "Cart item is not inserted!";
        public const string CART_ITEM_ADDED = "Cart item is added successfully!";
        public const string CART_ITEM_DELETED = "Cart item is deleted successfully!";
        public const string SHIPPING_INFO_NOT_FOUND = "Shipping information not found!";
        public const string SEVEN_DAYS_TRIAL_ADDED = "Trial added successfully!";
        public const string SEVEN_DAYS_MODIFIED_SUCCESSFULLY = "Trial modified successfully!";
        public const string NO_CART_ITEM_SELECTED = "No cart item selected!";
        public const string PLAN_PURCHASE_NOT_TRIAL = "Plan Purchase does not have trial selection!";
        #endregion

        #region Dashboard

        public const string DASHBOARD_SUCCESSMESSAGE = "Success";
        public const string DASHBOARD_FAILUREMESSAGE = "No Data";
        public const string DASHBOARD_TRENDINGSUBSCRIPTION_FAILUREMESSAGE = "No Trending Subscription";
        public const string DASHBOARD_CATALOG_FAILUREMESSAGE = "No Catalog List";
        public const string DASHBOARD_NEWCOURSE_FAILUREMESSAGE = "No New Courses List";
        public const string DASHBOARD_INPROGRESSCOURSE_FAILUREMESSAGE = "No In Progress Course List";
        public const string DASHBOARD_LEARNINGPATH_FAILUREMESSAGE = "No Learning Path";
        public const string DASHBOARD_RECOMMENDEDCOURSE_FAILUREMESSAGE = "No Recommended Course List";
        public const string DASHBOARD_PEERSCOURSE_FAILUREMESSAGE = "No Peers Course List";
        public const string DASHBOARD_PROFILE_FAILUREMESSAGE = "No Profile";
        public const string DASHBOARD_TRANSCRIPT_FAILUREMESSAGE = "No Transcript List";
        public const string DASHBOARD_UPCOMINGEVENTS_FAILUREMESSAGE = "No Upcoming Events List";
        public const string DASHBOARD_POPULARROLES_FAILUREMESSAGE = "No Popular Roles";
        public const string DASHBOARD_LEADERBOARD_FAILUREMESSAGE = "No Leader Board data";
        public const string DASHBOARD_HEROSECTION_FAILUREMESSAGE = "No Hero section data";

        #endregion Dashboard


        #region UserSubscription
        public const string NO_SUBSCRIPTION = "User has no subscriptions!";
        public const string NO_Available_SUBSCRIPTION = "There is no available subscription!";
        public const string SUBSCRIPTION_FOUND = "User has subscriptions!";
        public const string USER_IN_TRIAL_ACCOUNT = "User already in trial account!";
        public const string INVOICE_FOUND = "Invoice found!";
        public const string UNABLE_TO_FOUND_INVOICE = "Unable to found Invoice!";
        #endregion

        #region Course Catalog
        public const string Success = "Success!";
        public const string Failure = "Fail!";
        public const string NO_COURSES_FOUND = "No courses found!";
        public const string NoRecordsFound = "No records found!";
        public const string NoRecordsFoundDynamic = "No {0} found!";
        public const string NoRecordIsInserted = "Unable to add. Please contact the technical support team!";
        public const string SuccessUpdate = "Successfully Updated!";
        public const string DatabaseOperationFailed = "Unsuccessful! Please contact the technical support team!";
        public const string Invalid_Inputs = "Invalid Inputs!";

        #endregion

        #region Coupon
        public const string COUPON_VALID = "Recurly Coupon code is valid";
        public const string COUPON_IS_NOT_VALID = "Recurly Coupon code is not valid";
        #endregion

        #region QuickStart

        public const string SUCCESSMESSAGE = "Success";
        public const string QUICKSTART_GRIDDATA_FAILUREMESSAGE = "No Quick Start Data";
        public const string QUICKSTART_GETCATEGORY_FAILUREMESSAGE = "No Category Data";
        public const string QUICKSTART_GETSUBCATEGORY_FAILUREMESSAGE = "No Sub-Category Data";
        public const string QUICKSTART_GETQUICKSTARTNOTIFICATION_FAILUREMESSAGE = "No Quick Start Notification Flag";

        public const string QUICKSTART_NEWRELEASEFLAG = "NewQuickStartsRelease";

        public const int QUICKSTART_PAGSIZE = 8;

        #endregion QuickStart

        #region UserProfile

        public const string USERPROFILE_PERSONALPROFILE_SUCCESSMESSAGE = "Personal Profile Updated successfully";
        public const string USERPROFILE_BUSINESSPROFILE_SUCCESSMESSAGE = "Business Profile Updated successfully";
        public const string USERPROFILE_PERSONALPROFILE_FAILUREMESSAGE = "No Personal Profile Data";
        public const string USERPROFILE_BUSINESSPROFILE_FAILUREMESSAGE = "No Business Profile Data";
        public const string USERPROFILE_NOTIFICATIONSETTING_SUCCESSMESSAGE = "Notification Setting Updated successfully";
        public const string USERPROFILE_NOTIFICATIONSETTING_FAILUREMESSAGE = "No Notification Setting";

        public const string USERPROFILE_BUSINESSMANAGER_FAILUREMESSAGE = "No Business Manager";
        public const string USERPROFILE_COUNTRY_FAILUREMESSAGE = "No Country Data";
        public const string USERPROFILE_STATE_FAILUREMESSAGE = "No State Data";
        public const string USERPROFILE_INDUSTRYINFO_FAILUREMESSAGE = "No Industry Info Data";
        public const string USERPROFILE_CADAPPLICATIONLIST_FAILUREMESSAGE = "No CAD Application List Data";
        public const string USERPROFILE_USERGROUPS_FAILUREMESSAGE = "No User Groups";

        public enum NotificationTypeDef
        {
            Insights = 1,
            PendingCourse,
            Trial,
            ProfileCompletion,
            NewsLetter,
            MarketingEmail,
            QuickStartVideo
        }

        public enum NotificationFrequency
        {
            Daily = 'D',
            Weekly = 'W'
        }

        #endregion UserProfile


        #region Subscriptions
        public const string PURCHASE_HISTORY_FOUND = "Purchase history found!";
        public const string PURCHASE_HISTORY_NOT_FOUND = "Purchase history not found!";

        #endregion

        #region Learning

        public const string LEARNING_ADDFAVORITE_SUCCESSMESSAGE = "Item added to favorite successfully";
        public const string LEARNING_REMOVEFAVORITE_SUCCESSMESSAGE = "Item removed from favorite successfully";
        public const string LEARNING_GETHISTORY_FAILUREMESSAGE = "No History Data";
        public const string LEARNING_GETLEARNINGPATH_FAILUREMESSAGE = "No Learning Path Data";
        public const string LEARNING_GETMYLEARNINGCOURSE_FAILUREMESSAGE = "No courses available for the selected criteria, please check in Catalog";
        public const string LEARNING_GETMYLEARNINGASSESSMENT_FAILUREMESSAGE = "No assessment available for the selected criteria, please check in Catalog";
        public const string LEARNING_ADDFAVORITE_FAILUREMESSAGE = "Add Favorite Item";
        public const string LEARNING_REMOVEFAVORITE_FAILUREMESSAGE = "Remove Favorite Item";
        public const string LEARNING_DOWNLOAD_FAILUREMESSAGE = "No Certificate Download Info";

        public const int LEARNING_PAGSIZE = 10;

        #endregion Learning

        #region Roles, Skill, Competency

        public const string INVALID_ID = "Invalid ID!";
        public const string INVALID_ROLEID = "Invalid Role ID!";
        public const string ROLESKILLCOMPETENCY_GETROLESLIST_FAILUREMESSAGE = "No Roles List";
        public const string ROLESKILLCOMPETENCY_GETROLESDETAILS_FAILUREMESSAGE = "No Role Details";
        public const string ROLESKILLCOMPETENCY_ROLE_ADDED = "Role is added successfully!";
        public const string ROLESKILLCOMPETENCY_ROLE_UPDATED = "Role is updated successfully!";
        public const string ROLESKILLCOMPETENCY_ROLE_DELETED = "Role is deleted successfully!";
        public const string ISSUE_ROLE_INSERT = "There is an issue adding role!";
        public const string ISSUE_ROLE_DELETE = "There is an issue deleting role!";
        public const string ISSUE_ROLE_DOESNOTEXIST = "Role does not exist!";
        public const string ISSUE_UNABLE_INSERT_ROLE = "Role is not inserted!";
        public const string ISSUE_ROLENAME_EXISTS = "Role Name exists for the account!";
        public const string ROLES_PUBLICROLES_ADDED = "Public Role(s) are added successfully!";
        public const string ROLES_PUBLICROLES_ADDED_WITHFAILURE = "Public Role(s) are added with few omissions!";

        public const string INVALID_COMPETENCYID = "Invalid Competency ID!";
        public const string COMPETENCY_GETLIST_FAILUREMESSAGE = "No Competency List";
        public const string COMPETENCY_GETLEVEL_FAILUREMESSAGE = "No Competency Level List";
        public const string COMPETENCY_GETDETAILS_FAILUREMESSAGE = "No Competency Details";
        public const string COMPETENCY_GETTYPE_FAILUREMESSAGE = "No Competency Type List";
        public const string COMPETENCY_ADDED = "Competency is added successfully!";
        public const string COMPETENCY_UPDATED = "Competency is updated successfully!";
        public const string COMPETENCY_DELETED = "Competency is deleted successfully!";
        public const string ISSUE_COMPETENCY_INSERT = "There is an issue adding competency!";
        public const string ISSUE_COMPETENCY_DELETE = "There is an issue deleting competency!";
        public const string ISSUE_COMPETENCY_DOESNOTEXIST = "Competency does not exist!";
        public const string ISSUE_UNABLE_INSERT_COMPETENCY = "Competency is not inserted!";
        public const string ISSUE_COMPETENCYNAME_EXISTS = "Competency Name exists for the account!";

        public const string INVALID_STRUCTUREID = "Invalid Structure ID!";
        public const string STRUCTURE_GETLIST_FAILUREMESSAGE = "No Structure List";
        public const string STRUCTURE_GETDETAILS_FAILUREMESSAGE = "No Structure Details";
        public const string STRUCTURE_ADDED = "Structure is added successfully!";
        public const string STRUCTURE_UPDATED = "Structure is updated successfully!";
        public const string STRUCTURE_DELETED = "Structure is deleted successfully!";
        public const string ISSUE_STRUCTURE_INSERT = "There is an issue adding structure!";
        public const string ISSUE_STRUCTURE_DELETE = "There is an issue deleting structure!";
        public const string ISSUE_STRUCTURE_DOESNOTEXIST = "Structure does not exist!";
        public const string ISSUE_UNABLE_INSERT_STRUCTURE = "Structure is not inserted!";
        public const string ISSUE_STRUCTURENAME_EXISTS = "Structure Name exists for the account!";
        public const string STRUCTURE_GETLPLIST_FAILUREMESSAGE = "No Learning Path for Account ID";
        public const string STRUCTURE_GETLPLISTROLE_FAILUREMESSAGE = "No Learning Path for Role ID";

        public const string USERROLECOMPETENCY_ADDED = "Role and Competency is mapped successfully!";
        public const string USERROLECOMPETENCY_UPDATED = "Role and Competency mapping is updated successfully!";
        public const string ISSUE_USERROLECOMPETENCY_INSERT = "There is an issue mapping Role, Structure and Competency with User !";
        public const string USERROLECOMPETENCY_GETLIST_FAILUREMESSAGE = "No Role and Competency Mapping for the user";

        #endregion Roles, Skill, Competency

        #region Training
        public const string MEETING_CREATED_SUCCESSFULLY = "Meeting created successfully!";
        public const string MEETING_UPDATED_SUCCESSFULLY = "Meeting updated successfully!";
        public const string UNABLE_TO_CREATE_MEETING = "Unable to create meeting!";
        public const string UNABLE_TO_UPDATE_MEETING = "Unable to update meeting!";
        public const string UNABLE_TO_DELETE_MEETING = "Unable to delete meeting!";
        public const string MEETING_DELETED_SUCCESSFULLY = "Meeting deleted successfully!";
        public const string PLATFORM_DETAILS_NOT_FOUND = "Platform details not found!";

        public const string TRAININGS_NOT_FOUND = "Trainings are not found for the user!";

        public const string UNABLE_TO_CREATE_TRAINING = "Training creation failed.";
        public const string UNABLE_TO_UPDATE_TRAINING = "Training update failed.";
        public const string UNABLE_TO_DELETE_TRAINING = "Training delete failed.";


        public const string TRAINING_CREATED_SUCCESSFULLY = "Training created successfully.";
        public const string TRAINING_UPDATED_SUCCESSFULLY = "Training updated successfully.";
        public const string TRAINING_DELETED_SUCCESSFULLY = "Training deleted successfully.";

        public const string MEETING_FEATURE_UNAVAILABLE = "This meeting feature is currently unavailable!";

        #endregion


        #region Discussion Forum
        public const string INVALID_POSTID = "Invalid post id!";
        public const string POSTID_DOES_NOT_EXIST = "Post id does not exist!";
        public const string INVALID_POST_STATUS_CLOSED = "Invalid post status or closed!";
        public const string INVALID_COURSEID = "Invalid course id!";
        public const string TITLE_REQUIRED = "Title is required!";
        public const string REPLY_TEXT_REQUIRED = "ReplyText is required!";
        public const string DESCRIPTION_REQUIRED = "Description is required!";
        public const string POSTING_ADDED_SUCCESSFULLY = "Posting added successfully!";
        public const string POSTING_UPDATED_SUCCESSFULLY = "Posting updated successfully!";
        public const string POSTING_DELETED_SUCCESSFULLY = "Posting deleted successfully!";
        public const string UNABLE_TO_CREATE_POSTING = "Posting not created!";
        public const string UNABLE_TO_UPDATE_POSTING = "Posting not updated!";
        public const string UNABLE_TO_DELETE_POSTING = "Unable to delete post!";
        public const string UNABLE_TO_CREATE_REPLY = "Failed to add reply!";
        public const string UNABLE_TO_UPDATE_REPLY = "Failed to update reply!";
        public const string UNABLE_TO_DELETE_REPLY = "Unable to delete reply!";
        public const string SUPPORTED_FILE_FORMAT = "Application supports Images or PDF or ZIP file types !";
        public const string INVALID_REPLY_STATUS = "Invalid reply id or status!";
        public const string REPLY_ADDED_SUCCESSFULLY = "Reply added successfully!";
        public const string REPLY_UPDATED_SUCCESSFULLY = "Reply modified successfully!";
        public const string REPLY_DELETED_SUCCESSFULLY = "Reply deleted successfully!";
        public const string REPLY_REPORTED_SUCCESSFULLY = "Reported successfully!";
        public const string UNABLE_TO_MODIFY_DETAILS = "Unable to modify!";
        public const string RESOURCE_FOUND = "Resource found!";
        public const string RESOURCE_NOT_FOUND = "Resource not found!";
        #endregion

        #region Notification

        public const string NOTIFICATION_DATA_FAILUREMESSAGE = "No Notification Data";

        #endregion Notification

    }

    public static class ExceptionMessageConstants
    {

    }

}

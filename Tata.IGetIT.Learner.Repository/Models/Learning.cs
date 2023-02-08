namespace Tata.IGetIT.Learner.Repository.Models
{
    public class UserHistory
    {
        public int UserID { get; set; }
        public int ItemType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class UserHistoryGridData
    {
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public string Title { get; set; }
        public string LastAccess { get; set; }
        public int Progress { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Type { get; set; }
        public string tTime { get; set; }
        public int FavNum { get; set; }
        public string Submit { get; set; }
        public int EventID { get; set; }
        public int MinPassGrade { get; set; }
        public int DisplayAnswers { get; set; }
        public string ProficiencyLevel { get; set; }
    }

    public class UserHistoryGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<UserHistoryGridData> UserHistoryGridData { get; set; }
    }

    public class MyLearning
    {
        public int UserID { get; set; }
        public int ItemType { get; set; }
        public string SearchText { get; set; }
        public int CategoryID { get; set; }
        public int Status { get; set; }     // 1 - Not yet started/taken, 2 - Inprogress, 3 - Completed
        public int Favorites { get; set; }  // 0 - Not Favorite, 1 - Favorite
    }

    public class MyLearningGridData
    {
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public int EventID { get; set; }
        public string Title { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Progress { get; set; }
        public string Type { get; set; }
        public string tTime { get; set; }
        public int LessonsCompleted { get; set; }
        public int LessonsTotal { get; set; }
        public int Favorite { get; set; }
        public int MinPassGrade { get; set; }
        public string Result { get; set; }
        public string InitialGraphic { get; set; }
    }

    public class MyLearningGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<MyLearningGridData> MyLearningGridData { get; set; }
    }

    public class AddRemoveFavorite
    {
        public int UserID { get; set; }
        public int ItemID { get; set; }
        public int Type { get; set; }
    }

     public class MyLearningPathGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<LearningPathDetailedList> LearningPathDetailedList { get; set; }
    }

    public class DownloadCertificate
    {
        public int EventID { get; set; }
        public int ItemID { get; set; }
        public int IDType { get; set; }
        public int UserID { get; set; }
        public int Mode { get; set; }
    }

    public class DownloadCertificateInfo
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string CompletedDate { get; set; }
        public string TemplateFileName { get; set; }
        public string Prefix { get; set; }
        public string LogoFile { get; set; }
        public string SignatureFile1 { get; set; }
        public string SignatureFile2 { get; set; }
        public string OrganizationName { get; set; }
    }
}
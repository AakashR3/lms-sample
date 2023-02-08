namespace Tata.IGetIT.Learner.Repository.Models
{
    public class QuickStartGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<QuickStartGridData> QuickStartGridData { get; set; }
    }

    public class QuickStartGridData
    {
        public int LiveID { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public int Views { get; set; }
        public int CatID { get; set; }
        public int SubCatID { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class QuickStartGrid
    {
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public string SearchText { get; set; }
        public int Type { get; set; }
    }

    public class Categories
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class SubCategories
    {
        public int SubCategoryID { get; set; }
        public string Name { get; set; }
        public string CateName { get; set; }
        public string Version { get; set; }
    }

    public class QuickStartNotification
    {
        public int UserID { get; set; }
        public string Flag { get; set; }
        public int NewQuickstartsReleaseFlag { get; set; }
    }
}
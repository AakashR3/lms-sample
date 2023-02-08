using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Helpers;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public PaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize, int totalRecords)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            //this.PageSize = pageSize > 10 ? 10 : pageSize;
            this.PageSize = pageSize > 0 ? pageSize : totalRecords;
            var totalNoPages = ((double)totalRecords / (double)this.PageSize);
            this.TotalPages = Convert.ToInt32(Math.Ceiling(totalNoPages));
        }
    }
    public class SubsriptionPaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int CourseTotalPages { get; set; }
        public int AssessmentTotalPages { get; set; }
        public SubsriptionPaginationFilter()
        {
            this.PageNumber = 1;
            this.PageSize = 10;
        }
        public SubsriptionPaginationFilter(int pageNumber, int pageSize, int catalogTotalPages, int assessmentTotalPages)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 0 ? pageSize : catalogTotalPages;
            var totalNoPages = ((double)catalogTotalPages / (double)pageSize);
            this.CourseTotalPages = Convert.ToInt32(Math.Ceiling(totalNoPages));

            this.PageSize = pageSize > 0 ? pageSize : assessmentTotalPages;
            var assessTotalPages = ((double)assessmentTotalPages / (double)pageSize);
            this.AssessmentTotalPages = Convert.ToInt32(Math.Ceiling(assessTotalPages));
        }
    }

    public class CategoryDetails
    {
        public IEnumerable<GetMasterCategories> getMasterCategories { get; set; }
        public IEnumerable<GetCategories> getCategories { get; set; }
    }

    public class GetMasterCategories
    {
        public int ID { get; set; }
        public string MasterCategoryName { get; set; }
    }
    public class CourseTitles
    {
        public int CourseID { get; set; }
        public string Title { get; set; }
    }
    public class GetCategories
    {
        public int ID { get; set; }
        public string MasterCategoryName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        //public int Sequence { get; set; }
        //public bool Active { get; set; }
        //public byte[] CategoryImage { get; set; }
        //public bool IsPublic { get; set; }
        public string CategoryImageFileName { get; set; }

    }
    public class GetSubCategoriesBasedOnCategoryID
    {
        public int SubCategoryID { get; set; }
        public string Name { get; set; }
        public string Version { get; set; }
        //public int Live { get; set; }
        public string ShortTerm { get; set; }
        public int CategoryID { get; set; }
        //public bool ispublic { get; set; }
        //public byte[] Image { get; set; }
        //public string SubCategoryImageFileName { get; set; }

    }
    public class GetTopicsBasedOnCategoryID
    {
        public int ID { get; set; }
        public string Name { get; set; }
        //public string Overview { get; set; }
        //public string Prerequisites { get; set; }
        //public string IntendedAudience { get; set; }
        //public string CourseVersion { get; set; }
        //public byte[] Image { get; set; }
        public int CategoryID { get; set; }
        //public string CategoryName { get; set; }

    }

    public class Plans
    {
        public int ID { get; set; }
        public int PlanName { get; set; }
        public string PlanDescription { get; set; }
        public float Amount { get; set; }
        public int IsVisible { get; set; }
        public int SubscriptionID { get; set; }
        public int RecurlyPaidTrial { get; set; }

    }

    public class Common_EmailInfo : IValidatableObject
    {
        public string[] To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (Subject.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.EmptyEmailSubject));
            }
            if (Content.IsNullOrEmptyOrWhiteSpace())
            {
                validationErrors.Add(new ValidationResult(LearnerAppConstants.EmptyEmailContent));
            }

            return validationErrors;
        }
    }
    public class EmailConfiguration
    {
        public string From { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class IndividualPlans
    {
        public int SID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CountryCode { get; set; }
        public string Price { get; set; }
        public string URLName { get; set; }
        public string ImageLocation { get; set; }
        public int Category { get; set; }
        public int AssessmentCount { get; set; }
        public int CourseCount { get; set; }

    }
}
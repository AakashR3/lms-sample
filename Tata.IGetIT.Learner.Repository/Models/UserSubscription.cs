using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class UserSubscription
    {
        public string SubscriptionID { get; set; }
        public string SubscriptionName { get; set; }
    }

    public class UsersPurchasedHistoryParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<UsersPurchasedHistory> usersPurchasedHistories { get; set; }
    }
    public class UsersPurchasedHistory
    {
        public int SNo { get; set; }
        public int FulfillmentID { get; set; }
        public int SubscriptionID { get; set; }
        public string SubscriptionName { get; set; }
        public string PurchaseDate { get; set; }
        public string ExpireDate { get; set; }
        public string SalesOrderDetailID { get; set; }
        public string Image { get; set; }
        public string InvoiceType { get; set; }

    }
    public class ProfessionalBundle
    {
        public int SID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string CountryCode { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public bool isPrivate { get; set; }
        public int seq { get; set; }
        public int CategoryID { get; set; }
        public string ProductType { get; set; }
        public string ShortName { get; set; }
        public bool Highlight { get; set; }
        public int Duration { get; set; }
    }
    public class AvailableSubscription
    {
        public int SID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Price { get; set; }
        public string URLName { get; set; }
        public string ImageLocation { get; set; }
        public int Category { get; set; }
        public int CourseCount { get; set; }
        public int AssessmentCount { get; set; }
    }


    public class SkillAdvisor_Subscription
    {
        public int SID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public bool isPrivate { get; set; }
        public int seq { get; set; }
        public int CategoryID { get; set; }
        public string SubscriptionStatus { get; set; }
    }
    public class SubscriptionDetail
    {
        public SkillAdvisor_Subscription skillAdvisor_Subscription { get; set; }
        public IEnumerable<SkillAdvisor_Courses> skillAdvisor_Courses { get; set; }
        public IEnumerable<SkillAdvisor_Assessments> SkillAdvisor_Assessments { get; set; }

    }


    public class SubscriptionDetailParent
    {

        public int CourseTotalItems { get; set; }
        public int CourseTotalPages { get; set; }
        public int AssessmentTotalItems { get; set; }
        public int AssessmentTotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public SkillAdvisor_Subscription skillAdvisor_Subscription { get; set; }
        public IEnumerable<SkillAdvisor_Courses> skillAdvisor_Courses { get; set; }
        public IEnumerable<SkillAdvisor_Assessments> SkillAdvisor_Assessments { get; set; }

    }
}

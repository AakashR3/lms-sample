using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class TechTips
    {
        public string ArticleID { get; set; }
        public string Title { get; set; }
        public string SubCategory { get; set; }
        public string ModifiedDate { get; set; }
        public string UserName { get; set; }
        public string SubmittedDate { get; set; }
        public int Status { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public int CategoryID { get; set; }
        public int SubCategoryID { get; set; }
        public int TopicID { get; set; }
        public int UserTypeID { get; set; }
    }
    public class TechTipsGridDataParent
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TechTips> TechTipsData { get; set; }
    }
    public class Topics
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; }
    }
    public class TopicsByCategory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string Prerequisites { get; set; }
        public string IntendedAudience { get; set; }
        public string CourseVersion { get; set; }
        public string Image { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
                    

}

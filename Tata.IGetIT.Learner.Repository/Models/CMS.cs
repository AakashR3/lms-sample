using System.ComponentModel.DataAnnotations;
using Tata.IGetIT.Learner.Repository.Constants;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class V2_CMSFormType
    { 
        public int ID { get; set; }
        public string Name { get; set; }
    }
    public class V2_CMSFormDataInsert
    {

        public int UserID { get; set; }
        public int FormId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string email { get; set; }
        public string Phone { get; set; }
        public string Job { get; set; }
        public string Message { get; set; }
        public string Option { get; set; }

    }
}
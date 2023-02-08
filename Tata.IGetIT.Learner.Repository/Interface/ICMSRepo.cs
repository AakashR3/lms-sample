using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface ICMSRepo
    {
        public Task<IEnumerable<V2_CMSFormType>> GetCMSFormTypes();
        public Task<bool> InsertCMSFormData(V2_CMSFormDataInsert v2_CMSFormDataInsert);

    }
}

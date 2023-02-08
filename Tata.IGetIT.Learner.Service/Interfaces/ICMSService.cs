using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ICMSService
    {
        public Task<IEnumerable<V2_CMSFormType>> GetCMSFormTypes();
        public Task<bool> InsertCMSFormData(V2_CMSFormDataInsert v2_CMSFormDataInsert);
    }
}

using Newtonsoft.Json;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class Element
    {
        [JsonProperty("handle~")]
        public Handle Handle { get; set; }
        public string handle { get; set; }
    }
    public class Handle
    {
        public string emailAddress { get; set; }
    }

    public class LinkedinElement
    {
        public List<Element> elements { get; set; }
    }

    public class LinkedinAccessToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
    }
}
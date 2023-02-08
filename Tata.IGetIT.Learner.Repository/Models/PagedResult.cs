using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tata.IGetIT.Learner.Repository.Models
{
    public class PagedResult<T>
    {
        public string Message { get; set; }
        public int TotalRecords { get; set; }
        public T Data { get; set; }
    }
}

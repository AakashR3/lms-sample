using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Repository.Interface
{
    public interface IAuthRepo
    {

        public Task<int> CheckUser(string username);
       public Task<int> RegisterUser(SocialRegisteration register);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Models;

namespace Tata.IGetIT.Learner.Service.Interfaces
{
    public interface ICommunicationService
    {

        /// <summary>
        /// Send forgot password email using template
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public Task<bool> SendForgotPasswordEmailAsync(ResetPassword content);
        public Task<bool> SendOTPEmailAsync(RegistrationOTP registrationOTP);
    }
}

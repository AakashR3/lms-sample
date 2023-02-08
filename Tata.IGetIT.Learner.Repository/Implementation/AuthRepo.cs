using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tata.IGetIT.Learner.Repository.Constants;
using Tata.IGetIT.Learner.Repository.Core;
using Tata.IGetIT.Learner.Repository.Interface;
using Tata.IGetIT.Learner.Repository.Models;
using Tata.IGetIT.Learner.Repository.Models.Database.DTO;

namespace Tata.IGetIT.Learner.Repository.Implementation
{
    public class AuthRepo : IAuthRepo
    {

        private readonly IDatabaseManager _databaseOperations;
        public AuthRepo(IDatabaseManager databaseOperations)
        {
            _databaseOperations = databaseOperations;
        }

        public Task<int> CheckUser(string username)
        {
            return Task.FromResult(0);
        }

        public async Task<int> RegisterUser(SocialRegisteration register)
        {

            var param = new Dictionary<string, object>();
            param.Add("@AccountName", register.FirstName ?? "" + "_" + register.LastName ?? "");
            param.Add("@Email", register.Email);
            param.Add("@UserName", register.Email);
            param.Add("@Password", register.Password);
            param.Add("@FirstName", register.FirstName);
            param.Add("@LastName", register.LastName);
            param.Add("@PreferredSoftwareID", register.PreferredSoftwareID);
            param.Add("@FavouriteSoftware", register.FavouriteSoftware);
            param.Add("@AccountTypeID", register.AccountTypeID);
            param.Add("@ManagerID", register.ManagerID);
            param.Add("@CompanyName", register.CompanyName);
            param.Add("@Country", register.Country);
            param.Add("@Platform", register.Platform);
            param.Add("@SocialType", register.SocialType);
            param.Add("@SocialID", register.SocialID);
            param.Add("@EmailPref", register.EmailPref);
            param.Add("@MarketingEmail", register.MarketingEmail);

            QueryInfo queryInfo = new QueryInfo()
            {
                queryType = QueryType.DirectText,
                QueryText = "select * from UserTable where uId=@uid",
                Parameters = param
            };


            return await _databaseOperations.ExecuteNonQueryAsync(queryInfo);
        }


    }
}

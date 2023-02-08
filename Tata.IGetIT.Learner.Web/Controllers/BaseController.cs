﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;


namespace Tata.IGetIT.Web.Learner.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Non action method to populate model errors in request body
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public async Task<IActionResult> PopulateModelErrorsAsync()
        {
            var values = ModelState.Values.ToList();
            var keys = ModelState.Keys.ToList();
            var errorCol = new List<string>();

            for (int i = 0; i < values.Count; i++)
            {
                var key = keys[i];
                var value = values[i];
                var messages = value.Errors.Select(_ => $"{(key.IsNullOrEmptyOrWhiteSpace() ? "" : key + ":")}{(_.Exception != null ? _.Exception.Message : _.ErrorMessage)}");
                errorCol.AddRange(messages);
            }

            var response = new ErrorResponse(errorCol);

            return StatusCode((int)HttpStatusCode.BadRequest, response);
        }
        protected bool IsUserAuthenticated()
        {
            return (bool)HttpContext.Items.FirstOrDefault(x => (string)x.Key == "IsUserAuthenticated").Value;
        }
        protected int GetUserId()
        {
            if (IsUserAuthenticated())
            {
                return Int32.Parse((string)HttpContext.Items.FirstOrDefault(x => (string)x.Key == "UserId").Value);
            }
            return 0;
        }

        protected int GetAccountId()
        {
            if (IsUserAuthenticated())
            {
                return Int32.Parse((string)HttpContext.Items.FirstOrDefault(x => (string)x.Key == "AccountId").Value);
            }
            return 0;
        }

        protected string GetSessionId()
        {
            if (IsUserAuthenticated())
            {
                return (string)HttpContext.Items.FirstOrDefault(x => (string)x.Key == "SessionId").Value;
            }
            return null;         
        }

        protected string GetCurrency()
        {
            if (IsUserAuthenticated())
            {
                return (string)HttpContext.Items.FirstOrDefault(x => (string)x.Key == "Currency").Value;
            }
            return null;
        }

    }
}

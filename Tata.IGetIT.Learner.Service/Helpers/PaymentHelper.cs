using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace Tata.IGetIT.Learner.Service.Helpers
{
    public static class PaymentHelper
    {

        #region RazorPay       
        public static async Task<List<RazorPlanObject>> FetchAllPlans(string apiKey, string apiSecret, string apiUrl)
        {

            var responseString = await UtilityHelper.httpGetAsync(apiUrl, new List<KeyValuePair<string, string>>(), apiKey, apiSecret);
            JObject jsonLinq = JObject.Parse(responseString);
            var srcArray = jsonLinq.Descendants().Where(d => d is JArray).First();
            List<RazorPlanObject> objCurrency = new List<RazorPlanObject>();
            for (int cnt = 0; cnt < srcArray.Count(); cnt++)
            {
                JObject row = JObject.Parse(srcArray[cnt].ToString());
                var cleanRow = new JObject();
                string id = "";
                foreach (JProperty column in row.Properties())
                {
                    if (column.Value is JValue)
                    {
                        //cleanRow.Add(column.Name, column.Value);
                        if (column.Name == "id")
                        {
                            id = column.Value.ToString();
                        }
                    }
                    else if (column.Value.ToString() != "[]" && column.Value.ToString().Contains("undefined") == false)
                    {
                        JObject jsonLinq1 = JObject.Parse(column.Value.ToString());

                        double amount = 0;
                        string description = "";
                        string pname = "";
                        foreach (var x in jsonLinq1)
                        {
                            var key = ((x)).Key;
                            var Value = ((x)).Value;

                            if (key == "amount")
                            {
                                amount = ((x)).Value.ToObject<double>();
                            }
                            if (key == "description")
                            {
                                description = ((x)).Value.ToString();
                            }
                            if (key == "name")
                            {
                                pname = ((x)).Value.ToString();
                            }
                        }
                        objCurrency.Add(new RazorPlanObject { ID = id, Amount = amount, Description = description, PlanCode = pname });
                        id = "";
                        amount = 0;
                        description = "";
                        pname = "";
                    }
                }
            }
            return objCurrency;
        }

        public static RazorPlanObject GetPlanInfo(string PlanId, List<RazorPlanObject> planList)
        {
            RazorPlanObject purchasedPlan = new RazorPlanObject();
            foreach (RazorPlanObject plan in planList)
            {
                if (plan.ID == PlanId)
                {
                    purchasedPlan.Amount = plan.Amount;
                    purchasedPlan.Description = plan.Description;
                    purchasedPlan.PlanCode = plan.PlanCode;
                }
            }
            return purchasedPlan;
        }
        public static RazorPlanObject GetPlanID(string PlanCode, List<RazorPlanObject> planList)
        {
            RazorPlanObject purchasedPlan = new RazorPlanObject();
            foreach (RazorPlanObject plan in planList)
            {
                if (plan.PlanCode == PlanCode)
                {
                    purchasedPlan.ID = plan.ID;
                }
            }
            return purchasedPlan;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }
        public static Int32 DateTimeToUnixTimeStampUTC(DateTime date)
        {
            Int32 unixTimeStamp;
            DateTime currentTime = date;
            DateTime zuluTime = currentTime.ToUniversalTime();
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            unixTimeStamp = (Int32)(zuluTime.Subtract(unixEpoch)).TotalSeconds;
            return unixTimeStamp;
        }

        public static async Task<PlanDetailsModel> GetPlanDetails(string strPlanID, string apiKey, string apiSecret, string apiUrl)
        {
            PlanDetailsModel objPlanDetails = new PlanDetailsModel();
            var responseString = await UtilityHelper.httpGetAsync(apiUrl + strPlanID, new List<KeyValuePair<string, string>>(), apiKey, apiSecret);
            var data = (JObject)JsonConvert.DeserializeObject(responseString);
            objPlanDetails.PlanInterval = Convert.ToInt32(data["interval"].Value<string>());
            objPlanDetails.PlanDuration = data["period"].Value<string>();
            return objPlanDetails;
        }


        public static async Task<bool> RazorpayCancelSubscription(string subscriptionId, string rzp_apiKey, string rzp_apiSecret, string rzp_apiCancelSubscription)
        {
            bool blnFlag = false;
            try
            {
                string url = String.Format(rzp_apiCancelSubscription, subscriptionId);
                var input = new
                {
                    cancel_at_cycle_end = 0
                };

                var json = JsonConvert.SerializeObject(input);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                using var client = new HttpClient();
                var authenticationString = $"{rzp_apiKey}:{rzp_apiSecret}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.UTF8.GetBytes(authenticationString));
                client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
                var response = await client.PostAsync(url, data);


                string responseString = response.Content.ReadAsStringAsync().Result;
                XDocument xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(responseString), new XmlDictionaryReaderQuotas()));
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(xml.ToString());
                string notificationName = xmlDocument.DocumentElement.Name;
                string status = xmlDocument.SelectSingleNode(notificationName + "/status").InnerText;
                if (status != null && status != "" && status.ToUpper() == "CANCELLED")
                {
                    blnFlag = true;
                }
                else
                {
                    blnFlag = false;
                }
                return blnFlag;
            }
            catch
            {
                blnFlag = false;
            }
            return blnFlag;
        }


        public static async Task<string> GetRazorpayPlanDetails(string strUniqPlanID, string rzp_apiKey, string rzp_apiSecret, string rzp_apiPlanDetails)
        {
            string strPlanID = "";
            string url = rzp_apiPlanDetails + strUniqPlanID;
            List<KeyValuePair<string, string>> header = new()
            {
              new KeyValuePair<string, string>("ContentType", "application/json"),
              new KeyValuePair<string, string>("Accept", "application/json")
            };
            var responseString = await UtilityHelper.httpGetAsync(url, header, rzp_apiKey, rzp_apiSecret);

            XDocument xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(responseString), new XmlDictionaryReaderQuotas()));
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml.ToString());
            string notificationName = xmlDocument.DocumentElement.Name;
            strPlanID = xmlDocument.SelectSingleNode(notificationName + "/item" + "/name").InnerText;
            return strPlanID;
        }


        public static async Task<string> GetRazorPayInvoiceID(string PaymentID,string rzp_apiKey, string rzp_apiSecret, string rzp_api)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var authenticationBytes = Encoding.ASCII.GetBytes($"{rzp_apiKey}:{rzp_apiSecret}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"{rzp_api}{PaymentID}";
                    var IssueInvoiceResponse = await client.GetAsync(url);
                    var response_string = await IssueInvoiceResponse.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(response_string);
                    return data.invoice_id;
                }
            }
            catch
            {
                return "";
            }
        }

        public static async Task<float> GetRazorPayPurchasedAmount(string PaymentID, string rzp_apiKey, string rzp_apiSecret, string rzp_api)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    var authenticationBytes = Encoding.ASCII.GetBytes($"{rzp_apiKey}:{rzp_apiSecret}");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = $"{rzp_api}{PaymentID}";
                    var IssueInvoiceResponse = await client.GetAsync(url);
                    var response_string = await IssueInvoiceResponse.Content.ReadAsStringAsync();
                    dynamic data = JObject.Parse(response_string);
                    return data.amount;
                }
            }
            catch
            {
                return 0;
            }
        }






        #endregion


        #region Recurly Payment

        public static async Task<CouponObject> GetActiveCoupon(int accountId, string apiKey, string recurlyDomain, string apiUrl)
        {
            CouponObject info = new CouponObject();
            try
            {
                string url = string.Format(apiUrl, recurlyDomain, accountId);
                List<KeyValuePair<string, string>> header = new()
                {
                new KeyValuePair<string, string>("ContentType", "application/xml; charset=utf-8"),
                new KeyValuePair<string, string>("Accept", "application/xml")
                };
                var response = await UtilityHelper.httpGetAsyncResponse(url, header, apiKey, "");

                if (response.StatusCode == 200)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(response.ResponseString);
                    info.Amount = Convert.ToDouble(xmlDocument.SelectSingleNode("redemption/total_discounted_in_cents").InnerText) / 100;
                    info.Date = xmlDocument.SelectSingleNode("redemption/created_at").InnerText.Substring(0, xmlDocument.SelectSingleNode("redemption/created_at").InnerText.IndexOf("T"));
                    info.Currency = xmlDocument.SelectSingleNode("redemption/currency").InnerText;
                }
            }
            catch { }

            return info;
        }
        public static string GenerateRecurlyJSSignature(string planCode, string accountCode, string currency, string apiKey)
        {
            //Random nonce
            Random rnd = new Random();
            int ranInt = rnd.Next(10000, 99999);
            string non = "nonce=" + ranInt.ToString();

            //Subscription
            string sub = "subscription[plan_code]=" + planCode;

            //Timestamp
            TimeSpan span = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            string tim = "timestamp=" + ((int)span.TotalSeconds).ToString();

            //Account
            string acc = "account[account_code]=" + accountCode;

            //Currency
            string curr = "subscription[currency]=" + currency;

            //Combine protected string
            string pro = non + "&" + acc + "&" + sub + "&" + curr + "&" + tim;
            pro = HttpUtility.UrlEncode(pro).Replace("%3d", "=").Replace("%26", "&");

            //Generate byte hash
            string priv = apiKey;
            byte[] key = System.Text.Encoding.UTF8.GetBytes(priv);
            System.Security.Cryptography.HMACSHA1 myHMAC = new System.Security.Cryptography.HMACSHA1(key);
            byte[] hashData = myHMAC.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pro));

            //Generate hex hash
            string hashText = "";
            string hexValue = "";
            foreach (byte b in hashData)
            {
                hexValue = b.ToString("X").ToLower();
                hashText += (hexValue.Length == 1 ? "0" : "") + hexValue;
            }
            //Combine for signature
            return hashText + "|" + pro;
        }


        public static async Task<BillingObject> GetBillingInformation(int accountId, string apiKey, string recurlyDomain, string apiUrl)
        {
            BillingObject info = new BillingObject();
            try
            {

                string url = string.Format(apiUrl, recurlyDomain, accountId.ToString());
                List<KeyValuePair<string, string>> header = new()
                {
                new KeyValuePair<string, string>("ContentType", "application/xml; charset=utf-8"),
                new KeyValuePair<string, string>("Accept", "application/xml")
                };
                var response = await UtilityHelper.httpGetAsyncResponse(url, header, apiKey, "");


                if (response.StatusCode == 200)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(response.ToString());
                    info.FirstName = xmlDocument.SelectSingleNode("billing_info/first_name").InnerText;
                    info.LastName = xmlDocument.SelectSingleNode("billing_info/last_name").InnerText;
                    info.Address1 = xmlDocument.SelectSingleNode("billing_info/address1").InnerText;
                    info.Address2 = xmlDocument.SelectSingleNode("billing_info/address2").InnerText;
                    info.City = xmlDocument.SelectSingleNode("billing_info/city").InnerText;
                    info.State = xmlDocument.SelectSingleNode("billing_info/state").InnerText;
                    info.Country = xmlDocument.SelectSingleNode("billing_info/country").InnerText;
                    info.PostalCode = xmlDocument.SelectSingleNode("billing_info/zip").InnerText;
                    info.LastFour = "XXXXXXXXXXXX" + xmlDocument.SelectSingleNode("billing_info/last_four").InnerText;
                }
            }
            catch { }

            return info;
        }

        public static async Task<string> GetRecurlyLoginToken(int accountId, string apiKey, string recurlyDomain, string apiUrl)
        {
            string url = string.Format(apiUrl, recurlyDomain, accountId);
            List<KeyValuePair<string, string>> Properties = new()
            {
              new KeyValuePair<string, string>("ContentType", "application/xml; charset=utf-8"),
              new KeyValuePair<string, string>("Accept", "application/xml")
            };
            var responseString = await UtilityHelper.httpGetAsync(url, Properties, apiKey, "");

            try
            {
                //Parse response                
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseString);
                string strLoginToken = xmlDocument.SelectSingleNode("account/hosted_login_token").InnerText;
                return strLoginToken;
            }
            catch
            {
                return "";
            }
        }



        public static async Task<RenewalObject> VerifyRenewal(int accountId,string uuid, string apiKey, string recurlyDomain, string apiUrl)
        {
            RenewalObject info = new RenewalObject();
            try
            {
                string url = string.Format("https://{0}.recurly.com/v2/subscriptions/{1}", recurlyDomain, uuid);
                //string url = string.Format(apiUrl, recurlyDomain, accountId);
                List <KeyValuePair<string, string>> header = new()
                {
                new KeyValuePair<string, string>("ContentType", "application/xml; charset=utf-8"),
                new KeyValuePair<string, string>("Accept", "application/xml")
                };
                var response = await UtilityHelper.httpGetAsyncResponse(url, header, apiKey, "");

                if (response.StatusCode == 200)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(response.ResponseString);                  
                    info.ExpiryDate = xmlDocument.SelectSingleNode("subscription/current_period_ends_at").InnerText;
                    info.PlanCode = Convert.ToInt32(xmlDocument.SelectSingleNode("subscription/plan/plan_code").InnerText);
                    //Generate cost
                    info.Quantity = Convert.ToInt32(xmlDocument.SelectSingleNode("subscription/quantity").InnerText);
                    info.CentCost = xmlDocument.SelectSingleNode("subscription/unit_amount_in_cents").InnerText;
                    info.DollarCost = (Convert.ToInt32(info.Quantity) * Convert.ToInt32(info.CentCost)) / 100;
                }
            }
            catch { }

            return info;
        }


        public static async Task<TransactionObject> GetTransactionInfo(int accountId, string uuid, string apiKey, string recurlyDomain, string apiUrl)
        {
            TransactionObject obj = new TransactionObject();
            try
            {
                string url = string.Format("https://{0}.recurly.com/v2/accounts/{1}/transactions", recurlyDomain, accountId);
                //string url = string.Format(apiUrl, recurlyDomain, accountId);
                List<KeyValuePair<string, string>> header = new()
                {
                new KeyValuePair<string, string>("ContentType", "application/xml; charset=utf-8"),
                new KeyValuePair<string, string>("Accept", "application/xml")
                };
                var response = await UtilityHelper.httpGetAsyncResponse(url, header, apiKey, "");

                if (response.StatusCode == 200)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(response.ResponseString);
                    string transID = xmlDocument.SelectSingleNode("transactions/transaction/uuid").InnerText;
                    double transCost = Convert.ToDouble(xmlDocument.SelectSingleNode("transactions/transaction/amount_in_cents").InnerText);
                    string currCode = xmlDocument.SelectSingleNode("transactions/transaction/currency").InnerText;
                    string userEmail = xmlDocument.SelectSingleNode("transactions/transaction/details/account/email").InnerText;
                    transCost = transCost / 100;

                    obj.Cost = transCost;
                    obj.CurrencyCode = currCode;
                    obj.UUID = transID;
                    obj.Email = userEmail;
                }
                else
                {
                    obj.Cost = 0.00;
                    obj.CurrencyCode = "USD";
                    obj.UUID = "0";
                    obj.Email = "";
                }
            }
            catch { }

            return obj;
        }

        #endregion


    }
}

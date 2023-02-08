using Newtonsoft.Json;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Tata.IGetIT.Learner.Service.Helpers
{
    public static class UtilityHelper
    {
        public static async Task<string> httpPostAsync(string url, List<KeyValuePair<string, string>> reqParam)
        {
            HttpClient client = new HttpClient();
            var stringContent = new FormUrlEncodedContent(reqParam);
            var response = await client.PostAsync(url, stringContent);

            return await response.Content.ReadAsStringAsync();
        }

        

        public static async Task<string> httpGetAsync(string url, List<KeyValuePair<string, string>> reqParam)
        {
            HttpClient client = new HttpClient();
            foreach (var kvp in reqParam)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(kvp.Key, kvp.Value);
            }

            var response = await client.GetAsync(url);

            return await response.Content.ReadAsStringAsync();
        }

      

        public static async Task<string> httpGetAsync(string url, List<KeyValuePair<string, string>> reqHeaders, string apiKey, string apiSecret)
        {
            HttpClient client = new HttpClient();

            foreach (var kvp in reqHeaders)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(kvp.Key, kvp.Value);
            }

            var authString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{apiKey}:{apiSecret}"));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);

            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }


        public static async Task<APIResponse> httpGetAsyncResponse(string url, List<KeyValuePair<string, string>> reqHeaders, string apiKey, string apiSecret)
        {
            APIResponse result = new();
            HttpClient client = new HttpClient();

            foreach (var kvp in reqHeaders)
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation(kvp.Key, kvp.Value);
            }

            var authString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{apiKey}:{apiSecret}"));

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authString);

            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            HttpStatusCode httpStatus = response.StatusCode;
            result.StatusCode = (int)httpStatus;
            result.ResponseString = await response.Content.ReadAsStringAsync();

            return result;

        }


        public async static Task<string> GetGeoLocation(string Publid_IPAddress, string locationApiUrl, string azureMapKey, string publicIPApiUrl)
        {
            string countryCode = string.Empty;
            try
            {
                var httpClient = new HttpClient();
                //var publicIP = await httpClient.GetStringAsync(publicIPApiUrl);
                var publicIP = Publid_IPAddress;
                string apiUrl = string.Format(locationApiUrl, azureMapKey, publicIP);

                var responseString = await httpGetAsync(apiUrl, new List<KeyValuePair<string, string>>());
                GeoLocation myDeserializedClass = JsonConvert.DeserializeObject<GeoLocation>(responseString);
                countryCode = myDeserializedClass.countryRegion.isoCode;
            }
            catch
            {
                return "IN";
            }
            return countryCode;
        }
        public async static Task<string> GetGeoIPAddress(string Publid_IPAddress, string locationApiUrl, string azureMapKey, string publicIPApiUrl)
        {
            string countryCode = string.Empty;
            try
            {
                var httpClient = new HttpClient();
                var publicIP = Publid_IPAddress;
                string apiUrl = string.Format(locationApiUrl, azureMapKey, publicIP);
                var responseString = await httpGetAsync(apiUrl, new List<KeyValuePair<string, string>>());
                GeoLocation myDeserializedClass = JsonConvert.DeserializeObject<GeoLocation>(responseString);
                countryCode = myDeserializedClass.countryRegion.isoCode;
                return myDeserializedClass.ipAddress;
            }
            catch
            {
                return "IN";
            }
            return countryCode;
        }


        private readonly static string key = "TATATechnologiesLtdiGETIT2022AES";

        public static string Encrypt(string inputtext)
        {
            byte[] ivector = new byte[16];
            byte[] arraybuffer;
            var encodedBase64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(inputtext));
            string encryptedText = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = ivector;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream((Stream)ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter((Stream)cs))
                        {
                            //sw.Write(inputtext);
                            sw.Write(encodedBase64String);
                        }
                        arraybuffer = ms.ToArray();

                    }
                }
            }

            return new String(Convert.ToBase64String(arraybuffer));
        }

        public static string Decrypt(string inputtext)
        {
            byte[] ivector = new byte[16];
            byte[] arraybuffer = Convert.FromBase64String(inputtext);
            string decryptedText = string.Empty;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = ivector;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream(arraybuffer))
                {
                    using (CryptoStream cs = new CryptoStream((Stream)ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            decryptedText = sr.ReadToEnd();
                        }
                    }
                }
            }

            return new String(Encoding.UTF8.GetString(Convert.FromBase64String(decryptedText)));
        }


        public static string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);

            //return string.Concat(Path.GetFileNameWithoutExtension(fileName), "_", Guid.NewGuid().ToString().AsSpan(0, 4),
            //Path.GetExtension(fileName));

            //return string.Concat(Path.GetFileNameWithoutExtension(fileName).Replace("/", "").Replace(" ", "").ToString(),
            //    "_", Guid.NewGuid().ToString(),
            //Path.GetExtension(fileName));

            //return string.Concat(Path.GetFileNameWithoutExtension(fileName), "_", Guid.NewGuid().ToString(),Path.GetExtension(fileName));

            return string.Concat(Guid.NewGuid().ToString(),Path.GetExtension(fileName));
        }

        public static string ConvertToBase64(this Stream stream)
        {
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

    }
}

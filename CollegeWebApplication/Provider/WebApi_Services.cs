using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Models;

namespace CollegeWebApplication.Provider
{
    public class WebApi_Services
    {
        public static string CommonUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiURL"];

        private HttpResponseMessage GetWebApiResponse(string apiUrl, Dictionary<string, ArrayList> arrayList = null, string type = "GET")
        {
            string token = "";
            var request = HttpContext.Current.Request;
            HttpCookie reqCookie = request.Cookies["POS"];
            if (reqCookie != null)
            {
                token = reqCookie["Token"];
            }
            //token = "BED65H-HJUY87-JHY789";
            apiUrl = CommonUrl + apiUrl;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = null;
            if (type == "GET")
                response = client.GetAsync(apiUrl).Result;
            else if (type == "POST")
                response = client.PostAsync(apiUrl, new StringContent(JsonConvert.SerializeObject(arrayList), Encoding.UTF8, "application/json")).Result;
            return response;
        }

        public APIResponse GetResponse(string apiURL, Dictionary<string, ArrayList> arrayList = null, string Type = "GET")
        {
            HttpResponseMessage responseMessage = GetWebApiResponse(apiURL, arrayList, Type);
            APIResponse ParameterList = new APIResponse();
            var data = responseMessage.Content.ReadAsStringAsync().Result;
            ParameterList = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponse>(data);
            return ParameterList;
        }

        public string GetToken(string url, string username, string password)
        {
            url = CommonUrl;
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string> ("grant_type","password"),
                new KeyValuePair<string, string>("username",username),
                new KeyValuePair<string, string>("Password",password)
            };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslpolicyErrors) => true;
            using (var client = new HttpClient())
            {
                var response = client.PostAsync(url + "/GetToken", content).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }


        //For pos

        private HttpResponseMessage GetPOSWebApiResponse(string apiurl, ArrayList arrayList = null, string type = "GET", string apitype = "")
        {
            string token = "";
            var request = HttpContext.Current.Request;
            HttpCookie reqCookie = request.Cookies["POS"];
            if (reqCookie != null)
            {
                token = reqCookie["Token"];
            }
            if (apitype != "")
                CommonUrl = System.Configuration.ConfigurationManager.AppSettings["WebApiURLPOS"];

            apiurl = CommonUrl + apiurl;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiurl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = null;
            if (type == "GET")
                response = client.GetAsync(apiurl).Result;
            else if (type == "POST")
                response = client.PostAsync(apiurl, new StringContent(JsonConvert.SerializeObject(arrayList), Encoding.UTF8, "application/json")).Result;
            return response;
        }


        //public APIPOSResponse GetPOSResponse(string apiURL, ArrayList arrayList = null, string Type = "GET", string apitype = "")
        //{
        //    HttpResponseMessage responseMessage = GetPOSWebApiResponse(apiURL, arrayList, Type, apitype);
        //    APIPOSResponse ParameterList = new APIPOSResponse();
        //    var data = responseMessage.Content.ReadAsStringAsync().Result;
        //    ParameterList = Newtonsoft.Json.JsonConvert.DeserializeObject<APIPOSResponse>(data);
        //    return ParameterList;
        //}

        public APIResponse GetTRNResponse(string apiURL, ArrayList arrayList = null, string Type = "GET", string apitype = "")
        {
            HttpResponseMessage responseMessage = GetPOSWebApiResponse(apiURL, arrayList, Type, apitype);
            APIResponse ParameterList = new APIResponse();
            var data = responseMessage.Content.ReadAsStringAsync().Result;
            ParameterList = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponse>(data);
            return ParameterList;
        }


    }
}
using ChatCode.Bot.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatCode.Bot.Services
{
    public class DataClient
    {
        private string ApiUrl = "http://chatcodeapp.azurewebsites.net/";
        private HttpClient client = new HttpClient();


        public DataClient()
        {
            client.BaseAddress = new Uri(ApiUrl);
        }


        public async Task<ResultModel> SendInformation(CreateAboutMeModel model)
        {
            HttpResponseMessage req = await client.PostAsync("api/users/add", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultModel>(data);
            }

            else
                return new ResultModel()
                {
                    IsSuccess = false,
                    Message = "fail."
                };
        }

        public async Task<ResultModel> AddSocialMedia(CreateSocialMediaModel model)
        {
            HttpResponseMessage req = await client.PostAsync("api/users/social", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultModel>(data);
            }

            else
                return new ResultModel()
                {
                    IsSuccess = false,
                    Message = "fail."
                };

        }

        public async Task<string[]> RequestAnalysis(CreateAnalysisModel model)
        {
            HttpResponseMessage req = await client.PostAsync("api/text/analysis", new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));

            if (req != null && req.IsSuccessStatusCode)
            {
                var data = await req.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<string[]>(data);
            }

            return null;
        }
    }
}
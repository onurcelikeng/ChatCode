using ChatCode.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChatCode.Helpers
{
    public class TranslateHelper
    {
        private HttpClient client = new HttpClient();


        public static TranslateHelper Instance
        {
            get
            {
                return new TranslateHelper();
            }
        }


        public async Task<string> GetAccessToken()
        {
            string uri = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken?Subscription-Key=" + DomainHelper.Instance.TranslateApiKey;

            HttpResponseMessage response = await client.PostAsync(uri, null);
            if (response != null && response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(data))
                {
                    return data;
                }
            }

            return null;
        }

        public async Task<string> GetDetectLanguage(string text)
        {
            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/languages?";

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", DomainHelper.Instance.TextAnalysisApiKey);
            
            byte[] byteData = Encoding.UTF8.GetBytes("{\r\n     \'documents\': [\r\n         {\r\n             \'id\': \'1\',\r\n             \'text\': \'" + text + "\'\r\n         }\r\n     ]\r\n }");

            HttpResponseMessage response = await client.PostAsync(uri, new ByteArrayContent(byteData));
            if (response != null && response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LanguagesModel>(json);

                return data.documents[0].detectedLanguages[0].iso6391Name;
            }

            return null;
        }
    }
}
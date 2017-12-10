using ChatCode.DAL.Entities.Tables;
using ChatCode.DAL.Entities.Tables.WebsiteTypes;
using ChatCode.DAL.Repositories;
using ChatCode.Helpers;
using ChatCode.Models;
using ChatCode.Storage;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ChatCode.Controllers
{
    [RoutePrefix("api")]
    public class MessageController : BaseController
    {
        private Repository<AboutMe> AboutMeRepo = new Repository<AboutMe>();
        private Repository<User> UserRepo = new Repository<User>();
        private HttpClient client = new HttpClient();


        [HttpPost]
        [Route("image")]
        /// <summary>
        /// ChatBot dan gelen image, service e gönderilerek imagine processing yapılıyor.
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetImage()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest != null)
                {
                    foreach (string file in httpRequest.Files)
                    {
                        var content = new MultipartFormDataContent();
                        content.Add(new StreamContent(httpRequest.Files[file].InputStream));

                        HttpClient httpClient = new HttpClient();
                        var url = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Description,Tags&subscription-key=" + DomainHelper.Instance.ComputerVisionApiKey;

                        var response = await httpClient.PostAsync(url, content);
                        var responseString = await response.Content.ReadAsStringAsync();

                        return Ok(responseString);
                    }
                }

                return BadRequest();
            }

            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("text/translate")]
        /// <summary>
        /// Burada translate yapılıyor. 
        /// Dil algılama eklendi -> TranslateHelper.Instance.GetDetectLanguage(text);
        /// AccesToken eklendi -> TranslateHelper.Instance.GetDetectLanguage(text);
        /// </summary>
        /// <returns>Text geri döner</returns>
        public async Task<IHttpActionResult> TextTranlator()
        {
            try
            {
                string text = "mein Name ist Onur";
                string from = await TranslateHelper.Instance.GetDetectLanguage(text);
                string to = "en";
                string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to;

                var accessToken = await TranslateHelper.Instance.GetAccessToken();
                if (!string.IsNullOrEmpty(accessToken))
                {
                    client.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", accessToken));

                    HttpResponseMessage response = await client.GetAsync(uri);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(data))
                        {
                            HtmlDocument htmlDocument = new HtmlDocument();
                            htmlDocument.LoadHtml(data);
                            var value = htmlDocument.DocumentNode.InnerText;

                            return Ok(value);
                        }
                    }
                }

                return BadRequest();
            }

            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("image/upload")]
        /// <summary>
        /// User ile chat sırasında image eklendiği an storage upload ediliyor.
        /// </summary>
        /// <returns>Yeni eklenen imagi in blob storage daki url adresi</returns>
        public async Task<IHttpActionResult> ImageUploadToStorage()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                if (httpRequest != null)
                {
                    ImageService imageService = new ImageService();
                    var imageUrl = await imageService.UploadImageAsync(httpRequest.Files[0]);

                    if (!string.IsNullOrEmpty(imageUrl)) return Ok(imageUrl);
                    else return BadRequest();
                }

                else return BadRequest();
            }

            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("text/languages")]
        /// <summary>
        ///  ChatBottan alınan mesajın hangi dilde yazıldığını belirler
        /// </summary>
        /// <param name="Description">ChatBot alınan description verisi</param>
        /// <returns>Alınna mesajın dilini string tipinde döner. Orn "en", "tr" ...</returns>
        public async Task<IHttpActionResult> GetLanguages(DescriptionTextModel Description)
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", DomainHelper.Instance.TextAnalysisApiKey);
            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/languages?" + queryString;

            byte[] byteData = Encoding.UTF8.GetBytes("{\r\n     \'documents\': [\r\n         {\r\n             \'id\': \'1\',\r\n             \'text\': \'" + Description.Text + "\'\r\n         }\r\n     ]\r\n }");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LanguagesModel>(responseString);
                // Sadece dil adı dönüyor.
                return Ok(result.documents[0].detectedLanguages[0].iso6391Name);
            }
        }

        [HttpPost]
        [Route("text/analysis")]
        /// <summary>
        /// Alınan text in içindeki kelimeleri analiz eder.
        /// </summary>
        /// <param name="Description">ChatBot alınan description verisi</param>
        /// <returns>Analiz edilmiş kelimeler Array olarak geri döner</returns>
        public async Task<IHttpActionResult> TextAnalysis(DescriptionTextModel Description)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", DomainHelper.Instance.TextAnalysisApiKey);
            var uri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases" + queryString;

            byte[] byteData = Encoding.UTF8.GetBytes("{\r\n     \'documents\': [\r\n         {\r\n             \'id\': \'1\',\r\n             \'text\': \'" + Description.Text + "\'\r\n         }\r\n     ]\r\n }");

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await client.PostAsync(uri, content);
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AnalysisModel>(responseString);
                return Ok(result.documents[0].keyPhrases);
            }
        }
    }
}
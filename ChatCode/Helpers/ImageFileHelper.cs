using ChatCode.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft;
using Newtonsoft.Json;
using ChatCode.Controllers;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ChatCode.Helpers
{
    public class ImageFileHelper
    {
        public byte[] GetImageAsByteArray(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);

            return binaryReader.ReadBytes((int)fileStream.Length);
        }

        public async Task<ImageModel> MakeAnalysisRequest(string path)
        {
            HttpClient client = new HttpClient();

            string uri = "https://westus.api.cognitive.microsoft.com/vision/v1.0/analyze?visualFeatures=Description,Tags&subscription-key=" + DomainHelper.Instance.ComputerVisionApiKey;
            byte[] byteData = GetImageAsByteArray(path);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                var response = await client.PostAsync(uri, content);

                var responseString = await response.Content.ReadAsStringAsync();
                var image = JsonConvert.DeserializeObject<ImageModel>(responseString);

                return image;
            }
        }
    }
}
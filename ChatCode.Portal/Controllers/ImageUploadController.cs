using ChatCode.Portal.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatCode.Portal.Controllers
{
    public class ImageUploadController : Controller
    {
        ImageService imageService = new ImageService();
        [HttpPost]
        public async Task<ActionResult> Upload1(HttpPostedFileBase imge1)
        {
            var imageUrl1 = await imageService.UploadImageAsync(imge1);

            var _url1 = imageUrl1.ToString();
            return Json(new { success = true, urlOne = _url1 });
        }
    }
}
using ChatCode.DAL.Entities.Tables;
using ChatCode.DAL.Repositories;
using ChatCode.Helpers;
using ChatCode.Models.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ChatCode.Controllers
{
    [RoutePrefix("api/website")]
    public class WebSitesController : BaseController
    {
        private Repository<Website> WebsiteRepo = new Repository<Website>();


        [HttpPost]
        [Route("star")]
        //test edilecek.
        public IHttpActionResult WebsiteStartIncrease([FromBody] CreateStarModel model)
        {
            var website = WebsiteRepo.Where(c => c.Id == model.WebsiteId).SingleOrDefault();

            website.Rating = ((website.NumberOfDownload * website.Rating) + model.Star) / (website.NumberOfDownload + 1);
            website.NumberOfDownload += 1;

            var response = WebsiteRepo.Update(website);
            if (response == true)
                return Success(website.Id.ToString());
            else
                return Error("error");
        }
    }
}
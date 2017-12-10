using ChatCode.DAL.Entities.Tables;
using ChatCode.DAL.Entities.Tables.SocialMedia;
using ChatCode.DAL.Entities.Tables.WebsiteTypes;
using ChatCode.DAL.Repositories;
using ChatCode.Helpers;
using ChatCode.Models.PostModels;
using System;
using System.Linq;
using System.Web.Http;

namespace ChatCode.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : BaseController
    {
        private Repository<User> UserRepo = new Repository<User>();
        private Repository<AboutMe> AboutmeRepo = new Repository<AboutMe>();
        private Repository<SocialLookupList> SocialLookupListRepo = new Repository<SocialLookupList>();
        private Repository<Website> WebSiteRepo = new Repository<Website>();
        private Repository<Project> ProjectRepo = new Repository<Project>();


        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddUser([FromBody] CreateAboutMeModel model)
        {
            var user = new User()
            {
                Email = model.Email
            };

            var userResponse = UserRepo.Add(user);
            if (userResponse == true)
            {
                var aboutme = new AboutMe()
                {
                    UserId = UserRepo.Where(c => c.Email == model.Email).SingleOrDefault().Id,
                    NameSurname = model.NameSurname,
                    Image = model.ImageUrl,
                    Age = Convert.ToInt32(model.Age),
                    Gender = model.Gender,
                    Description = model.Description,
                    Background = model.Background,
                    Foreground = model.Foreground
                };

                var compare = WebSiteRepo.List();

                for (int i = 0; i < model.AnalysisText.Length; i++)
                {
                    foreach(var item in compare)
                    {
                        if(item.Style1.ToLower() == model.AnalysisText[i].ToLower())
                        {
                            var projectModel = new Project()
                            {
                                UserId = aboutme.UserId,
                                Url = "empty-url",
                                WebSourceCode = compare[i].SourceCode
                            };
                            ProjectRepo.Add(projectModel);

                            break;
                        }
                    }
                }

                var response = AboutmeRepo.Add(aboutme);
                if (response == true)
                {
                    return Success("Successful create website");
                }

                else
                    return Error("UnSuccessful create website");
            }

            else
                return Error("Unsuccessful create account");
        }

        [HttpPost]
        [Route("social")]
        public IHttpActionResult AddUserSocialMedia([FromBody] CreateSocialMediaModel model)
        {
            int userId = UserRepo.Where(c => c.Email == model.Email).SingleOrDefault().Id;

            if (!string.IsNullOrEmpty(model.Facebook)) //soicalId = 1
            {
                var facebook = new SocialLookupList()
                {
                    UserId = userId,
                    SocialLookupId = 1,
                    Url = model.Facebook
                };
                SocialLookupListRepo.Add(facebook);
            }

            if (!string.IsNullOrEmpty(model.Twitter)) //soicalId = 2
            {
                var twitter = new SocialLookupList()
                {
                    UserId = userId,
                    SocialLookupId = 2,
                    Url = model.Twitter
                };
                SocialLookupListRepo.Add(twitter);
            }

            if (!string.IsNullOrEmpty(model.LinkedIn)) //soicalId = 3
            {
                var linkedin = new SocialLookupList()
                {
                    UserId = userId,
                    SocialLookupId = 3,
                    Url = model.LinkedIn
                };
                SocialLookupListRepo.Add(linkedin);
            }

            return Success("successful");
        }
    }
}
using ChatCode.Models;
using System.Web.Http;

namespace ChatCode.Helpers
{
    public class BaseController : ApiController
    {
        public IHttpActionResult Success(string message)
        {
            return Ok(new ResultModel<string>()
            {
                IsSuccess = true,
                Message = message,
                Data = null
            });
        }

        public IHttpActionResult Error(string message)
        {
            return Ok(new ResultModel<string>()
            {
                IsSuccess = false,
                Message = message,
                Data = null
            });
        }

        public IHttpActionResult Result<T>(T data, string message = null) where T : class
        {
            return Ok(new ResultModel<T>()
            {
                Data = data,
                IsSuccess = true,
                Message = message
            });
        }
    }
}
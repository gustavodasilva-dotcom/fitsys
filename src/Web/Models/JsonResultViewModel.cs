using System.Net;

namespace Web.Models
{
    public sealed class JsonResultViewModel
    {
        public JsonResultViewModel()
        {
            Data = null;
            StatusCode = HttpStatusCode.OK;
            Message = "OK";
        }

        public HttpStatusCode StatusCode { get; set; }
        public dynamic? Data { get; set; }
        public string Message { get; set; }
    }
}

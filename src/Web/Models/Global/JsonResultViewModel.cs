using System.Net;

namespace Web.Models.Global
{
    public sealed class JsonResultViewModel
    {
        public JsonResultViewModel()
        {
            data = null;
            statusCode = HttpStatusCode.OK;
            message = "OK";
        }

        public HttpStatusCode statusCode { get; set; }
        public dynamic? data { get; set; }
        public string message { get; set; }
    }
}

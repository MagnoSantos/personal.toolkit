using System;
using System.Net;

namespace Personal.Commom
{
    public class InnerError
    {
        private InnerError(string title, string detail, HttpStatusCode statusCode)
        {
            Title = title;
            Detail = detail;
            Status = ((int)statusCode).ToString();
        }

        public string Title { get; set; }
        public string Detail { get; set; }
        public string Status { get; set; }

        public static InnerError FromDefault(Exception ex, HttpStatusCode statusCode) =>
            new("unexpected error", ex.Message, statusCode);
    }
}
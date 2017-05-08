using System.Web.Mvc;

namespace Web.Models
{
    public class HandleErrorInfoViewModel
    {
        public HandleErrorInfo ErrorInfo { get; set; }
        public string ReturnUrl { get; set; }
    }
}
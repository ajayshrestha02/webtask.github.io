using EVJ.Infrastructure;
using EVJ.Infrastructure.Interface;
using EVJ.Infrastructure.Models;
using EVJ.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Web;

namespace EVJ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmailSender _emailSender;
        public HomeController(ILogger<HomeController> logger, IEmailSender emailSender)
        {
            _logger = logger;
			_emailSender=emailSender;

		}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SendEmail(ContactUsViewModel model)
        {
            if(ModelState.IsValid==true)
			{
                	var msg = "";
	msg +="<body style='width: 650px; margin: 0 auto; font-family: Avenirnextregular; background: #f3f3f3;'>";
	msg +="<table border='0' style='width: 100%;'>";
		msg +="<tbody>";
			msg +="<tr>";
			msg +="<td style='background: #fff; padding: 0;'>";
			msg +="<span>Name</span>";
			msg +="</td>";
			msg +="<td style='background: #fff; padding: 0;'>";
			msg +="<span>" + HttpUtility.HtmlEncode(model.Name) + "</span>";
			msg +="</td>";
			msg +="</tr>";
			msg +="<tr>";
			msg +="<td style='background: #fff; padding: 0;'>";
			msg +="<span>Organization</span>";
			msg +="</td>";
			msg +="<td style='background: #fff; padding: 0;'>";
			msg +="<span>"+ HttpUtility.HtmlEncode(model.Organization)+"</span>";
			msg +="</td>";
			msg +="</tr>";

		    msg +="<tr>";
			msg +="<td style='background: #fff; padding: 0;'>";
			msg +="<span>Email</span>";
			msg +="</td>";
			msg +="<td style='background: #fff; padding: 0;'>";
			msg +="<span>"+ HttpUtility.HtmlEncode(model.Email) +"</span>";
			msg +="</td>";
			msg +="</tr>";
		    msg +="<tr>";
			msg +="<td style='background: #fff; padding: 0;'>";
			msg +="<span>Comments</span>";
			msg +="</td>";
			msg +="<td style='background: #fff; padding: 0;'>";
			msg +="<span>"+HttpUtility.HtmlEncode(model.Comment) +"</span>"; 
			msg +="</td>";
			msg +="</tr>";


			msg +="</body>";

				//var message = new Message(new string[] { model.Email }, "Message to Executive Voice", msg);
				var message = new Message(new string[] { "contact@executive-voice.com" }, "Message to Executive Voice", msg);

				// _emailSender.SendEmailViaOldSMTP(model.Email, "Message to Executive Voice", model.Comment);
				_emailSender.SendEmail(message);

			}
			return Json(new { Sucess = false });
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
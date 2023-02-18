using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using QuizzAppFilRouge.Models;
using System.Text;
using MailKit.Security;

namespace QuizzAppFilRouge.Controllers
{
    public class EmailController : Controller
    {
        [HttpGet]
        [Route("/Email/Index/{validationCode}/{firstName}/{lastName}/{quizzId}/{applicantId}")]
        public IActionResult Index(string validationCode,string firstName, string lastName, int quizzId, string applicantId)
        {
            // Créer un objet EmailViewModel à passer à la vue

            var emailViewModel = new EmailViewModel()
            {
                LastName = lastName,
                FirstName = firstName,
                ValidationCode = validationCode,
                QuizzId = quizzId,
                ApplicantId = applicantId

            };

            return View("SendEmail", emailViewModel);
        }

        [HttpPost]
        [Route("/Email/Index/{validationCode}/{firstName}/{lastName}/{quizzId}/{applicantId}")]
        public IActionResult Index(EmailViewModel emailViewModel)
        {
            var quizzUrl = $"https://localhost:7256/Quizzs/CheckValidationCode?applicantId={emailViewModel.ApplicantId}&&quizzId={emailViewModel.QuizzId}";


            //var emailBody =
            //    $"Veuillez trouvez ci joint le code de validation vous permettant d'accéder au quizz " +
            //    $": {Environment.NewLine}" +
            //    $"{emailViewModel.ValidationCode}" +
            //    $"et l'URL du Quizz pour y accéder" +
            //    $"{emailViewModel.ValidationCode}" +
            //    $"{quizzUrl}" +
            //    $"";

            var emailBody = "Test";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("howard.ratke96@ethereal.email"));
            email.To.Add(MailboxAddress.Parse("howard.ratke96@ethereal.email"));
            email.Subject = "Code de validation passage du Quizz";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };


            using var smtp = new SmtpClient();
            smtp.Connect("howard.ratke96@ethereal.email", 587);
            smtp.Authenticate("howard.ratke96@ethereal.email", "NBGv9wMN4BpwMPcccE");
            smtp.Send(email);

            smtp.Disconnect(true);

            return Ok();
            //return RedirectToAction("Quizzs");



        }



    }
}

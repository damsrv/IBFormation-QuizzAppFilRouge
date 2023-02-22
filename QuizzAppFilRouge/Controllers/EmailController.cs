using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Domain;
using QuizzAppFilRouge.Models;

namespace QuizzAppFilRouge.Controllers
{
    public class EmailController : Controller
    {
        private readonly IQuizzRepository quizzsRepository;


        public EmailController(IQuizzRepository quizzsRepository)
        {
            this.quizzsRepository = quizzsRepository;

        }


        [HttpGet]
        [Route("/Email/Index/{validationCode}/{firstName}/{lastName}/{quizzId}/{applicantId}/{date}")]
        public async Task<IActionResult> Index(string validationCode, string firstName, string lastName, int quizzId, string applicantId, DateTime date)
        {


            // Créer un objet EmailViewModel à passer à la vue

            var emailViewModel = new EmailViewModel()
            {
                LastName = lastName,
                FirstName = firstName,
                ValidationCode = validationCode,
                QuizzId = quizzId,
                ApplicantId = applicantId,

            };

            return View("SendEmail", emailViewModel);
        }

        [HttpPost]
        [Route("/Email/Index/{validationCode}/{firstName}/{lastName}/{quizzId}/{applicantId}/{date}")]
        public async Task<IActionResult> Index(EmailViewModel emailViewModel)
        {

            // Obligé de récupérer la date en base car impossible de la passer en param
            var passage = await quizzsRepository.GetPassageDate(emailViewModel.QuizzId);


            var quizzUrl = $"https://localhost:7256/Quizzs/CheckValidationCode?applicantId={emailViewModel.ApplicantId}&&quizzId={emailViewModel.QuizzId}";


            var emailBody =
                $"Veuillez trouvez ci joint le code de validation vous permettant d'accéder au quizz" +
                $"" +
                $"" +
                $"" +
                $"" +
                $"" +
                $"{Environment.NewLine}" +
                $"{emailViewModel.ValidationCode}" +
                $"{Environment.NewLine}" +
                $"ainsi que l'URL du Quizz pour y accéder" +
                $"{Environment.NewLine}" +
                $"{quizzUrl}" +
                $"{Environment.NewLine}" +
                $"Votre date de passage est prévue le : {passage}";

            //var emailBody = "Test";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("candidatfilrouge@hotmail.com"));
            email.To.Add(MailboxAddress.Parse("candidatfilrouge@hotmail.com"));
            email.Subject = "Code de validation passage du Quizz";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };
            //

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587);
            smtp.Authenticate("candidatfilrouge@hotmail.com", "TestTestTest@2023!");
            smtp.Send(email);

            smtp.Disconnect(true);

            var isAdmin = User.IsInRole("Admin");

            return RedirectToAction("GetAllQuizzs", "Quizzs", new {isAdmin = isAdmin});

        }

        /**
         * Méthode envoi de mail au recruteur en fin de passage de quizz pour le candidat.
         */
        [HttpGet]
        public IActionResult SendEmailEndQuizz(int quizzId)
        {

            var date = DateTime.Now;
            var days = date.ToString("dd-MM-yyyy");
            var hours = date.ToString("HH:mm:ss");

            var emailBody =
                $"Le quizz n° {quizzId} vient d'être terminé par le candidat le {days} à {hours}.";


            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("candidatfilrouge@hotmail.com"));
            email.To.Add(MailboxAddress.Parse("candidatfilrouge@hotmail.com"));
            email.Subject = "Code de validation passage du Quizz";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = emailBody
            };
            //

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.office365.com", 587);
            smtp.Authenticate("candidatfilrouge@hotmail.com", "TestTestTest@2023!");
            smtp.Send(email);

            smtp.Disconnect(true);

            //return RedirectToAction("Index", "Quizzs");

            return View("~/Views\\Quizzs\\ThanksMessage.cshtml");

        }
    }

}
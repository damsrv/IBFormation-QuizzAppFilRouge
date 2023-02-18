using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Domain;

namespace QuizzAppFilRouge.Controllers
{
    public class PDFController : Controller
    {

        private readonly IResponseRepository responseRepository;

        public PDFController (IResponseRepository responseRepository)
        {
            this.responseRepository = responseRepository;

        }


        public IActionResult Index()
        {
            return View();
        }

        [Route("/PDF/CreatePDFForFreeAnwer/{id}")]
        public async Task<IActionResult> CreatePDFForFreeAnwer(int id)
        {
            var freeQuestionsAnswers = await responseRepository.GetFreeQuestionResponses(id);

            string test = "";

            foreach (var freeQuestionAnswer in freeQuestionsAnswers)
            {
                test +=
                    $": {Environment.NewLine}" + 
                    $"Intitulé de la question : " +
                    $": {Environment.NewLine}" +
                    $"{freeQuestionAnswer.Question.Content}" +
                    $": {Environment.NewLine}" +
                    $"{freeQuestionAnswer.Content}" +
                    $": {Environment.NewLine}";

            };

            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            //Draw the text.
            graphics.DrawString(test, font, PdfBrushes.Black, new PointF(0, 0));
            //Saving the PDF to the MemoryStream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = "Sample.pdf";
            return fileStreamResult;
        }

        [Route("/PDF/CreateResultPDF/{id}")]
        public async Task<IActionResult> CreateResultPDF(int id)
        {
            var freeQuestionsAnswers = await responseRepository.GetFreeQuestionResponses(id);
            string test = "";

            foreach (var freeQuestionAnswer in freeQuestionsAnswers)
            {
                test = $"Intitulé de la question : " +
                    $": {Environment.NewLine}" +
                    $"{freeQuestionAnswer.Question.Content}" +
                    $": {Environment.NewLine}" +
                    $"{freeQuestionAnswer.Content}" +
                    $": {Environment.NewLine}";

            };

            //Create a new PDF document.
            PdfDocument document = new PdfDocument();
            //Add a page to the document.
            PdfPage page = document.Pages.Add();
            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;
            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            //Draw the text.
            graphics.DrawString(test, font, PdfBrushes.Black, new PointF(0, 0));
            //Saving the PDF to the MemoryStream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = "Sample.pdf";
            return fileStreamResult;
        }




    }
}

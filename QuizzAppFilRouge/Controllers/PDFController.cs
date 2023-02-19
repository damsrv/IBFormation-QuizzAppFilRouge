using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using QuizzAppFilRouge.Data.Entities;
using QuizzAppFilRouge.Domain;
using Syncfusion.Pdf.Grid;
using System.Data;

namespace QuizzAppFilRouge.Controllers
{
    public class PDFController : Controller
    {

        private readonly IResponseRepository responseRepository;
        private readonly IQuizzRepository quizzRepository;


        public PDFController (IResponseRepository responseRepository, IQuizzRepository quizzRepository )
        {
            this.responseRepository = responseRepository;
            this.quizzRepository = quizzRepository;

        }


        public IActionResult Index()
        {
            return View();
        }

        [Route("/PDF/CreatePDFForFreeAnwer/{quizzid}")]
        public async Task<IActionResult> CreatePDFForFreeAnwer(int quizzid)
        {
            var freeQuestionsAnswers = await responseRepository.GetFreeQuestionResponses(quizzid);

            string pdfMessage = "";

            foreach (var freeQuestionAnswer in freeQuestionsAnswers)
            {
                if (freeQuestionAnswer.Content == null)
                    freeQuestionAnswer.Content = "L'utilisateur n'a pas répondu à cette question.";

                pdfMessage +=
                    $"{Environment.NewLine}" + 
                    $"Intitulé de la question : " +
                    $"{Environment.NewLine}" +
                    $"{freeQuestionAnswer.Question.Content}" +
                    $"{Environment.NewLine}" +
                    $"Réponse à la question " +
                    $"{Environment.NewLine}" +
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
            graphics.DrawString(pdfMessage, font, PdfBrushes.Black, new PointF(0, 0));
            //Saving the PDF to the MemoryStream.
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            //Set the position as '0'.
            stream.Position = 0;
            //Download the PDF document in the browser.
            FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            fileStreamResult.FileDownloadName = $"Questions Libres Quizz n° {quizzid}.pdf";
            return fileStreamResult;
        }

        // OK FONCTIONNE NICKEL
        [Route("/PDF/CreateResultPDF/{quizzId}/")]
        public async Task<IActionResult> CreateResultPDF(int quizzId)        
        {
            // récupere le quizz
            var quizz = await quizzRepository.GetQuizzById(quizzId);

            var quizzResponses = quizz.Responses.ToList();

            string notation = $"Note : {quizz.Notation} / {quizzResponses.Count}"; 

            //Generate a new PDF document.
            PdfDocument doc = new PdfDocument();

            //Add a page.
            PdfPage page = doc.Pages.Add();

            //Create PDF graphics for the page.
            PdfGraphics graphics = page.Graphics;

            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the text. // Ajoute la note en haut
            graphics.DrawString(notation, font, PdfBrushes.Black, new PointF(10, 10));

            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();

            //Add values to list.
            var data = createListForResultPDF(quizzResponses);

            //Add list to IEnumerable.
            IEnumerable<object> dataTable = data;

            //Assign data source.
            pdfGrid.DataSource = dataTable;

            //Draw grid to the page of PDF document.
            pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(10, 50));

            //Write the PDF document to stream.
            MemoryStream stream = new MemoryStream();
            doc.Save(stream);

            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;

            //Close the document.
            doc.Close(true);

            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";

            //Define the file name.
            string fileName = $"Formulaire de résultat - Quizz n° {quizzId}.pdf";

            //Creates a FileContentResult object by using the file contents, content type, and file name.
            return File(stream, contentType, fileName);
        }


        //////////////////////////////////////////////////////////////////////////////////////
        //// OTHER FUNCTIONS
        //////////////////////////////////////////////////////////////////////////////////////

        // Transforme une liste de réponse en liste d'objet utilisable par la biblioteque
        // de génération de pdf
        public List<object> createListForResultPDF (List<Response> quizzResponses)
        {
            List<object> data = new List<object>();
            string noResponse = "l'utilisateur n'a pas répondu à cette question.";
            var officialResponse = "";

            foreach (var response in quizzResponses)
            {
                // si question libres alors no answer à ajouter
                // Affiche juste bonne ou mauvaise réponse
                if (response.Question.Answers.Count == 0)
                {
                    if(response.Content != null) 
                    {
                        officialResponse = response.IsCorrect == true ? "Bonne réponse" : "Mauvaise réponse";
                    }
                    else // Sauf si contenu vide alors message de non remplissage
                    {
                        response.Content = noResponse;
                        officialResponse = response.IsCorrect == true ? "Bonne réponse" : "Mauvaise réponse";
                    }
                }
                // Si l'utilisateur n'a pas répondu à une question QCM
                else if (response.Content == null)
                {
                    response.Content = noResponse;

                }
                // Si il y à bien une réponse.
                else
                {
                    officialResponse = response.Question.Answers
                        .Where(answer => answer.IsARightAnswer == true)
                        .Select(answer => answer.Content)
                        .FirstOrDefault();
                }


                Object newRow = new
                {
                    Intitulé = response.Question.Content,
                    Réponse = response.Content,
                    Officielle = officialResponse,
                };
                data.Add(newRow);
            }

            return data;
        }





    }
}

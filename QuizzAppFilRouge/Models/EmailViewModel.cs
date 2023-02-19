namespace QuizzAppFilRouge.Models
{
    public class EmailViewModel
    {
        public string FirstName{ get; set; }
        public string ValidationCode{ get; set; }
        public string LastName { get; set; }

        public int QuizzId { get; set; }

        public string ApplicantId { get; set; }

        public DateTime PassageDate { get; set; }

    }
}

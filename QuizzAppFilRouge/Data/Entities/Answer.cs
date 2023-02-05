namespace QuizzAppFilRouge.Data.Entities
{
    public class Answer
    {
        public int Id { get; set; }
    
        public string Content { get; set; }

        public bool IsARightAnswer { get; set; }
    
        public Question Question { get; set; }


    }
}

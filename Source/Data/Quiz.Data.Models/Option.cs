namespace Quiz.Data.Models
{
    public class Option
    {
        public int Id { get; set; }

        public bool IsCorrect { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}

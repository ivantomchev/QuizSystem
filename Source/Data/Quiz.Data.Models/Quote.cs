namespace Quiz.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Quote
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}

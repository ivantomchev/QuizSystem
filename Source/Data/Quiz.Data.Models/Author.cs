namespace Quiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        private ICollection<Quote> quotes;

        public Author()
        {
            this.quotes = new HashSet<Quote>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Quote> Quotes
        {
            get
            {
                return this.quotes;
            }
            set
            {
                this.quotes = value;
            }
        }
    }
}

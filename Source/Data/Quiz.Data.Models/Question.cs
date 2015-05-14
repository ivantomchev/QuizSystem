namespace Quiz.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Question
    {
        private ICollection<Option> options;

        private ICollection<Exam> exams;

        public Question()
        {
            this.options = new HashSet<Option>();
            this.exams = new HashSet<Exam>();
        }

        [Key]
        public int Id { get; set; }

        public int QuoteId { get; set; }

        public virtual Quote Quote { get; set; }

        public virtual ICollection<Option> Options
        {
            get
            {
                return this.options;
            }
            set
            {
                this.options = value;
            }
        }

        public virtual ICollection<Exam> Exams
        {
            get
            {
                return this.exams;
            }
            set
            {
                this.exams = value;
            }
        }
    }
}

namespace Quiz.Data.Models
{
    using System.Collections.Generic;

    public class Exam
    {
        private ICollection<Question> questions;

        public int Id { get; set; }

        public Exam()
        {
            this.questions = new HashSet<Question>();
        }

        public virtual ICollection<Question> Questions
        {
            get
            {
                return this.questions;
            }
            set
            {
                this.questions = value;
            }
        }
    }
}

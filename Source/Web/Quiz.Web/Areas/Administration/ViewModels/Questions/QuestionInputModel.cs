namespace Quiz.Web.Areas.Administration.ViewModels.Questions
{
    using Quiz.Data.Models;
    using Quiz.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class QuestionInputModel : IMapFrom<Question>
    {
        public QuestionInputModel()
        {
            this.Options = new HashSet<Option>();
        }

        public ICollection<Option> Options { get; set; }

        public int QuoteId { get; set; }

        public IEnumerable<SelectListItem> QuoteList { get; set; }

        public int[] selectedAuthors { get; set; }

        public IEnumerable<SelectListItem> AuthorList { get; set; }
    }
}
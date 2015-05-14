namespace Quiz.Web.Areas.Administration.ViewModels.Quotes
{
    using Quiz.Data.Models;
    using Quiz.Web.Infrastructure.Mapping;

    public class QuoteDeleteViewModel : IMapFrom<Quote>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
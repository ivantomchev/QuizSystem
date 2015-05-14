namespace Quiz.Web.Areas.Administration.ViewModels.Authors
{
    using Quiz.Data.Models;
    using Quiz.Web.Infrastructure.Mapping;

    public class AuthorIndexViewModel : IMapFrom<Author>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
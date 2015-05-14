namespace Quiz.Web.Areas.Administration.ViewModels.Quotes
{
    using AutoMapper;

    using Quiz.Data.Models;
    using Quiz.Web.Infrastructure.Mapping;

    public class QuoteIndexViewModel : IMapFrom<Quote>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Quote, QuoteIndexViewModel>()
                .ForMember(d => d.Author, opt => opt.MapFrom(s => s.Author.Name));
        }
    }
}
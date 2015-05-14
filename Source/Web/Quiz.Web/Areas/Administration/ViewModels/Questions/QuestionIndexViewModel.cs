namespace Quiz.Web.Areas.Administration.ViewModels.Questions
{
    using AutoMapper;
    using Quiz.Data.Models;
    using Quiz.Web.Infrastructure.Mapping;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class QuestionIndexViewModel : IMapFrom<Question>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "About")]
        public string Quote { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionIndexViewModel>()
                .ForMember(d => d.Quote, opt => opt.MapFrom(s => s.Quote.Content));
        }
    }
}
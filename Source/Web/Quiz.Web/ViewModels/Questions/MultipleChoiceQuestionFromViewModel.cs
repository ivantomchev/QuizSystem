namespace Quiz.Web.ViewModels.Questions
{
    using AutoMapper;
    using Quiz.Data.Models;
    using Quiz.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class MultipleChoiceQuestionFromViewModel : IMapFrom<Question>, IHaveCustomMappings
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int QuoteId { get; set; }

        [Display(Name="Quote")]
        public string QuoteContent { get; set; }

        public int AnswerId { get; set; }

        public IEnumerable<Option> Options { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Question, MultipleChoiceQuestionFromViewModel>()
                .ForMember(d => d.QuoteId, opt => opt.MapFrom(s => s.Quote.Id))
                .ForMember(d => d.QuoteContent, opt => opt.MapFrom(s => s.Quote.Content));
        }
    }
}
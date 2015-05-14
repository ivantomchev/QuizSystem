using Quiz.Data.Models;
using Quiz.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quiz.Web.Areas.Administration.ViewModels.Questions
{
    public class QuestionDetailsViewModel : IMapFrom<Question>,IHaveCustomMappings
    {
        public string Quote { get; set; }

        public IEnumerable<Option> Options { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Question, QuestionDetailsViewModel>()
                .ForMember(d => d.Quote, opt => opt.MapFrom(s => s.Quote.Content))
                .ForMember(d => d.Options, opt => opt.MapFrom(s => s.Options));
        }
    }
}
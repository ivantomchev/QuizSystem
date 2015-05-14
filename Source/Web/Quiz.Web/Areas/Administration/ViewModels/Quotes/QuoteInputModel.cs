namespace Quiz.Web.Areas.Administration.ViewModels.Quotes
{
    using AutoMapper;
    using Quiz.Data.Models;
    using Quiz.Web.Infrastructure.Mapping;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class QuoteInputModel : IMapFrom<Quote>, IHaveCustomMappings
    {
        [Required]
        [UIHint("SmallMultiLineText")]
        [StringLength(250, MinimumLength = 2)]
        public string Content { get; set; }

        [Display(Name = "Author")]
        public int selectedAuthor { get; set; }

        public IEnumerable<SelectListItem> AuthorsList { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<QuoteInputModel, Quote>()
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.selectedAuthor));
        }
    }
}
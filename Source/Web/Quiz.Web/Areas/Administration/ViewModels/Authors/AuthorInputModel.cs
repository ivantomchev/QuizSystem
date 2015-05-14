namespace Quiz.Web.Areas.Administration.ViewModels.Authors
{
    using System.ComponentModel.DataAnnotations;

    using Quiz.Data.Models;
    using Quiz.Web.Infrastructure.Mapping;

    public class AuthorInputModel : IMapFrom<Author>
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [UIHint("SingleLineText")]
        public string Name { get; set; }
    }
}
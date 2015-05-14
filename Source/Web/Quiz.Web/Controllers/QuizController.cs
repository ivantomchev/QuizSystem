namespace Quiz.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Quiz.Data.UnitOfWork;
    using Quiz.Web.ViewModels.Questions;

    public class QuizController : BaseController
    {

        public QuizController(IQuizData data)
            : base(data)
        {
        }

        public ActionResult MultiChoiceMode()
        {
            return PartialView("_MultiChoiceModePartial");
        }

        public ActionResult BinaryMode()
        {
            return PartialView("_BinaryModePartial");
        }

        public ActionResult QuizMode()
        {
            return PartialView("QuizMode");
        }

        public ActionResult NextQuestionBinary(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);

            var count = this.Data.Questions.All().Count();

            if (pageNumber > count)
            {
                return PartialView("_QuizFinishedPartial");
            }

            var data = this.GetData<BinaryModeQuestionViewModel>().OrderBy(x => x.Id).Skip(pageNumber - 1).Take(1).First();

            data.Author = this.Data.Authors.All().OrderBy(x => Guid.NewGuid()).Take(1).First();
            TempData["Current"] = pageNumber + 1;

            return PartialView("_NextQuestionBinaryPartial", data);
        }

        [HttpPost]
        public ActionResult CheckQuestionBinary(int AuthorId, int QuoteId, bool Answer)
        {
            ViewBag.Current = TempData["Current"];
            var quote = this.Data.Quotes.GetById(QuoteId);

            var model = new AnswerViewModel();
            model.AuthorName = quote.Author.Name;

            if ((quote.AuthorId == AuthorId) == Answer)
            {
                model.Message = "Correct! The right answer is:";
                return PartialView("_AnswerBinaryPartial", model);
            }

            model.Message = "Sorry, you are wrong! The right answer is:";
            return PartialView("_AnswerBinaryPartial", model);
        }


        public ActionResult NextQuestionMultiChoice(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);

            var count = this.Data.Questions.All().Count();

            if (pageNumber > count)
            {
                return PartialView("_QuizFinishedPartial");
            }

            var data = this.GetData<MultipleChoiceQuestionFromViewModel>().OrderBy(x => x.Id).Skip(pageNumber - 1).Take(1).First();

            TempData["Current"] = pageNumber + 1;

            return PartialView("_NextQuestionMultiChoicePartial", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckQuestionMultiChoice(MultipleChoiceQuestionFromViewModel model)
        {
            var quote = this.Data.Quotes.GetById(model.QuoteId);

            ViewBag.Current = TempData["Current"];

            var answer = new AnswerViewModel();
            answer.AuthorName = quote.Author.Name;
            if (model.AnswerId == quote.AuthorId)
            {
                answer.Message = "Correct! The right answer is:";

                return PartialView("_AnswerMultiChoicePartial", answer);
            }

            answer.Message = "Sorry, you are wrong! The right answer is:";

            return PartialView("_AnswerMultiChoicePartial", answer);
        }


        private IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Questions.All().Project().To<TViewModel>();
        }

    }
}
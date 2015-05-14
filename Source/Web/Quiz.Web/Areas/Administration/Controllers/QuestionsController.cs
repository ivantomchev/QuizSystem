namespace Quiz.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Quiz.Common.Extensions;
    using Quiz.Data.Models;
    using Quiz.Data.UnitOfWork;
    using Quiz.Web.Areas.Administration.Controllers.Base;

    using DbModel = Quiz.Data.Models.Question;
    using DetailedViewModel = Quiz.Web.Areas.Administration.ViewModels.Questions.QuestionDetailsViewModel;
    using IndexViewModel = Quiz.Web.Areas.Administration.ViewModels.Questions.QuestionIndexViewModel;
    using InputModel = Quiz.Web.Areas.Administration.ViewModels.Questions.QuestionInputModel;

    public class QuestionsController : AdminController
    {
        private const int PageSize = 10;

        public QuestionsController(IQuizData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            var data = this.GetData<IndexViewModel>();

            return View(data);
        }

        public ActionResult ReadData(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);
            var count = (double)GetData<IndexViewModel>().Count();
            var data = GetData<IndexViewModel>().OrderByDescending(x => x.Id).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            ViewBag.Pages = Math.Ceiling(count / PageSize);
            ViewBag.CurrentPage = pageNumber;
            ViewBag.PreviousPage = pageNumber - 1;
            ViewBag.NextPage = pageNumber + 1;

            return PartialView("_ReadQuestionsPartial", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            model.AuthorList = this.Data.Authors.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();
            model.QuoteList = this.Data.Quotes.All().ToSelectList(x => x.Content, x => x.Id).OrderBy(x => x.Text).ToList();
            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            //PopulateSelectedAuthors(model.Options, model.selectedAuthors);
            foreach (var item in model.selectedAuthors)
            {
                var option = new Option();
                option.AuthorId = item;
                option.IsCorrect = false;

                model.Options.Add(option);
            }

            var correctOption = new Option();
            correctOption.AuthorId = this.Data.Authors.All().Where(x => x.Quotes.FirstOrDefault().Id == model.QuoteId).Select(x => x.Id).First();
            correctOption.IsCorrect = true;
            model.Options.Add(correctOption);

            var dbModel = base.Create<DbModel>(model);

            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }
            model.AuthorList = this.Data.Authors.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();
            model.QuoteList = this.Data.Quotes.All().ToSelectList(x => x.Content, x => x.Id).OrderBy(x => x.Text).ToList();
            return View(model);
        }


        [HttpGet]
        public ActionResult Details(int? id)
        {
            var model = base.GetViewModel<DbModel, DetailedViewModel>(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Questions.GetById(id) as T;
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Questions.All().Project().To<TViewModel>();
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData","Questions");
        }
    }
}
namespace Quiz.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Quiz.Data.UnitOfWork;
    using Quiz.Common.Extensions;
    using Quiz.Web.Areas.Administration.Controllers.Base;

    using DbModel = Quiz.Data.Models.Quote;
    using IndexViewModel = Quiz.Web.Areas.Administration.ViewModels.Quotes.QuoteIndexViewModel;
    using InputModel = Quiz.Web.Areas.Administration.ViewModels.Quotes.QuoteInputModel;
    using DeleteViewModel = Quiz.Web.Areas.Administration.ViewModels.Quotes.QuoteDeleteViewModel;

    public class QuotesController : AdminController
    {
        private const int PageSize = 10;

        public QuotesController(IQuizData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
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

            return PartialView("_ReadQuotesPartial", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();
            model.AuthorsList = this.Data.Authors.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(InputModel model)
        {
            var dbModel = base.Create<DbModel>(model);

            if (dbModel != null)
            {
                return RedirectToAction("Index");
            }

            model.AuthorsList = this.Data.Authors.All().ToSelectList(x => x.Name, x => x.Id).OrderBy(x => x.Text).ToList();

            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, DeleteViewModel>(id);

            return PartialView("_DeleteQuotePartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(DeleteViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id);

            return base.GridOperationAjaxRefreshData();
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Quotes");
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Quotes.All().Project().To<TViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Quotes.GetById(id) as T;
        }
    }
}
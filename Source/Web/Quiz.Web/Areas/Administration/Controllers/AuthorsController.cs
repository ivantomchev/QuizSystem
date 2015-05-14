namespace Quiz.Web.Areas.Administration.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Quiz.Data.UnitOfWork;
    using Quiz.Web.Areas.Administration.Controllers.Base;

    using DbModel = Quiz.Data.Models.Author;
    using IndexViewModel = Quiz.Web.Areas.Administration.ViewModels.Authors.AuthorIndexViewModel;
    using InputModel = Quiz.Web.Areas.Administration.ViewModels.Authors.AuthorInputModel;

    public class AuthorsController : AdminController
    {
        private const int PageSize = 10;

        public AuthorsController(IQuizData data)
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

            return PartialView("_ReadAuthorsPartial", data);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new InputModel();

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

            return View(model);
        }

        [HttpGet]
        public ActionResult ActualDelete(int? id)
        {
            var model = base.GetViewModel<DbModel, IndexViewModel>(id);

            return PartialView("_DeleteAuthorPartial", model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ActualDelete(IndexViewModel model)
        {
            base.ActualDelete<DbModel>(model.Id);

            return base.GridOperationAjaxRefreshData();
        }

        protected override string GetReadDataActionUrl()
        {
            return Url.Action("ReadData", "Authors");
        }

        protected override IQueryable<TViewModel> GetData<TViewModel>()
        {
            return this.Data.Authors.All().Project().To<TViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Authors.GetById(id) as T;
        }
    }
}
using Quiz.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiz.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IQuizData data)
            :base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StartExam()
        {
            return View();
        }

        public ActionResult NextQuestion(int? Id)
        {
            int pageNumber = Id.GetValueOrDefault(1);

            var count = this.Data.Questions.All().Count();

            if (pageNumber > count)
            {
                return Content("Finish");
            }

            var data = this.Data.Questions.All().OrderBy(x => x.Id).Skip(pageNumber - 1).Take(1).First();
            //var count = (double)GetData<IndexViewModel>().Count();
            //var data = GetData<IndexViewModel>().OrderByDescending(x => x.CreatedOn).Skip((pageNumber - 1) * PageSize).Take(PageSize);

            //ViewBag.Pages = Math.Ceiling(count / PageSize);
            //ViewBag.CurrentPage = pageNumber;
            //ViewBag.PreviousPage = pageNumber - 1;
            //ViewBag.NextPage = pageNumber + 1;

            ViewBag.Current = pageNumber + 1;

            return View(data);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
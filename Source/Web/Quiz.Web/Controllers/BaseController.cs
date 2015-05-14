namespace Quiz.Web.Controllers
{
    using Quiz.Data.Models;
    using Quiz.Data.UnitOfWork;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class BaseController : Controller
    {
        protected IQuizData Data { get; private set; }

        protected User CurrentUser { get; private set; }

        public BaseController(IQuizData data)
        {
            this.Data = data;
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            this.CurrentUser = this.Data.Users.All().Where(u => u.UserName == requestContext.HttpContext.User.Identity.Name).FirstOrDefault();

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}
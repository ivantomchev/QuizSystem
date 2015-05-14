namespace Quiz.Web.Areas.Administration.Controllers.Base
{
    using System.Data.Entity;
    using System.IO;
    using System.Web.Mvc;
    using System.Web;
    using System.Collections;

    using AutoMapper;

    using Quiz.Data.UnitOfWork;
    using Quiz.Web.Controllers;
    using System.Linq;
    //using Quiz.Common;

    public abstract class AdminController : BaseController
    {

        // [Authorize(Roles = "Admin")]
        public AdminController(IQuizData data)
            : base(data)
        {
        }

        protected abstract string GetReadDataActionUrl();

        protected abstract IQueryable<TViewModel> GetData<TViewModel>() where TViewModel : class;

        protected abstract T GetById<T>(object id) where T : class;

        protected virtual TViewModel GetViewModel<TModel, TViewModel>(object id)
            where TModel : class
            where TViewModel : class
        {
            if (id == null)
            {
                return null;
            }

            var dbModel = this.GetById<TModel>(id);

            if (dbModel == null)
            {
                return null;
            }

            var model = Mapper.Map<TViewModel>(dbModel);

            return model;
        }

        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        protected virtual TModel Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : class
            where TViewModel : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);

                return dbModel;
            }
            return null;
        }

        protected virtual void Delete<TModel>(object model)
            where TModel : class
        {
            this.ChangeEntityStateAndSave(model, EntityState.Modified);
        }

        //TODO Make this return null if fail
        protected virtual void ActualDelete<T>(object id)
            where T : class
        {
            var dbModel = this.GetById<T>(id);
            this.ChangeEntityStateAndSave(dbModel, EntityState.Deleted);
        }

        protected void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }

        protected JsonResult GridOperation()
        {
            return Json(new { success = true });
        }

        protected JsonResult GridOperationAjaxRefreshData()
        {
            return Json(new { success = true, url = this.GetReadDataActionUrl() });
        }
    }
}
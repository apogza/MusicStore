using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public abstract class BaseController<TEntity> : Controller where TEntity : BaseEntity, new()
    {
        protected DbContext DbContext { get; set; } 
        protected IUnitOfWork UnitOfWork { get; set; }
        protected IRepository<TEntity> Repository { get; set; }
        //to be used when an individual entity is loaded
        protected List<string> Includes { get; set; }
        //to be used when a list of entities is loaded
        protected List<string> ListIncludes {get; set; }
        //used to detect whether we need to redirect to a different controller and action
        protected Helpers.RedirectObject RedirectAfterDbMod { get; set; }
        protected Expression<Func<TEntity, bool>> Filter { get; set; }
        protected Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; set;}

        public BaseController(DbContext context)
        {
            DbContext = context;
            UnitOfWork = new UnitOfWork(DbContext);
            Repository = UnitOfWork.Repository<TEntity>();
        }

        public virtual async Task<IActionResult> Index()
        {
            IEnumerable<TEntity> listEntity  = await Repository.Get(Filter, OrderBy, ListIncludes);
            return View(listEntity);
        }
        
        [HttpGetAttribute]
        public virtual async Task<IActionResult> CreateEdit(int? id)
        {
            TEntity entity = new TEntity();
            
            if(id.HasValue)
                entity = await Repository.GetById(id.Value, Includes);

            return View(entity);
        }

        [HttpPostAttribute]
        [ValidateAntiForgeryTokenAttribute]
        public virtual IActionResult CreateEdit(TEntity entity)
        {
            bool success = false;
            if(entity.ID == 0 && ModelState.IsValid)
            {
                Repository.Insert(entity);
                success = true;
            }
            else
            {
                if(ModelState.IsValid)
                {
                    Repository.Update(entity);
                    success = true;
                }
            }

            if(success)
                return RedirectOnSuccess();

            return View(entity);
        }

        [HttpGetAttribute]
        public virtual async Task<IActionResult> Delete(int id)
        {   
            TEntity entity = await Repository.GetById(id);
            return View(entity);
        } 

        [HttpPostAttribute, ActionNameAttribute("Delete")]
        [ValidateAntiForgeryTokenAttribute]
        public virtual async Task<IActionResult> DeleteConfirmed(int id)
        {
            TEntity entity = await Repository.GetById(id);
            Repository.Delete(entity);
            return RedirectOnSuccess();
        }

        public virtual async Task<IActionResult> Details(int id)
        {
            TEntity entity = await Repository.GetById(id, Includes);
            return View(entity);
        }

        protected override void Dispose(bool disposing)
        {
            UnitOfWork.Dispose();
            base.Dispose(disposing);
        }

        ///redirect to a specified controller and action 
        ///upon successful completion of insert/update/delete
        protected IActionResult RedirectOnSuccess()
        {
            if(RedirectAfterDbMod != null)
            {
                string action = RedirectAfterDbMod.Action;
                string controller = RedirectAfterDbMod.Controller;
                int? ID = RedirectAfterDbMod.ID;
                
                //reset the redirect object
                RedirectAfterDbMod = null;

                if(!string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action) && ID.HasValue)
                    return RedirectToAction(action, controller, new { id = ID });    

                if(!string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action) && !ID.HasValue)
                    return RedirectToAction(action, controller);

                if(string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action) && ID.HasValue)
                    return RedirectToAction(action, new {id = ID});

                if(string.IsNullOrEmpty(controller) && !string.IsNullOrEmpty(action) && !ID.HasValue)
                    return RedirectToAction(action);
            }   

            return RedirectToAction("Index");
        }

    }
}
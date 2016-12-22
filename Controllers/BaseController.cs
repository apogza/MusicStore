using System.Collections.Generic;
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
        protected List<string> Includes { get; set; }

        public BaseController(DbContext context)
        {
            DbContext = context;
            UnitOfWork = new UnitOfWork(DbContext);
            Repository = UnitOfWork.Repository<TEntity>();
        }

        public virtual async Task<IActionResult> Index()
        {
            IEnumerable<TEntity> listEntity  = await Repository.Table.ToListAsync();
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
            if(entity.ID == 0 && ModelState.IsValid)
            {
                Repository.Insert(entity);
                return RedirectToAction("Index");
            }
            else
            {
                if(ModelState.IsValid)
                {
                    Repository.Update(entity);
                    return RedirectToAction("Index");
                }
            }

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
            return RedirectToAction("Index");
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
    }
}
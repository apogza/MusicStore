using MusicStore.Models;
using MusicStore.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MusicStore.Controllers
{
    public class SongController : BaseController<Song>
    {
        public SongController(MusicStoreContext context)
            :base(context)
        {
            Includes = new List<string>{"Album"};
        }

        public override async Task<IActionResult> CreateEdit(int? id)
        {
            ViewBag.ListAlbums = await UnitOfWork.Repository<Album>().Table.ToListAsync();
            return await base.CreateEdit(id);
        } 
    }
}
using MusicStore.Models;
using MusicStore.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MusicStore.Controllers
{
    public class SongController : BaseController<Song>
    {
        public SongController(MusicStoreContext context)
            :base(context)
        {
            Includes = new List<string>{ "Album" };
            ListIncludes = new List<string>{ "Album" };
            
            OrderBy = (songs => songs.OrderBy(s => s.AlbumID).ThenBy(s => s.TrackNumber));
        }

        public override async Task<IActionResult> CreateEdit(int? id)
        {
            ViewBag.ListAlbums = await UnitOfWork.Repository<Album>().Table.ToListAsync();
            return await base.CreateEdit(id);
        }

        [HttpGetAttribute]
        [RouteAttribute("Song/CreateEditByAlbumId/{albumId}")]
        public async Task<IActionResult> CreateEditByAlbumId(int albumId)
        {
            ViewBag.ListAlbums = await UnitOfWork.Repository<Album>().Table.ToListAsync();

            Song song = new Song();
            song.ID = 0;
            song.AlbumID = albumId;

            return View(song);
        }
        [HttpPostAttribute]
        [ValidateAntiForgeryTokenAttribute]
        public IActionResult CreateEditByAlbumId(Song song)
        {
            RedirectAfterDbMod = 
                new Helpers.RedirectObject{ Controller = "Album", Action = "Details", ID = song.AlbumID };

            return base.CreateEdit(song);
        }
    }
}
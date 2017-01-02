using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace MusicStore.Controllers
{
    public class AlbumController : BaseController<Album>
    {
        public AlbumController(MusicStoreContext musicStoreContext)
            :base(musicStoreContext)
        {
            Includes = new List<string>{ "Artist", "Genre", "Songs" };
            ListIncludes = new List<string>{"Artist"};

            OrderBy = (albums => albums.OrderBy(a => a.Artist.Name).ThenBy(a => a.YearRelease));
        }
        public override async Task<IActionResult> CreateEdit(int? id)
        {
            //add all the genres in the view bag
            ViewBag.ListGenres = await UnitOfWork.Repository<Genre>().Table.ToListAsync();
            ViewBag.ListArtists = await UnitOfWork.Repository<Artist>().Table.ToListAsync();
            //call the base class
                       
            return await base.CreateEdit(id);
        }
        [HttpGetAttribute]
        [RouteAttribute("Album/CreateEditByArtistId/{artistId}")]
        public async Task<IActionResult> CreateEditByArtistId(int artistId)
        {
            //add all the genres in the view bag
            ViewBag.ListGenres = await UnitOfWork.Repository<Genre>().Table.ToListAsync();
            ViewBag.ListArtists = await UnitOfWork.Repository<Artist>().Table.ToListAsync();

            Album album = new Album();
            album.ID = 0;
            album.ArtistID = artistId;

            return View(album);
       }

       [HttpPostAttribute]
       [ValidateAntiForgeryTokenAttribute]
       public IActionResult CreateEditByArtistId(Album album)
       {
            //redirect to the artist controller
            RedirectAfterDbMod = 
                new Helpers.RedirectObject{ Action = "Details", Controller = "Artist", ID = album.ArtistID };

            return base.CreateEdit(album);
       }
    }
}
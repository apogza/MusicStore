using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;
using System.Collections.Generic;

namespace MusicStore.Controllers
{
    public class AlbumController : BaseController<Album>
    {
        public AlbumController(MusicStoreContext musicStoreContext)
            :base(musicStoreContext)
        {
            Includes = new List<string>{ "Artist", "Genre", "Songs" };
        }

        [HttpGetAttribute]
        public override async Task<IActionResult> CreateEdit(int? id)
        {
            //add all the genres in the view bag
            ViewBag.ListGenres = await UnitOfWork.Repository<Genre>().Table.ToListAsync();
            ViewBag.ListArtists = await UnitOfWork.Repository<Artist>().Table.ToListAsync();
            //call the base class
            return await base.CreateEdit(id);
        }
        [HttpGetAttribute]
        public async Task<IActionResult> CreateEditByArtistId(int id)
        {
            //add all the genres in the view bag
            ViewBag.ListGenres = await UnitOfWork.Repository<Genre>().Table.ToListAsync();
            ViewBag.ListArtists = await UnitOfWork.Repository<Artist>().Table.ToListAsync();

            Album album = new Album();
            album.ID = 0;
            album.ArtistID = id;
            id = 0;

            return View(album);
        }
    }
}
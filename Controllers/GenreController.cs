using System.Collections.Generic;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class GenreController : BaseController<Genre>
    {
        public GenreController(MusicStoreContext context)
            :base(context)
        {
            Includes = new List<string>{ "Albums" };
        }
    }
}
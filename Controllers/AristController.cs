using System.Collections.Generic;
using MusicStore.Data;
using MusicStore.Models;

namespace MusicStore.Controllers
{
    public class ArtistController : BaseController<Artist>
    {
        public ArtistController(MusicStoreContext context)
            :base(context)
        {
            Includes = new List<string>{"Albums"};
        }
    }
}
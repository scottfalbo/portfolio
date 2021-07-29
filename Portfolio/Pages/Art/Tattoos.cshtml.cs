using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Art
{
    public class TattoosModel : PageModel
    {
        public IArtAdmin _artAdmin;
        public IAdmin _admin;

        public TattoosModel(IArtAdmin art, IAdmin admin)
        {
            _artAdmin = art;
            _admin = admin;
        }

        [BindProperty]
        public List<Tattoo> Tattoos { get; set; }
        [BindProperty]
        public List<Tattoo> AllTattoos { get; set; }
        [BindProperty]
        public int Limit { get; set; }
        [BindProperty]
        public bool Loadable { get; set; }
        [BindProperty]
        public bool IsLoaded { get; set; }
        public HomePage HomePage { get; set; }

        /// <summary>
        /// Get all of the tattoos from the database and displays the first 12
        /// </summary>
        public async Task OnGet()
        {
            IsLoaded = false;
            Limit = 0;
            await Paginate();
        }

        /// <summary>
        /// Loads the next 12 images in thumbnail view
        /// </summary>
        public async Task OnPostLoadMore()
        {
            IsLoaded = false;
            await Paginate();
        }

        /// <summary>
        /// Loads the next 12 images in open gallery view.
        /// Toggles on IsLoaded.  On reload gallery opens to proper index.
        /// </summary>
        /// <returns></returns>
        public async Task OnPostLoadFromCarousel()
        {
            IsLoaded = true;
            await Paginate();
        }

        /// <summary>
        /// Helper method to load the next 12 images in the gallery
        /// </summary>
        private async Task Paginate()
        {
            Loadable = true;
            Limit += 12;
            AllTattoos = await _artAdmin.GetTattoos();
            AllTattoos.Reverse();
            if (Limit < AllTattoos.Count())
                Tattoos = AllTattoos.Take(Limit).ToList();
            else
            {
                Limit = AllTattoos.Count();
                Tattoos = AllTattoos;
                Loadable = false;
            }
            HomePage = await _admin.GetHomePage("Tattoo");
            Redirect("/Art/Tattoos");
        }
    }
}

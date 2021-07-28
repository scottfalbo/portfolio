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
        public HomePage HomePage { get; set; }

        /// <summary>
        /// Get all of the tattoos from the database and displays the first 12
        /// </summary>
        public async Task OnGet()
        {
            Loadable = true;
            Limit = 12;
            AllTattoos = await _artAdmin.GetTattoos();
            AllTattoos.Reverse();
            if (Limit < AllTattoos.Count())
                Tattoos = AllTattoos.Take(Limit).ToList();
            else
            {
                Tattoos = AllTattoos;
                Loadable = false;
            }
            
            HomePage = await _admin.GetHomePage("Tattoo");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task OnPostLoadMore()
        {
            Loadable = true;
            Limit += 12;
            AllTattoos = await _artAdmin.GetTattoos();
            AllTattoos.Reverse();
            if (Limit < AllTattoos.Count())
                Tattoos = AllTattoos.Take(Limit).ToList();
            else
            {
                Tattoos = AllTattoos;
                Loadable = false;
            }

            HomePage = await _admin.GetHomePage("Tattoo");
            Redirect("/Art/Tattoos");
        }

    }
}

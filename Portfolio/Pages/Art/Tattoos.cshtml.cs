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

        public TattoosModel(IArtAdmin art)
        {
            _artAdmin = art;
        }

        [BindProperty]
        public List<Tattoo> Tattoos { get; set; }

        public async Task OnGet()
        {
            Tattoos = await _artAdmin.GetTattoos();
            AssignOrder();
        }

        /// <summary>
        /// Assigns display order to be used as index for thumbnail links to gallery view
        /// </summary>
        public void AssignOrder()
        {
            int order = 0;
            foreach(Tattoo tattoo in Tattoos)
            {
                tattoo.Order = order;
                order++;
            }
        }
    }
}

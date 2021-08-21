using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.SecretGame.Models.Interfaces;

namespace Portfolio.Pages.Code
{
    public class SecretGameModel : PageModel
    {
        public IGameMaster _game;

        public SecretGameModel(IGameMaster game)
        {
            _game = game;
        }

        public void OnGet()
        {

        }
    }
}

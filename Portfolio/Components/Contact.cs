using Microsoft.AspNetCore.Mvc;
using Portfolio.Email.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Components
{
    [ViewComponent]
    public class ContactViewComponent : ViewComponent
    {
        private readonly IEmail _email;

        public ContactViewComponent(IEmail email)
        {
            _email = email;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewModel vm = new ViewModel()
            {
                Hello = "hello world"
            };
            return View(vm);
        }

        public class ViewModel
        {
            public string Hello { get; set; }
        }
    }



}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models.Interface;

namespace Portfolio.Pages.Admin
{
    public class TattooLairModel : PageModel
    {
        public IArtAdmin _adminContext;
        public IUploadService _uploadService;

        public TattooLairModel(IArtAdmin context, IUploadService upload)
        {
            _adminContext = context;
            _uploadService = upload;
        }

        public void OnGet()
        {
        }
    }
}

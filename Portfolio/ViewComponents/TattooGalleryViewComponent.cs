using Microsoft.AspNetCore.Mvc;
using Portfolio.Components.GalleryDataStructures;
using Portfolio.Models;
using Portfolio.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.ViewComponents
{
    public class TattooGalleryViewComponent : ViewComponent
    {
        public IArtAdmin _adminContext;

        public TattooGalleryViewComponent(IArtAdmin admin)
        {
            _adminContext = admin;
            Gallery = new Gallery<Tattoo>();
            Current = new Image<Tattoo>();
        }

        public List<Tattoo> Tattoos { get; set; }
        public Gallery<Tattoo> Gallery { get; set; }
        public Image<Tattoo> Current { get; set; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            Tattoos = await _adminContext.GetTattoos();
            MakeGallery();

            ViewModel vm = new ViewModel()
            {
                Gallery = Gallery,
                Current = Current
            };

            return View(vm);
        }

        public void MakeGallery()
        {
            foreach (Tattoo tattoo in Tattoos)
                Gallery.Insert(tattoo);
            Current = Gallery.Head;
        }
    }

    public class ViewModel
    {
        public Gallery<Tattoo> Gallery { get; set; }
        public Image<Tattoo> Current { get; set; }
    }
}

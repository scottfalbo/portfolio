using Portfolio.Components.GalleryDataStructures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PortfolioTests
{
    public class GalleryStructTests
    {
        [Fact]
        public void Gallery_Can_Add_Images_Objects()
        {
            Gallery<int> testGallery = CreateGallery();
            int expected = 5;
            int actual = testGallery.Counter;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Gallery_Head_Is_Correct()
        {
            Gallery<int> testGallery = CreateGallery();
            int expected = 5;
            int actual = testGallery.Head.Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Gallery_Tail_Is_Correct()
        {
            Gallery<int> testGallery = CreateGallery();
            int expected = 1;
            int actual = testGallery.Tail.Value;
            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Helper method to instantiate a Gallary object
        /// </summary>
        /// <returns> Populated Gallery Object </returns>
        private Gallery<int> CreateGallery()
        {
            Gallery<int> newGallery = new Gallery<int>();
            newGallery.Insert(1);
            newGallery.Insert(2);
            newGallery.Insert(3);
            newGallery.Insert(4);
            newGallery.Insert(5);
            return newGallery;
        }
    }
}

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
        public void Can_You_Even_Add_Image_Objects_Bro()
        {
            Gallery<int> testGallery = CreateGallery();
            int expected = 5;
            int actual = testGallery.Counter;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Is_Your_Head_Even_Correct_Bro()
        {
            Gallery<int> testGallery = CreateGallery();
            int expected = 5;
            int actual = testGallery.Head.Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Is_Your_Tail_Even_Correct_Bro()
        {
            Gallery<int> testGallery = CreateGallery();
            int expected = 1;
            int actual = testGallery.Tail.Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Do_You_Even_Previous_Bro()
        {
            Gallery<int> testGallery = CreateGallery();
            int expected = 2;
            int actual = testGallery.Tail.Prev.Value;
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Do_You_Even_Enumerate_Bro()
        {
            Gallery<int> testGallery = CreateGallery();
            int expected = 15;
            int actual = 0;
            foreach (var item in testGallery)
                actual += item;
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

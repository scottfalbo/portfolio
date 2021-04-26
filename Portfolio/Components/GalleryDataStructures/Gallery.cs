using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Components.GalleryDataStructures
{
    public class Gallery<T> : IEnumerable<T>
    {
        public Image<T> Head { get; set; }
        public Image<T> Tail { get; set; }
        public int Counter { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Gallery() { }

        /// <summary>
        /// Constructor to instantiate a gallery with an Image object of T value
        /// </summary>
        /// <param name="value"> T value </param>
        public Gallery(T value)
        {
            Image<T> image = new Image<T>(value);
            Head = image;
            Tail = image;
            Counter = 1;
        }

        /// <summary>
        /// Counstructor to instantiate a gallery with Image object
        /// </summary>
        /// <param name="image"></param>
        public Gallery(Image<T> image)
        {
            Head = image;
            Tail = image;
            Counter = 1;
        }


        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}

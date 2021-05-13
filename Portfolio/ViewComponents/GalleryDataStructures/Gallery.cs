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

        /// <summary>
        /// Instantiate and insert a new Image object into the front of the Gallery
        /// </summary>
        /// <param name="value"></param>
        public void Insert (T value)
        {
            Image<T> image = new Image<T>(value);

            if (Head == null)
            {
                Head = image;
                Tail = Head;
            }
            else
            {
                image.Next = Head;
                image.Next.Prev = image;
                Head = image;
            }
            Counter++;
        }

        /// <summary>
        /// Method to make the Gallery enumerable
        /// </summary>
        /// <returns> IEnumerator of Gallery </returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (Counter == 0) yield break;

            Image<T> current = Head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

}

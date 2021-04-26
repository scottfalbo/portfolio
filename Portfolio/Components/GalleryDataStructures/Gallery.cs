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

        // delete image


        public IEnumerator<T> GetEnumerator() => new GalleryEnumerator<T>(Head);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class  GalleryEnumerator<T> : IEnumerator<T>
    {
        private Image<T> current;

        public GalleryEnumerator(Image<T> current)
        {
            this.current = current;
        }

        public T Current => current.Value;
        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (current == null) return false;
            current = current.Next;
            return (current != null);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

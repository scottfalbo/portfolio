using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Components.GalleryDataStructures
{
    public class Image<T>
    {
        public T Value { get; set; }
        public Image<T> Next { get; set; }
        public Image<T> Prev { get; set; }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public Image() { }

        /// <summary>
        /// Constructor to create an Image with a value of type T
        /// </summary>
        /// <param name="value"> Image value </param>
        public Image(T value)
        {
            Value = value;
        }

    }
}

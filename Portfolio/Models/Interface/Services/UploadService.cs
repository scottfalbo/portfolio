using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Portfolio.Data;
using System;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Portfolio.Models.Interface.Services
{
    public class UploadService : IUploadService
    {
        public IConfiguration Configuration { get; }
        public IAdmin _admin;
        public IArtAdmin _artAdmin;
        private readonly PortfolioDbContext _context;

        public UploadService(IConfiguration config, IAdmin admin, PortfolioDbContext context, IArtAdmin art)
        {
            Configuration = config;
            _admin = admin;
            _context = context;
            _artAdmin = art;
        }

        /// <summary>
        /// Uploads the image to azure blob storage
        /// </summary>
        /// <param name="file"> file to upload </param>
        /// <returns> new BlobClient object </returns>
        public async Task<BlobClient> UploadImage(IFormFile file)
        {
            BlobContainerClient container = new BlobContainerClient(Configuration["ImageBlob"], "images");

            await container.CreateIfNotExistsAsync();

                BlobClient blob = container.GetBlobClient(file.FileName);

                using var stream = file.OpenReadStream();

                BlobUploadOptions options = new BlobUploadOptions()
                {
                    HttpHeaders = new BlobHttpHeaders() { ContentType = file.ContentType }
                };

                if (!blob.Exists())
                    await blob.UploadAsync(stream, options);

                return blob;
        }

        /// <summary>
        /// Overload method for uploading resized images that are already Streams
        /// </summary>
        /// <param name="file"> IFormFile file </param>
        /// <param name="stream"> Stream stream </param>
        /// <returns> BlobClient blob </returns>
        public async Task<BlobClient> UploadImage(Stream stream, string filename, string contentType)
        {
            filename = AugmentFileName(filename);
            BlobContainerClient container = new BlobContainerClient(Configuration["ImageBlob"], "images");

            await container.CreateIfNotExistsAsync();
            BlobClient blob = container.GetBlobClient(filename);

            BlobUploadOptions options = new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders() { ContentType = contentType }
            };

            if (!blob.Exists())
                await blob.UploadAsync(stream, options);
            return blob;
        }

        /// <summary>
        /// Helper method that adds the time and date to the end of the filename to ensure it is unique.
        /// </summary>
        /// <param name="file"> string filename </param>
        /// <returns> augmented filename </returns>
        private string AugmentFileName(string file)
        {
            string timeStamp = DateTime.Now.ToString();
            timeStamp = Regex.Replace(timeStamp, "[^0-9]", "");

            string pattern = @"[^.]+$";
            string fileType = Regex.Match(file, pattern).ToString();

            file = Regex.Replace(file, $@"\b.{fileType}\b", "");
            file = file.Replace(" ", String.Empty);
            return file + $"{timeStamp}.{fileType}";
        }

        /// <summary>
        /// Resizes the gallery image and creates a second thumbnail file.
        /// Both images are uploaded to the blob and the returned Uri assigned to the object.
        /// </summary>
        /// <param name="file"> IFormFile from form input </param>
        /// <returns> new Image object </returns>
        public async Task<Image> AddArtImage(IFormFile file)
        {
            Stream stream = ResizeImage(file, 1900);
            BlobClient blob = await UploadImage(stream, file.FileName, file.ContentType);

            string thumbFile = ThumbNailFileName(file.FileName);

            Stream thumbStream = ResizeImage(file, 100);
            BlobClient thumb = await UploadImage(thumbStream, thumbFile, file.ContentType);

            Image image = new Image()
            {
                ImageURL = blob.Uri.ToString(),
                FileName = file.FileName,
                ThumbURL = thumb.Uri.ToString(),
                ThumbFileName = thumbFile,
                Order = 0
            };
            return await _artAdmin.CreateImage(image);
        }

        /// <summary>
        /// Helper method to insert "_thumb" before the file extension
        /// </summary>
        /// <param name="file"> string filename </param>
        /// <returns> string filename + _thumb </returns>
        private string ThumbNailFileName(string file)
        {
            string pattern = @"[^.]+$";
            string fileType = Regex.Match(file, pattern).ToString();
            string thumb = Regex.Replace(file, $@"\b.{fileType}\b", "");
            return $"{thumb}_thumb.{fileType}";
        }

        /// <summary>
        /// Resizes the project portfolio image and uploads to the blob.
        /// Assigns the returned Uri to the object.
        /// </summary>
        /// <param name="file"> IFormFile from form input </param>
        /// <returns> new Image object </returns>
        public async Task<Image> AddProjectImage(IFormFile file)
        {
            Stream stream = ResizeImage(file, 400);
            BlobClient blob = await UploadImage(stream, file.FileName, file.ContentType);

            Image image = new Image()
            {
                ImageURL = blob.Uri.ToString(),
                FileName = file.FileName,
                ThumbURL = "",
                ThumbFileName = "",
                Order = 0
            };
            return await _artAdmin.CreateImage(image);
        }

        /// <summary>
        /// Create Image object from the upload file.
        /// Resize for max gallery height or thumbnail based on n parameter.
        /// Save the updated Image to a Stream for upload to blob.
        /// </summary>
        /// <param name="file"> IFormFile from form </param>
        /// <param name="n"> height </param>
        /// <returns> Steam of resized Image </returns>
        private Stream ResizeImage(IFormFile file, int n)
        {
            using var image = SixLabors.ImageSharp.Image.Load(file.OpenReadStream());
            var stream = new MemoryStream();

            int width = FindWidth(image.Width, image.Height, n);
            image.Mutate(x => x.Resize(width, n));

            switch (file.ContentType)
            {
                case "image/jpeg":
                    image.SaveAsJpeg(stream);
                    break;
                case "image/png":
                    image.SaveAsPng(stream);
                    break;
                case "image/bmp":
                    image.SaveAsBmp(stream);
                    break;
                case "image/gif":
                    image.SaveAsGif(stream);
                    break;
                default:
                    throw new Exception("invalud file type");
            }
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Helper function to find width relative to new height.
        /// </summary>
        /// <param name="width"> image width </param>
        /// <param name="height"> image height </param>
        /// <param name="n"> new height </param>
        /// <returns> int new width </returns>
        private int FindWidth (int width, int height, int n)
        {
            float ratio = (float)height / (float)n;
            int newWidth = Convert.ToInt32((float)width / ratio);
            return newWidth;
        }

        /// <summary>
        /// Updates the HomePage selfie to new image
        /// </summary>
        /// <param name="file"> input file </param>
        /// <param name="id"> homepage id </param>
        public async Task UpdateSelfie(IFormFile file, int id)
        {
            HomePage homepage = await _context.HomePage.FindAsync(id);

            Stream stream = ResizeImage(file, 400);
            BlobClient blob = await UploadImage(stream, file.FileName, file.ContentType);

            homepage.Selfie = blob.Uri.ToString();
            homepage.FileName = file.FileName;
            await _admin.UpdateHomePage(homepage);
        }
    }
}

using Ecom.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries.service
{
    public class ImageManagementService : IimageManagementSerives
    {
        private readonly IFileProvider fileProvider;

        public ImageManagementService(IFileProvider fileProvider)
        {
            this.fileProvider = fileProvider;
        }
       
        
        
        
        
        public async Task<List<string>> AddImageAsync(IFormFileCollection file, string src)
        {
            List<string> SaveImageSrc = new List<string>();
            var ImageDirectory = Path.Combine("wwwroot", "Images", src);

            if (!Directory.Exists(ImageDirectory))
            {
               Directory.CreateDirectory(ImageDirectory);
            }
           
            foreach (var item in file)
            {
                if (item.Length > 0)
                {


                    var imageName = item.FileName;
                    var imageSrc = $"/Images/{src}/{imageName}";
                    var root = Path.Combine(ImageDirectory, imageName);
                        
                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await item.CopyToAsync(stream);

                    }

                    SaveImageSrc.Add(imageName);
                }
            }
            
           return SaveImageSrc;
        }

      

        public void DeleteImageAsync(string imageName, string folderName)
        {
            var imagePath = Path.Combine("wwwroot", "Images", folderName, imageName);

            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
        }




        // old update

        //public void DeleteImageAsync(string src)
        //{
        //    var info = fileProvider.GetFileInfo(src);
        //    var root = info.PhysicalPath;
        //    File.Delete(root);
        //}


    }



}




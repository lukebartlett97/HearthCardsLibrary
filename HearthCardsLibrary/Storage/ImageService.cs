using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HearthCardsLibrary.Storage
{
    class ImageService
    {
        private static ImageService instance;

        private const string ImagePath = "CardImages";

        public static ImageService GetInstance()
        {
            if (instance == null)
            {
                instance = new ImageService();
            }
            return instance;
        }

        public string GetImagePath(string fileName)
        {
            return Path.Combine(ImagePath, fileName + ".png");
        }

        public void InitialiseFolder()
        {
            if (!Directory.Exists(ImagePath))
            {
                Directory.CreateDirectory(ImagePath);
            }
        }
    }
}

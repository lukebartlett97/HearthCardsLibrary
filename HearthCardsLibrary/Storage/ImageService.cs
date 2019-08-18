using Microsoft.Win32;
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
        public static ImageService Instance {
            get
            {
                return instance ?? (instance = new ImageService());
            }
        }

        private const string ImagePath = "CardImages";

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

        public byte[] GetImage(string path)
        {
            return File.ReadAllBytes(path);
        }

        public void ExportImage(byte[] image)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PNG Image|*.png";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                File.WriteAllBytes(saveFileDialog1.FileName, image);
            }
        }
    }
}

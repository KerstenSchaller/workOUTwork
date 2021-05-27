using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wow
{
    public class ScreenImageComposer
    {
        private Screen[] screens;
        private Screen PrimaryScreen;

        public ScreenImageComposer() 
        {
            screens = Screen.AllScreens;
            foreach (Screen screen in screens) 
            {
                if (screen.Primary == true) 
                {
                    PrimaryScreen = screen;
                }
            }
        }

        public Image createScreenImageWithText(int xpos, int ypos, String text, Color textColor, Color backColor, string filename = "")
        {
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               40,
               FontStyle.Regular,
               GraphicsUnit.Pixel);

            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap(PrimaryScreen.Bounds.Width, PrimaryScreen.Bounds.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, xpos, ypos);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            Image dilbertImage = addDilbertComic(img);

            if (filename != "") 
            { 
                new Bitmap(dilbertImage).Save(filename);
            }
            return img;
        }

        public Image addDilbertComic(Image backgroundImage) 
        {
            DilbertComicDownloader dilbertComicDownloader = new DilbertComicDownloader();
            var dilbertComic = dilbertComicDownloader.getDilbertComicImageByDate(DateTime.Now.Date);

            int w = dilbertComic.Width;
            int h = dilbertComic.Height;
            float m = 500f/w;
            int newHeight = (int)(h * m);

            Image embeddedImage = embeddImage(backgroundImage, dilbertComic, 50,50,500, newHeight);
            return embeddedImage;
        }

        public Image embeddImage(Image backImg, Image frontImage,int xpos, int ypos, int width, int height) 
        {

            Graphics g = Graphics.FromImage(backImg);
            var resizedfrontImage = ResizeImage(frontImage, width, height);
            g.DrawImage(frontImage, xpos, ypos);
            return backImg;
        }

        private static Bitmap ResizeImage(Image image, int width, int height)
        {
            // function taken from: https://stackoverflow.com/questions/1922040/how-to-resize-an-image-c-sharp
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wow
{
    public interface IScreenWidget
    {
        Image addSelfToBackground(Image image);
    }

    class TextWidget : IScreenWidget
    {
        string text;
        Color color;

        public string Text{ set{ text = value; } }

        public Color Textcolor { set { color = value; } }

        public Image addSelfToBackground(Image image)
        {
            return createScreenImageWithText(image,  0, 0, this.text, color, "tempLockScreen2.jpg");
        }

        private Image createScreenImageWithText(Image backgroundImage, int xpos, int ypos, String text, Color textColor, string filename = "")
        {
            FontFamily fontFamily = new FontFamily("Arial");
            Font font = new Font(
               fontFamily,
               40,
               FontStyle.Regular,
               GraphicsUnit.Pixel);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);
            var img = backgroundImage;

            Graphics drawing = Graphics.FromImage(img);
            drawing.DrawString(text, font, textBrush, xpos, ypos);

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }

    class DilbertWidget : IScreenWidget
    {
        public Image addSelfToBackground(Image image)
        {
            return addDilbertComic(image);
        }

        public Image addDilbertComic(Image backgroundImage)
        {
            DilbertComicDownloader dilbertComicDownloader = new DilbertComicDownloader();
            var dilbertComic = dilbertComicDownloader.getDilbertComicImageByDate(DateTime.Now.Date);

            int w = dilbertComic.Width;
            int h = dilbertComic.Height;
            float m = 500f / w;
            int newHeight = (int)(h * m);

            Image embeddedImage = ScreenImageComposer.embeddImage(backgroundImage, dilbertComic, 50, 50, 500, newHeight);
            return embeddedImage;
        }
    }


}

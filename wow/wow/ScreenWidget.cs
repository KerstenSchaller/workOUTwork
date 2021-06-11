using System;
using System.Drawing;

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
        int xPos = 0;
        int yPos = 0;
        int xSize = 500;

        string xPosString = "dilbert_Widget_X_Position";
        string yPosString = "dilbert_Widget_Y_Position";
        string xSizeString = "dilbert_Widget_X_Size";

        public DilbertWidget()
        {
            new Configuration().addConfigEntry(xPosString, 50);
            new Configuration().addConfigEntry(yPosString, 50);
            new Configuration().addConfigEntry(xSizeString, 1);
            xPos = Int32.Parse(new Configuration().getValueString(xPosString));
            yPos = Int32.Parse(new Configuration().getValueString(yPosString));
            xSize = Int32.Parse(new Configuration().getValueString(xSizeString));
        }
        public Image addSelfToBackground(Image image)
        {
            return addDilbertComic(image);
        }

        public Image addDilbertComic(Image backgroundImage)
        {
            DilbertComicDownloader dilbertComicDownloader = new DilbertComicDownloader();
            var dilbertComic = dilbertComicDownloader.getDilbertComicImageByDate(DateTime.Now.Date);

            float width = (float)dilbertComic.Width;
            float height = (float)dilbertComic.Height;
            float ratio = xSize / width;
            int newHeight = (int)(height * ratio);

            Image embeddedImage = ScreenImageComposer.embeddImage(backgroundImage, dilbertComic, xPos, yPos, xSize, newHeight);
            return embeddedImage;
        }
    }


}

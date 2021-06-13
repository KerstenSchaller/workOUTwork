using System;
using System.Collections.Generic;
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

    class DilbertWidget : Configurable , IScreenWidget
    {
        ConfigIntParameter xPosParam = new ConfigIntParameter("dilbert_Widget_X_Position", 50);
        ConfigIntParameter yPosParam = new ConfigIntParameter("dilbert_Widget_Y_Position", 50);
        ConfigIntParameter xSizeParam = new ConfigIntParameter("dilbert_Widget_X_Size", 1000);

        public DilbertWidget() : base("xDilbertWidget")
        {         
            base.parameters.Add(xPosParam);
            base.parameters.Add(yPosParam);
            base.parameters.Add(xSizeParam);
            this.setParameters(parameters);
        }
        public Image addSelfToBackground(Image image)
        {
            return addDilbertComic(image);
        }

        public Image addDilbertComic(Image backgroundImage)
        {
            DilbertComicDownloader dilbertComicDownloader = new DilbertComicDownloader();
            var dilbertComic = dilbertComicDownloader.getDilbertComicImageByDate(DateTime.Now.Date);

            int newWidth = xSizeParam.getValue();
            float width = (float)dilbertComic.Width;
            float height = (float)dilbertComic.Height;
            //float ratio = xSizeParam.getValue() / width;
            //int newHeight = (int)(height * ratio);

            float ratio = height / width;
            int newHeight = (int)(newWidth * ratio);
            Image embeddedImage = ScreenImageComposer.embeddImage(backgroundImage, dilbertComic, xPosParam.getValue(), yPosParam.getValue(), newWidth, newHeight);
            return embeddedImage;
        }


    }


}

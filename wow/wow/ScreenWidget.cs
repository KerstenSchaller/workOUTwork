using System;
using System.Collections.Generic;
using System.Drawing;

namespace wow
{
    public abstract class ScreenWidget
    {
        protected ConfigIntParameter xPosParam = new ConfigIntParameter("Widget_X_Position", 50);
        protected ConfigIntParameter yPosParam = new ConfigIntParameter("Widget_Y_Position", 50);
        protected ConfigIntParameter xSizeParam = new ConfigIntParameter("Widget_X_Size", 1000);
        protected ConfigIntParameter ySizeParam = new ConfigIntParameter("Widget_Y_Size", 500);
        ConfigContainer configContainer;

        protected ScreenWidget(string ConfigObjectName) 
        {
            ConfigContainer configContainer = new ConfigContainer(ConfigObjectName);
            List<ConfigParameter> parameters = new List<ConfigParameter>();
            parameters.Add(xPosParam);
            parameters.Add(yPosParam);
            parameters.Add(xSizeParam);
            parameters.Add(ySizeParam);
            configContainer.setParameters(parameters);
        }

        public abstract Image addSelfToBackground(Image image);
    }

    class TextWidget : ScreenWidget
    {
        string text;
        Color color;
        static int id = 0;

        public TextWidget():base("TextWidget" + id.ToString()) 
        {

        }
        public string Text{ set{ text = value; } }

        public Color Textcolor { set { color = value; } }

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

        public override Image addSelfToBackground(Image image)
        {
            return createScreenImageWithText(image, 0, 0, this.text, color, "tempLockScreen2.jpg");
        }
    }

    class DilbertWidget : ScreenWidget
    {


        public DilbertWidget() : base("DilbertWidget")
        {

        }

        public Image addDilbertComic(Image backgroundImage)
        {
            DilbertComicDownloader dilbertComicDownloader = new DilbertComicDownloader();
            var dilbertComic = dilbertComicDownloader.getDilbertComicImageByDate(DateTime.Now.Date);

            int newWidth = base.xSizeParam.getValue();
            float width = (float)dilbertComic.Width;
            float height = (float)dilbertComic.Height;
            //float ratio = xSizeParam.getValue() / width;
            //int newHeight = (int)(height * ratio);

            float ratio = height / width;
            int newHeight = (int)(newWidth * ratio);
            Image embeddedImage = ScreenImageComposer.embeddImage(backgroundImage, dilbertComic, xPosParam.getValue(), yPosParam.getValue(), newWidth, newHeight);
            return embeddedImage;
        }

        public override Image addSelfToBackground(Image image)
        {
            return addDilbertComic(image);
        }
    }


}

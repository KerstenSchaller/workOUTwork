using System;
using System.Collections.Generic;
using System.Drawing;
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

            if (filename != "") 
            { 
                new Bitmap(img).Save(filename);
            }
            return img;
        }
}
}

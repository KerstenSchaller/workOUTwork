using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace wow
{
    public class ScreenImageComposer
    {
        private Screen[] screens;
        private Screen PrimaryScreen;

        private List<ScreenWidget> widgets = new List<ScreenWidget>();

        private static ScreenImageComposer instance = null;
        private static readonly object padlock = new object();
        public static ScreenImageComposer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ScreenImageComposer();
                    }
                    return instance;
                }
            }
        }

        public void attachWidget(ScreenWidget widget) 
        {
            widgets.Add(widget);
        }

        private ScreenImageComposer() 
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

        public Image getBackgroundImage(Color color) 
        {
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
            drawing.Clear(color);
            drawing.Save();
            drawing.Dispose();

            return img;
        }

        public Image getScreenImage() 
        {
            var img = getBackgroundImage(Color.Black);
            foreach (ScreenWidget widget in widgets) 
            {
                img = widget.addSelfToBackground(img);
            }
            return img;
        }


        public static Image embeddImage(Image backImg, Image frontImage,int xpos, int ypos, int width, int height) 
        {
            Graphics g = Graphics.FromImage(backImg);
            var resizedfrontImage = ResizeImage(frontImage, width, height);
            g.DrawImage(resizedfrontImage, xpos, ypos , width,height);
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

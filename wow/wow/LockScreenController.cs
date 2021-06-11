using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.System.UserProfile;
using System.Drawing;

namespace wow
{
    class LockScreenController
    {
        private LockScreenController() 
        {
        }

        private static LockScreenController instance = null;
        private static readonly object padlock = new object();
        public static LockScreenController Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new LockScreenController();
                    }
                    return instance;
                }
            }
        }


        public async Task setLockScreen(string filename)
        {
            string directory = Directory.GetCurrentDirectory();
            string path = Path.Combine(directory, filename);
            StorageFile imageFile = await StorageFile.GetFileFromPathAsync(path);
            await LockScreen.SetImageFileAsync(imageFile);
        }

        private async Task setBackground(Color color)
        {

            Image img = ScreenImageComposer.Instance.getScreenImage();
            string filename = "tempLockScreen.jpg";
            new Bitmap(ScreenImageComposer.Instance.getBackgroundImage(color)).Save(filename);
            await setLockScreen(filename);
            File.Delete(filename);

        }

        public async Task setInformationtalLockscreen()
        {

            Image img = ScreenImageComposer.Instance.getScreenImage();
            string filename = "tempLockScreen.jpg";
            new Bitmap(img).Save(filename);
            await setLockScreen(filename);
            File.Delete(filename);
            
        }


    }
}

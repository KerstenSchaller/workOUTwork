using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.System.UserProfile;
using System.Windows.Forms;
using System.Drawing;

namespace wow
{
    class IndividualLockScreen
    {
        private static IndividualLockScreen instance = null;
        private static readonly object padlock = new object();

        private IndividualLockScreen() { }

        public static IndividualLockScreen Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new IndividualLockScreen();
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

        public async Task setInformationtalLockscreen(string text, Color textColor)
        {
            ScreenImageComposer screenImageComposer = new ScreenImageComposer();
            screenImageComposer.createScreenImageWithText(0, 0, text, textColor, Color.Black, "tempLockScreen2.jpg");
            await setLockScreen("tempLockScreen2.jpg");
            
        }


    }
}

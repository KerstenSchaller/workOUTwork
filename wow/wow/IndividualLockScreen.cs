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

        public async Task setLockScreen(string filename)
        {
            string directory = Directory.GetCurrentDirectory();
            string path = Path.Combine(directory, filename);
            StorageFile imageFile = await StorageFile.GetFileFromPathAsync(path);
            await LockScreen.SetImageFileAsync(imageFile);
        }

        public async void setInformationtalLockscreen(string text, Color textColor)
        {
            ScreenImageComposer screenImageComposer = new ScreenImageComposer();
            screenImageComposer.createScreenImageWithText(0, 0, text, textColor, Color.Black, "tempLockScreen.jpg");
            await setLockScreen("tempLockScreen.jpg");
            File.Delete("tempLockScreen.jpg");
        }


    }
}

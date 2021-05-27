using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.System.UserProfile;
using System.Windows.Forms;

namespace wow
{
    class IndividualLockScreen
    {
        Timer timer = new Timer();
        bool state = true;

        public IndividualLockScreen()
        {
            //string filename = "IMG_5196.jpg";
            string filename = "IMG_5196_2.jpg";
            var t = setLockScreen(filename);

            timer.Interval = 5 * 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (state)
            {
                var t = setLockScreen("IMG_5196.jpg");
                timer.Start();
                state = false;
            }
            else
            {
                var t = setLockScreen("IMG_5196_2.jpg");
                timer.Start();
                state = true;
            }    
        }

        public async Task setLockScreen(string filename)
        {
            string directory = Directory.GetCurrentDirectory();
            string path = Path.Combine(directory, filename);
            StorageFile imageFile = await StorageFile.GetFileFromPathAsync(path);
            await LockScreen.SetImageFileAsync(imageFile);
        }


    }
}

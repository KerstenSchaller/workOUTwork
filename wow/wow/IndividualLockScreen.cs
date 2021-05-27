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
        int loopCounter = 0;
        string[] files;

        public IndividualLockScreen()
        {


            timer.Interval = 5 * 1000;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            lockScreenSlideShow(@"C:\Users\kerst\Desktop\bouldern_dez2020\ausgewählt\einzeön");
            timer.Start();
        }

        public void lockScreenSlideShow(string path) 
        {
            files = loadImagesFromFolder(path);
            setLockScreen(files[loopCounter]);
            if (loopCounter < (files.Length - 1))
            {
                loopCounter++;
            }
            else
            {
                loopCounter = 0;
            }
        }

        public async Task setLockScreen(string filename)
        {
            string directory = Directory.GetCurrentDirectory();
            string path = Path.Combine(directory, filename);
            StorageFile imageFile = await StorageFile.GetFileFromPathAsync(path);
            await LockScreen.SetImageFileAsync(imageFile);
        }

        public string[] loadImagesFromFolder(string path) 
        {
            return Directory.GetFiles(path);
        }


    }
}

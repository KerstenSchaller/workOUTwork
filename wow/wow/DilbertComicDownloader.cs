using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace wow
{
    class DilbertComicDownloader : WebClient
    {
        string httpContent = "";
        string ImageUrl = "";
        List<string> all_tags = new List<string>();

        public bool AcceptAllCertifications(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certification, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        public Image getDilbertComicImageByDate(DateTime date)
        {
            Image image;
            string url = getDilbertComícUrlByDate(date);
            image = GetImageFromURL(url);
            return image;

        }


        private string getDilbertComícUrlByDate(DateTime date)
        {
            string adress = "https://dilbert.com/strip/";
            adress += date.Year;
            adress += "-";
            adress += (date.Month < 10) ? "0" : "";// prepend 0 if needed
            adress += date.Month;
            adress += "-";
            adress += (date.Day < 10) ? "0" : "";// prepend 0 if needed
            adress += date.Day;
            this.getHtml(adress);
            return this.getImageUrl();
        }

        public string getHtml(string address)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            //Use SecurityProtocolType.Ssl3 if needed for compatibility reasons

            //Use callback for https connection certificate acception
            ServicePointManager.ServerCertificateValidationCallback +=
                delegate (object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslError)
                {
                    bool validationResult = true;
                    return validationResult;
                };


            Uri uri = new Uri(address);
            WebRequest request = GetWebRequest(uri);
            WebResponse response = request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
            string result = sr.ReadToEnd();
            httpContent = result;
            return result;
        }



        private Image GetImageFromURL(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream stream = httpWebReponse.GetResponseStream();
            return Image.FromStream(stream);
        }

        public string getImageUrl()
        {
            string UrlStartstring = "https://assets.amuniversal";

            int startIndex = httpContent.IndexOf(UrlStartstring);
            int endindex = httpContent.IndexOf("\"/>\n", startIndex);

            int startindexUrl = startIndex;
            int endindexUrl = endindex;
            int length = endindexUrl - startIndex;
            string imageUrl = httpContent.Substring(startindexUrl, length);
            return imageUrl;
        }





        protected override WebRequest GetWebRequest(Uri uri)
        {
            HttpWebRequest request = base.GetWebRequest(uri) as HttpWebRequest;
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            return request;
        }


    }
}

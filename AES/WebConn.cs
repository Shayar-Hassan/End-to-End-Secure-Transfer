using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace AES
{
    class WebConn
    {

        public static void fileDownload(String filename)
        {
            string url = "http://localhost/shayar/files/";

            WebClient mwc = new WebClient();
            string url2 = url + filename;

            mwc.DownloadFile(url2, "d:\\aes_tmp\\" + filename);
        }

        public static string fileUpload(String filename)
        {
            string url = "http://localhost/shayar/uploadyyy.php";
            //            string url = "http://localhost/shayar/u.php";

            WebClient mwc = new WebClient();
            string url2 = url + filename;

            byte[] b = mwc.UploadFile(url, filename);

            string s = "";
            for (int i = 0; i < b.Length; i++)
            {
                s = s + (char)b[i];
            }
            return s;
        }


    }
}

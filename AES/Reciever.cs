using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace AES
{
    public partial class Reciever : Form
    {
        public Reciever()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            string fileCode = textBox1.Text;
            string password = textBox2.Text;
            string fout = saveFileDialog1.FileName;
            string tempFile = "d:\\aes_tmp\\" + fileCode;
            
            Block.setCipherKey(password);
            WebConn.fileDownload(fileCode);
            Encryption.writeFile(tempFile, fout);
            string msg = "download Code:   " + fileCode + "\n";
            msg = msg + "password   :     " + password;
            MessageBox.Show(msg);
        }

        /*
        private void button1_Click(object sender, EventArgs e)
        {
        
            string mainURL = "http://localhost/shayar/check_file.php?code=";
            string code = textBox1.Text;
            string state = "9999999";
            using (WebClient client = new WebClient())
            {
                //client.DownloadFile("", "");
                state = client.DownloadString(mainURL + code);
            }
            MessageBox.Show("-" + state + "-");
            if (state == "0")
                MessageBox.Show("error");
            else
            {
                downloadFile(code);
                //MessageBox.Show("DL");
            }
        }
        */
    }
}

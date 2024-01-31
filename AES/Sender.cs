using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AES
{
    public partial class Sender : Form
    {
        public Sender()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Block.setCipherKey(textBox1.Text);
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            String fin = openFileDialog1.FileName;
            string fout = openFileDialog1.FileName + ".aes";

            Encryption.readFile(fin, fout);
            string fileCode = WebConn.fileUpload(fout);
            string password = Block.getCipherKey();
            string msg = "download Code:   " + fileCode + Environment.NewLine;
            msg = msg + "password   :     " + password;

            textBox2.Visible = true;
            textBox2.Text = msg;
            //MessageBox.Show(msg);

        }
    }
}

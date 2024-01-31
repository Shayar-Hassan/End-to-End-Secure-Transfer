using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Numerics;

namespace AES
{
    class Encryption
    {
        char[] txtFile = new char[512];
        public void openFile()
        {
            for (int i = 0; i < txtFile.Length; i++)
            {
                txtFile[i] = 'd';
            }

            FileStream fs = new FileStream("Encryption.txt", FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);
            for (int i = 0; i < txtFile.Length; i++)
            {
                w.Write(txtFile[i]);
            }

            w.Flush();
            w.Close();
            fs.Close();

        }

        public void closeFile()
        {
            byte[,] array = new byte[4, 4];
            FileStream fs = new FileStream("ENCRYPTION.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            Console.WriteLine(sr.ReadToEnd());
            fs.Position = 0;
            BinaryReader br = new BinaryReader(fs);
            while (sr.Read(txtFile, 0, 16) > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        array[i, j] = (byte)txtFile[i];
                    }
                }
            }

            Block block = new Block();
            block.encrypt(array);
            fs.Close();


            FileStream fs2 = new FileStream("ENCRYPTED", FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs2);

            for (int k = 0; k < 4; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    w.Write(array[k, j]);
                }

            }

            w.Flush();
            w.Close();
            fs2.Close();
        }
        public static byte[,] array = new byte[4, 4];
        public static byte[,] array2 = new byte[4, 4];

        public static void readFile(string fileName_org, string fileName_enc)
        {
            Block block = new Block();
            //string fileName_org = "d:\\y\\org.txt"; string fileName_enc = "d:\\y\\enc.txt";
            FileStream fs = new FileStream(fileName_org, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            Console.WriteLine(sr.ReadToEnd());
            fs.Position = 0;
            BinaryReader br = new BinaryReader(fs);
            long len = br.BaseStream.Length;
            int rb = 0;

            FileStream fs2 = new FileStream(fileName_enc, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs2);

            bw.Write(len);

            while (rb < len)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (rb < len)
                        {
                            array[i, j] = br.ReadByte();
                            rb++;
                        }
                        else
                        {
                            array[i, j] = 0;
                        }
                    }
                }

                array2 = block.encrypt(array);
                writeArray(array2);

                // write arr2 to bin file


                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        bw.Write(array2[i, j]);
                    }
                }
                Console.WriteLine();
            }// while
            bw.Flush();
            bw.Close();
            fs2.Close();
            fs.Close();
        }


        public static void writeFile(string fileName_in_enc, string fileName_out_dec)
        {
            Block block = new Block();
            //string fileName_in_enc = "d:\\y\\enc.txt";
            //string fileName_out_dec = "d:\\y\\dec.txt";

            FileStream fs = new FileStream(fileName_in_enc, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            //Console.WriteLine(sr.ReadToEnd());
            fs.Position = 0;
            BinaryReader br = new BinaryReader(fs);
            long len = br.BaseStream.Length - 8;
            long len2 = br.ReadInt64();

            int rb = 0, wb = 0;

            FileStream fs2 = new FileStream(fileName_out_dec, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs2);

            while (rb < len)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        array[i, j] = br.ReadByte();
                        rb++;
                    }
                }

                array2 = block.dencrypt(array);
                writeArray(array2);

                // write arr2 to bin file


                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (wb < len2)
                        {
                            bw.Write(array2[i, j]);
                            wb++;
                        }
                    }
                }
                Console.WriteLine();
            }// while
            bw.Flush();
            bw.Close();
            fs2.Close();
            fs.Close();
        }


        public static void writeArray(byte[,] array)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(((char)array[i, j]) + (" " + array[i, j] + "\t"));
                }
                Console.WriteLine();
            }

        }

    } //class
} //namespace

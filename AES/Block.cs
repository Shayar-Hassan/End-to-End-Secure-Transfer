using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    class Block
    {
        static byte[,] cipherKey0 = {{0x2b,0x28,0xab,0x09},
                                 {0x7e,0xae,0xf7,0xcf},
                                 {0x15,0xd2,0x15,0x4f}, 
                                 {0x16,0xa6,0x88,0x3c} };
        static byte[,] cipherKey = new byte[4, 4];

        public static string getCipherKey()
        {

            string s = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    s = s + (char)cipherKey[i,j];
                }
            }
            return s;
        }

        public static void setCipherKey(string s)
        {
            if (s.Length < 16)
                cipherKey = cipherKey0;
            else
            {
                int k = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j  = 0; j < 4; j++)
                    {
                        cipherKey[i, j] = (byte)s[k];
                        k++;
                    }
                }
            }
                
        }


        byte[,] state = new byte[4, 4];

        public void dummyData()
        {
            string[,] s = {
                               {"19","a0","9a","e9"},
                               {"3d","f4","c6","f8"},
                               {"e3","e2","8d","48"},
                               {"be","2b","2a","08"}
                           };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = Sbox.hexTobyte(s[i, j]);
                }
            }
        }

        public void dummyData2()
        {
            string[,] s = {
                               {"eb","59","8b","1b"},
                               {"40","2e","a1","c3"},
                               {"f2","38","13","42"},
                               {"1e","84","e7","d2"}
                           };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = Sbox.hexTobyte(s[i, j]);
                }
            }
        }

        public void dummyData3()
        {
            string[,] s = {
                               {"ea","04","65","85"},
                               {"83","45","5d","96"},
                               {"5c","33","98","b0"},
                               {"f0","2d","ad","c5"}
                           };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = Sbox.hexTobyte(s[i, j]);
                }
            }
        }

        public void dummyData4()
        {
            string[,] s = {
                               {"32","88","31","e0"},
                               {"43","5a","31","37"},
                               {"f6","30","98","07"},
                               {"a8","8d","a2","34"}
                           };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = Sbox.hexTobyte(s[i, j]);
                }
            }
        }


        public void subBytes()
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    state[i, j] = Sbox.getVal(state[i, j]);

        }

        public void print()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(state[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void printHex()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string s = Convert.ToString(state[i, j], 16);
                    Console.Write(s + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void mixColumns()
        {
            int[,] state2 = new int[4, 4];
            int[] t = new int[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        t[k] = MulAES(galios[i, k], state[k, j]);
                    }
                    state2[i, j] = (t[0] ^ t[1] ^ t[2] ^ t[3]);
                    state2[i, j] = remAES(state2[i, j], 0x0100);
                }
            }
            copy(state2);



            //Console.WriteLine("state2");
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Console.Write(state2[i, j] + "\t");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

        }

        public void mixColumnsInv()
        {
            int[,] state2 = new int[4, 4];
            int[] t = new int[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        t[k] = MulAES(galiosInv[i, k], state[k, j]);
                    }
                    state2[i, j] = (t[0] ^ t[1] ^ t[2] ^ t[3]);
                    state2[i, j] = remAES(state2[i, j], 0x0100);
                }
            }
            copy(state2);



            //Console.WriteLine("state2");
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Console.Write(state2[i, j] + "\t");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

        }


        public void copy(int[,] state2)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = (byte)state2[i, j];
                }
            }
        }

        public void shiftRows()
        {
            byte temp;
            temp = state[1, 0];
            for (int j = 0; j < 3; j++)
            {
                state[1, j] = state[1, j + 1];
            }
            state[1, 3] = temp;

            temp = state[2, 0];
            state[2, 0] = state[2, 2];
            state[2, 2] = temp;
            temp = state[2, 1];
            state[2, 1] = state[2, 3];
            state[2, 3] = temp;

            temp = state[3, 3];
            for (int j = 3; j >= 1; j--)
            {
                state[3, j] = state[3, j - 1];
            }
            state[3, 0] = temp;

        }




        //MC try code
        public void mixColumns01()
        {
            byte[,] tmp = new byte[4, 4];
            tmp[0, 0] = (byte)(multiplyBy2[state[0, 0]] ^ multiplyBy3[state[0, 1]] ^ state[0, 2] ^ state[0, 3]);
            tmp[0, 1] = (byte)(state[0, 0] ^ multiplyBy2[state[0, 1]] ^ multiplyBy3[state[0, 2]] ^ state[0, 3]);
            tmp[0, 2] = (byte)(state[0, 0] ^ state[0, 1] ^ multiplyBy2[state[0, 2]] ^ multiplyBy3[state[0, 3]]);
            tmp[0, 3] = (byte)(multiplyBy3[state[0, 0]] ^ state[0, 1] ^ state[0, 2] ^ multiplyBy2[state[0, 3]]);

            tmp[1, 0] = (byte)(multiplyBy2[state[1, 0]] ^ multiplyBy3[state[1, 1]] ^ state[1, 2] ^ state[1, 3]);
            tmp[1, 1] = (byte)(state[1, 0] ^ multiplyBy2[state[1, 1]] ^ multiplyBy3[state[1, 2]] ^ state[1, 3]);
            tmp[1, 2] = (byte)(state[1, 0] ^ state[1, 1] ^ multiplyBy2[state[1, 2]] ^ multiplyBy3[state[1, 3]]);
            tmp[1, 3] = (byte)(multiplyBy3[state[1, 0]] ^ state[1, 1] ^ state[1, 2] ^ multiplyBy2[state[1, 3]]);

            tmp[2, 0] = (byte)(multiplyBy2[state[2, 0]] ^ multiplyBy3[state[2, 1]] ^ state[2, 2] ^ state[2, 3]);
            tmp[2, 1] = (byte)(state[2, 0] ^ multiplyBy2[state[2, 1]] ^ multiplyBy3[state[2, 2]] ^ state[2, 3]);
            tmp[2, 2] = (byte)(state[2, 0] ^ state[2, 1] ^ multiplyBy2[state[2, 2]] ^ multiplyBy3[state[2, 3]]);
            tmp[2, 3] = (byte)(multiplyBy3[state[2, 0]] ^ state[2, 1] ^ state[2, 2] ^ multiplyBy2[state[2, 3]]);

            tmp[3, 0] = (byte)(multiplyBy2[state[3, 0]] ^ multiplyBy3[state[3, 1]] ^ state[3, 2] ^ state[3, 3]);
            tmp[3, 1] = (byte)(state[3, 0] ^ multiplyBy2[state[3, 1]] ^ multiplyBy3[state[3, 2]] ^ state[3, 3]);
            tmp[3, 2] = (byte)(state[3, 0] ^ state[3, 1] ^ multiplyBy2[state[3, 2]] ^ multiplyBy3[state[3, 3]]);
            tmp[3, 3] = (byte)(multiplyBy3[state[3, 0]] ^ state[3, 1] ^ state[3, 2] ^ multiplyBy2[state[3, 3]]);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = tmp[i, j];
                }
            }
            Console.WriteLine("state2");
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(state[i, j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

        }

        public static int MulAES(byte ai, byte bi)
        {
            string a = Sbox.toBin(ai);
            string b = Sbox.toBin(bi);

            bool[] arr = new bool[20]; //zeros
            int pow;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (a[i] == '1' && b[j] == '1')
                    {
                        pow = (7 - i) + (7 - j);

                        arr[pow] = !arr[pow];
                    }
                }
            }

            //for (int i = arr.Length - 1; i >= 0; i--)
            //{
            //    if (i % 4 == 3)
            //        Console.WriteLine(" ");

            //    if (arr[i] == true)
            //        Console.Write("1");
            //    else
            //        Console.Write("0");
            //}

            int val = 0;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] == true)
                {
                    val += (1 << i);
                }
            }

            return val;
        }

        public static int remAES(int a, int b)
        {
            int blen = Convert.ToString(b, 2).Length;

            while (true)
            {
                int alen = Convert.ToString(a, 2).Length;
                int dlen = alen - blen;
                if (a < b)
                    return a;
                int bb = b << dlen;
                a = a ^ bb;
                //Console.WriteLine(a + " " + b + " " + bb + " " + Convert.ToString(a, 2) + " " + Convert.ToString(b, 2));
            }
        }

        public static byte powAES(byte a, byte b)
        {
            byte y = 1;
            for (int p = 1; p <= b; p++)
            {
                y = (byte)MulAES(y, a);
            }
            return y;
        }



        public void copy(byte[,] state2)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = state2[i, j];
                }
            }
        }

        //generate 4*44 array key
        public void keySchedule()
        {
            byte[,] rCon = {{0x01,0x02,0x04,0x08,0x10,0x20,0x40,0x80,0x1b,0x36},
                            {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00},
                            {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00},
                            {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}};


            byte[] B = new byte[4];
            byte[] A = new byte[4];
            for (int k = 0; k < 4; k++)
            {
                for (int j = 0; j < 4; j++)
                {
                    rotWord[k, j] = cipherKey[k, j];
                }
            }

            for (int col = 4; col < 44; col++)
            {
                if (col % 4 == 0)
                {
                    B[3] = rotWord[0, col - 1];
                    for (int row = 0; row < 3; row++)
                    {
                        B[row] = rotWord[row + 1, col - 1];
                        B[row] = Sbox.getVal(B[row]);

                        A[row] = rotWord[row, col - 4];
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        rotWord[i, col] = (byte)(A[i] ^ B[i] ^ rCon[i, (col / 4) - 1]);

                        //rotWord[i, col] = (byte)(A[i] ^ B[i] ^ rCon((col / 4), i));
                        // rotWord[i, col] = (byte)(B[i] ^ A[i] ^ rCon(i, col - 1));
                    }
                }
                else
                {
                    for (int k = 0; k < 4; k++)
                        rotWord[k, col] = (byte)(rotWord[k, col - 1] ^ rotWord[k, col - 4]);
                }
            }
        }

        byte[,] rotWord = new byte[4, 44];

        //4*4 array key
        public byte[,] getRoundKey_block(int k)
        {
            byte[,] key = new byte[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    key[i, j] = rotWord[i, j + 4 * k];
                }

            }
            return key;
        }

        //test to generate rCon array
        public byte rConZ(int i, int row)
        {
            if (row > 0)
                return 0;

            int r = i + 254;
            byte cur = 1;
            int div = 0x11B;
            for (int j = 1; j < r; j++)
            {
                cur = (byte)(cur << 1);
                if (cur >= div)
                    cur %= (byte)div;
            }
            return cur;
        }

        //main rCon Function
        public byte rCon(int i, int row)
        {
            if (row > 0)
                return 0;

            return powAES(0x02, (byte)(i - 1));
        }

        public void addRoundKey(int roundKeyIndex)
        {
            byte[,] key = getRoundKey_block(roundKeyIndex);

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = (byte)(state[i, j] ^ key[i, j]);
                }
            }
        }


        //inverse functions
        public void dummyInvData()
        {
            string[,] s = {
                               {"39","02","dc","19"},
                               {"25","dc","11","6a"},
                               {"84","09","85","0b"},
                               {"1d","fb","97","32"}
                           };

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = Sbox.hexTobyte(s[i, j]);
                }
            }
        }

        public void subBytesInv() //done
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    state[i, j] = Sbox.getValInv(state[i, j]);

        }

        public void shiftRowsInvOld() //done
        {
            byte temp0;

            temp0 = state[1, 0];
            state[1, 0] = state[1, 3];
            state[1, 3] = state[1, 2];
            state[1, 2] = state[1, 1];
            state[1, 1] = temp0;

            byte temp1;
            byte temp2;

            temp1 = state[2, 0];
            state[2, 0] = state[2, 2];
            state[2, 2] = temp1;
            temp2 = state[2, 1];
            state[2, 1] = state[2, 3];
            state[2, 3] = temp2;

            byte temp3;

            temp3 = state[3, 0];
            state[3, 0] = state[3, 1];
            state[3, 1] = state[3, 2];
            state[3, 2] = state[3, 3];
            state[3, 3] = temp3;

        }

        public void shiftRowsInv()
        {
            byte temp;
            temp = state[3, 0];
            for (int j = 0; j < 3; j++)
            {
                state[3, j] = state[3, j + 1];
            }
            state[3, 3] = temp;

            temp = state[2, 0];
            state[2, 0] = state[2, 2];
            state[2, 2] = temp;
            temp = state[2, 1];
            state[2, 1] = state[2, 3];
            state[2, 3] = temp;

            temp = state[1, 3];
            for (int j = 3; j >= 1; j--)
            {
                state[1, j] = state[1, j - 1];
            }
            state[1, 0] = temp;
        }

        public void mixColumnsInv999()
        {
            Console.WriteLine("error");
            int[,] state2 = new int[4, 4];
            int[] t = new int[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 4; k++)
                    {
                        t[k] = MulAES(galiosInv[i, k], state[k, j]);
                    }
                    state2[i, j] = (t[0] ^ t[1] ^ t[2] ^ t[3]);
                    state2[i, j] = remAES(state2[i, j], 0x011b);
                }
            }
            copy(state2);

            //Console.WriteLine("state2");
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        Console.Write(state2[i, j] + "\t");
            //    }
            //    Console.WriteLine();
            //}
            //Console.WriteLine();

        }

        public void decryptOld()
        {
            // read pass
            keySchedule();

            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        state[i, j] = (byte)(state[i, j] ^ cipherKey[i, j]);
            //    }

            //}

            addRoundKey(10);
            for (int i = 9; i >= 1; i--)
            {
                shiftRowsInv();
                subBytesInv();
                
                addRoundKey(i);
                printHex();

                mixColumnsInv();
                
            }
            shiftRowsInv();
            subBytesInv();
            addRoundKey(0);

        }


        public void encrypt()
        {
            // read pass
            keySchedule();

            /*
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    state[i, j] = (byte)(state[i, j] ^ cipherKey[i, j]);
                }

            }
            */
            addRoundKey(0);
            for (int i = 1; i <= 9; i++)
            {
                
                subBytes();
                
                shiftRows();
                mixColumns();//in for
                
                addRoundKey(i);
                
            }
            subBytes();
            shiftRows();
            addRoundKey(10);
        }



        public void decrypt()
        {
            keySchedule();

            addRoundKey(10);

            for (int i = 9; i >= 1; i--)
            {
       
                shiftRowsInv();
                subBytesInv();
                addRoundKey(i);
                mixColumnsInv();//in for
       
            }
            shiftRowsInv();
            subBytesInv();
            addRoundKey(0);
        }


        public void decrypt_1b_error()
        {
            keySchedule();

            addRoundKey(10);
            shiftRowsInv();
            subBytesInv();

            for (int i = 9; i >= 1; i--)
            {
                Console.Write("\n\n------------------------- decrypt ");
                Console.WriteLine(i);
                printHex();

                addRoundKey(i);
                printHex();

                mixColumnsInv();//in for
                printHex();

                shiftRowsInv();
                printHex();

                subBytesInv();
                printHex();

                Console.Write("***decrypt ");

            }
            addRoundKey(0);
        }


        public void decrypt3()
        {
            //keySchedule();

            addRoundKey(10);

            for (int i = 5; i >= 100; i--)
            {
                Console.Write("\n\n------------------------- decrypt ");
                Console.WriteLine(i);
                printHex();

                subBytesInv();
                printHex();

                shiftRowsInv();
                printHex();

                mixColumnsInv();//in for
                printHex();

                addRoundKey(i);
                printHex();

                Console.Write("***decrypt ");

            }
            addRoundKey(0);
        }


        //public void encryptZ()
        //{
        //    byte[,] cipherKey = getRoundKey_block(0);
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            state[i, j] = (byte)(state[i, j] + 1);
        //        }
        //    }
        //}

        //public void dencryptZ()
        //{
        //    byte[,] cipherKey = getRoundKey_block(0);
        //    for (int i = 0; i < 4; i++)
        //    {
        //        for (int j = 0; j < 4; j++)
        //        {
        //            state[i, j] = (byte)(state[i, j] - 1);
        //        }
        //    }
        //}

        public byte[,] encrypt(byte[,] input)
        {
            copy(input);
            encrypt();
            return state;
        }

        public byte[,] dencrypt(byte[,] input)
        {
            copy(input);
            decrypt();
            return state;
        }

        byte[,] galios = {
                          {2,3,1,1},
                          {1,2,3,1},
                          {1,1,2,3},
                          {3,1,1,2}
                          };


        byte[,] galiosInv = {
                          {0x0e,0x0b,0x0d,0x09},
                          {0x09,0x0e,0x0b,0x0d},
                          {0x0d,0x09,0x0e,0x0b},
                          {0x0b,0x0d,0x09,0x0e}
                          };


        byte[] multiplyBy2 = {0x00,0x02,0x04,0x06,0x08,0x0a,0x0c,0x0e,0x10,0x12,0x14,0x16,0x18,0x1a,0x1c,0x1e,
                              0x20,0x22,0x24,0x26,0x28,0x2a,0x2c,0x2e,0x30,0x32,0x34,0x36,0x38,0x3a,0x3c,0x3e,
                              0x40,0x42,0x44,0x46,0x48,0x4a,0x4c,0x4e,0x50,0x52,0x54,0x56,0x58,0x5a,0x5c,0x5e,
                              0x60,0x62,0x64,0x66,0x68,0x6a,0x6c,0x6e,0x70,0x72,0x74,0x76,0x78,0x7a,0x7c,0x7e,
                              0x80,0x82,0x84,0x86,0x88,0x8a,0x8c,0x8e,0x90,0x92,0x94,0x96,0x98,0x9a,0x9c,0x9e,
                              0xa0,0xa2,0xa4,0xa6,0xa8,0xaa,0xac,0xae,0xb0,0xb2,0xb4,0xb6,0xb8,0xba,0xbc,0xbe,
                              0xc0,0xc2,0xc4,0xc6,0xc8,0xca,0xcc,0xce,0xd0,0xd2,0xd4,0xd6,0xd8,0xda,0xdc,0xde,
                              0xe0,0xe2,0xe4,0xe6,0xe8,0xea,0xec,0xee,0xf0,0xf2,0xf4,0xf6,0xf8,0xfa,0xfc,0xfe,
                              0x1b,0x19,0x1f,0x1d,0x13,0x11,0x17,0x15,0x0b,0x09,0x0f,0x0d,0x03,0x01,0x07,0x05,
                              0x3b,0x39,0x3f,0x3d,0x33,0x31,0x37,0x35,0x2b,0x29,0x2f,0x2d,0x23,0x21,0x27,0x25,
                              0x5b,0x59,0x5f,0x5d,0x53,0x51,0x57,0x55,0x4b,0x49,0x4f,0x4d,0x43,0x41,0x47,0x45,
                              0x7b,0x79,0x7f,0x7d,0x73,0x71,0x77,0x75,0x6b,0x69,0x6f,0x6d,0x63,0x61,0x67,0x65,
                              0x9b,0x99,0x9f,0x9d,0x93,0x91,0x97,0x95,0x8b,0x89,0x8f,0x8d,0x83,0x81,0x87,0x85,
                              0xbb,0xb9,0xbf,0xbd,0xb3,0xb1,0xb7,0xb5,0xab,0xa9,0xaf,0xad,0xa3,0xa1,0xa7,0xa5,
                              0xdb,0xd9,0xdf,0xdd,0xd3,0xd1,0xd7,0xd5,0xcb,0xc9,0xcf,0xcd,0xc3,0xc1,0xc7,0xc5,
                              0xfb,0xf9,0xff,0xfd,0xf3,0xf1,0xf7,0xf5,0xeb,0xe9,0xef,0xed,0xe3,0xe1,0xe7,0xe5};

        byte[] multiplyBy3 = {0x00,0x03,0x06,0x05,0x0c,0x0f,0x0a,0x09,0x18,0x1b,0x1e,0x1d,0x14,0x17,0x12,0x11,
                              0x30,0x33,0x36,0x35,0x3c,0x3f,0x3a,0x39,0x28,0x2b,0x2e,0x2d,0x24,0x27,0x22,0x21,
                              0x60,0x63,0x66,0x65,0x6c,0x6f,0x6a,0x69,0x78,0x7b,0x7e,0x7d,0x74,0x77,0x72,0x71,
                              0x50,0x53,0x56,0x55,0x5c,0x5f,0x5a,0x59,0x48,0x4b,0x4e,0x4d,0x44,0x47,0x42,0x41,
                              0xc0,0xc3,0xc6,0xc5,0xcc,0xcf,0xca,0xc9,0xd8,0xdb,0xde,0xdd,0xd4,0xd7,0xd2,0xd1,
                              0xf0,0xf3,0xf6,0xf5,0xfc,0xff,0xfa,0xf9,0xe8,0xeb,0xee,0xed,0xe4,0xe7,0xe2,0xe1,
                              0xa0,0xa3,0xa6,0xa5,0xac,0xaf,0xaa,0xa9,0xb8,0xbb,0xbe,0xbd,0xb4,0xb7,0xb2,0xb1,
                              0x90,0x93,0x96,0x95,0x9c,0x9f,0x9a,0x99,0x88,0x8b,0x8e,0x8d,0x84,0x87,0x82,0x81,
                              0x9b,0x98,0x9d,0x9e,0x97,0x94,0x91,0x92,0x83,0x80,0x85,0x86,0x8f,0x8c,0x89,0x8a,
                              0xab,0xa8,0xad,0xae,0xa7,0xa4,0xa1,0xa2,0xb3,0xb0,0xb5,0xb6,0xbf,0xbc,0xb9,0xba,
                              0xfb,0xf8,0xfd,0xfe,0xf7,0xf4,0xf1,0xf2,0xe3,0xe0,0xe5,0xe6,0xef,0xec,0xe9,0xea,
                              0xcb,0xc8,0xcd,0xce,0xc7,0xc4,0xc1,0xc2,0xd3,0xd0,0xd5,0xd6,0xdf,0xdc,0xd9,0xda,
                              0x5b,0x58,0x5d,0x5e,0x57,0x54,0x51,0x52,0x43,0x40,0x45,0x46,0x4f,0x4c,0x49,0x4a,
                              0x6b,0x68,0x6d,0x6e,0x67,0x64,0x61,0x62,0x73,0x70,0x75,0x76,0x7f,0x7c,0x79,0x7a,
                              0x3b,0x38,0x3d,0x3e,0x37,0x34,0x31,0x32,0x23,0x20,0x25,0x26,0x2f,0x2c,0x29,0x2a,
                              0x0b,0x08,0x0d,0x0e,0x07,0x04,0x01,0x02,0x13,0x10,0x15,0x16,0x1f,0x1c,0x19,0x1a};



    } //class
} //namespace


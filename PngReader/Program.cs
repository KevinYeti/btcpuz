using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PngReader
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat src = new Mat("challenge.png", ImreadModes.Unchanged);
            //extractA(src);

            //getC();

            getAscii();


            Console.ReadKey();
        }

        static void getAscii()
        {
            string code = File.ReadAllText("code.txt");
            string s = string.Empty;
            for (int i = 0; i < code.Length; i+=8)
            {
                string b = code.Substring(i, 8);
                s += Convert.ToInt32(b, 2);

            }

            File.WriteAllText("ascii.txt", s);

            Console.WriteLine("ascii done.");

        }

        static void getC()
        {
            Mat alpha = new Mat("alpha.png", ImreadModes.Unchanged);

            string s = string.Empty;

            for (int j = 0; j < alpha.Width; j++)
            {
                int c = alpha.At<Vec4b>(310, j)[0];

                if (c == 0)
                    s += "1";
                else
                    s += "0";
            }

            File.WriteAllText("code.txt", s);
            Console.WriteLine("code done.");
        }

        static void extractA(Mat src)
        {
            Mat dst = new Mat();
            Cv2.ExtractChannel(src, dst, 3);

            for (int i = 0; i < dst.Height; i++)
            {
                for (int j = 0; j < dst.Width; j++)
                {
                    int a = dst.At<Vec4b>(i, j)[3];
                    switch (a)
                    {
                        case 253:
                            dst.Set(i, j, 0);
                            break;
                        case 254:
                            dst.Set(i, j, 255);
                            break;
                        case 255:
                        default:
                            break;
                    }

                }
            }

            dst.SaveImage("alpha.png");

            //Mat[] Planes = new Mat[4];
            //Planes = Cv2.Split(src);
            using (new Window("dst image", WindowMode.Normal, dst))
            {
                Cv2.WaitKey();
            }
        }
    }
}

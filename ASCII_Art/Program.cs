using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Windows;
using System.Diagnostics;

namespace ASCII_Art
{
    class Program
    {
        //public static String value = " .,;=ijL(FEOMB@";
        public static String value = "@BMOJjEF(Li=;,. ";

        [STAThreadAttribute]
        static void Main(string[] args)
        {
            start();
        }

        static void start() {
            Console.Write(@"Enter image path: D:\User\Darbvisma\");
            String path = Console.ReadLine();

            path = @"D:\User\Darbvisma\" + path;

            Console.Write("Downwsize rezolution by a factor of: ");
            int resolution = Int32.Parse(Console.ReadLine());

            var image = Image.FromFile(@path);

            Console.Clear();
            show(images(image.Width / resolution, (int)(image.Height / resolution / 2.0), path));
            Console.WriteLine(" " + path + "\n Resolution changed by a factor of: " + resolution);

            Console.ReadLine();
        }

        static String[] images(int width, int height, String path){
            String[] text = new String[height];
            String newImagePath = @"D:\User\Darbvisma\%TEMP-2018%";
            createFolder(newImagePath);

            using (Bitmap bitmap = (Bitmap)Image.FromFile(@path))
            {
                using (Bitmap newBitmap = new Bitmap(bitmap, width, height))
                {
                    newBitmap.SetResolution(width, height);
                    newBitmap.Save(newImagePath + @"\test.jpg", ImageFormat.Jpeg); 
                }
            }

            Bitmap image = (Bitmap)Image.FromFile(newImagePath + @"\test.jpg");

            int number = 0;
            for (int i = 0; i < height; i++) {
                for (int b = 0; b < width; b++) {
                    Color color = image.GetPixel(b, i);
                    Debug.WriteLine(color.GetBrightness());
                    int myInt = (int)Math.Ceiling(Math.Round((Decimal)color.GetBrightness() * (value.Length - 1), 0, MidpointRounding.AwayFromZero));

                    text[i] += value[myInt];
                }

                if (number == 0) {
                Console.Clear();
                Console.WriteLine("            " + Math.Round((Decimal)((decimal)i / (decimal)height) * 100, 0, MidpointRounding.AwayFromZero) + "%");
                }

                number += (number == height / 100 - 1? -(height / 100 - 1) : 1);
            }

            return text;
        }

        static void createFolder(String path) {
            System.IO.Directory.CreateDirectory(@path);
        }

        static void deleteFolder() {
        }

        static void show(String[] array) {
            String text = "";
            for (int i = 0; i < array.Length; i++)
            {
                text += array[i] + "\r\n";
            }

            Clipboard.SetText(text);
            Console.Clear();
            Console.WriteLine(" Copied to clipboard.");
        }
    }
}

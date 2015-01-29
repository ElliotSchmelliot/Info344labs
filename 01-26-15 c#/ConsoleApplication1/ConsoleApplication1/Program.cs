using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Basic data classes
            int var1 = 10;
            float var2 = 10.0f;
            double var3 = 10.0;
            string var4 = "hello world";
            bool var5 = true;

            int[] intArray = new int[10];
            string[] strArray = new string[3];
            strArray[0] = "ice cream";
            strArray[1] = "pineapple";
            Console.WriteLine(strArray[0]);

            // Collections
            List<int> intList = new List<int>();
            intList.Add(10);
            intList.Add(1);
            Console.WriteLine(intList.Last());

            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("name", "CK");
            dict.Add("class", "info344");
            Console.WriteLine(dict["name"]);

            // String comparison
            StringBuilder s1 = new StringBuilder("s1");
            StringBuilder s2 = new StringBuilder("s1");
            Console.WriteLine(s1 == s2); //False
            Console.WriteLine(s1.Equals(s2)); //True
            Console.WriteLine(s1.ToString() == s2.ToString()); //True
            Console.WriteLine("s1" == "s2"); //False

            // for loop
            foreach (string s in dict.Keys)
            {
                Console.WriteLine(s + ": " + dict[s]);
            }

            // classes
            eBayProduct p = new eBayProduct();
            Console.WriteLine(p.Name + ": " + p.Price);
            p.SetName("Windows 8");
            p.Price = 1500.00;
            //p.Name = "doesn't work, private";
            Console.WriteLine(p.Name + ": " + p.Price);
            Console.WriteLine(eBayProduct.GetDefaultProducts());

            // files
            using (StreamWriter sw = new StreamWriter("output.txt"))
            {
                for (int i = 0; i < 10; i++)
                {
                    sw.WriteLine(i);
                }
            }

            using (StreamReader sr = new StreamReader("output.txt"))
            {
                while (sr.EndOfStream == false)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }

            // Set debug point here to see console output during run
            Console.WriteLine("End");
        }
    }
}

﻿using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Query the table for partition matches
            List<WebPageEntity> candidates = new List<WebPageEntity>();
            candidates.Add(new WebPageEntity("nba", "x.com", new DateTime(2015, 3, 9), "div stuff"));
            candidates.Add(new WebPageEntity("nba", "y.com", new DateTime(2015, 3, 10), "div stuff"));
            candidates.Add(new WebPageEntity("nba", "z.com", new DateTime(2015, 3, 8), "div stuff"));
            candidates.Add(new WebPageEntity("bleacher", "x.com", new DateTime(2015, 3, 6), "div stuff"));
            candidates.Add(new WebPageEntity("bleacher", "y.com", new DateTime(2015, 3, 12), "div stuff"));
            candidates.Add(new WebPageEntity("report", "x.com", new DateTime(2015, 3, 15), "div stuff"));

            var res = candidates
                .GroupBy(x => x.RowKey)
                .Select(x => new Tuple<string, int>(x.Key, x.ToList().Count))
                .OrderByDescending(x => x.Item2)
                .Take(10);

            foreach (Tuple<string, int> ent in res)
            {
                Console.WriteLine(ent);
            }


            string s = "bleacher, report | says stuff....";
            var sb = new StringBuilder();

            foreach (char c in s)
            {
                if (!char.IsPunctuation(c) || c == ' ')
                    sb.Append(c);
            }
            s = sb.ToString();
            Console.WriteLine("s = " + s);



            string codeTest = "http://bleacherreport.com/nba";
            string encoded = HttpUtility.UrlEncode(codeTest);
            Console.WriteLine(encoded);
            Console.WriteLine(HttpUtility.UrlDecode(encoded));



            List<string> results = GetSquares(5);
            foreach (string n in results)
            {
                Console.WriteLine(n);
            }

            var numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 2, 4, 6, 8, 2 };
            IEnumerable<Tuple<int, int>> tupleEnum = GetEvenTuples(numbers);
            foreach (Tuple<int, int> t in tupleEnum)
            {
                Console.WriteLine(t);
            }

            IEnumerable<int> primes = GetPrimes(numbers);
            foreach (int n in primes)
            {
                Console.WriteLine(n);
            }

            // SelectMany flatten strings to chars
            //var input = new string[] { };
            //var result = input.SelectMany(x => x);

            StreamReader reader = new StreamReader("obama.txt");
            string text = reader.ReadToEnd();
            char[] splits = new char[] { ' ', '\n' };
            int obamas = text.Split(splits).Where(x => x.ToLower().Equals("obama")).Count();
            Console.WriteLine("Obama: " + obamas);

            // int obamaAndPres = text.ToLower().Split('.').Where(x => x.Contains(" obama ") && x.Contains(" president ")).Count();
            int obamaAndPres = text.ToLower().Split('.').Select(x => x.Split(splits)).Where(x => x.Contains("obama") && x.Contains("president")).Count();

            Console.WriteLine("Obama + President: " + obamaAndPres);
            Exit();
        }

        // Gets squares from 0 to n-1.
        public static List<string> GetSquares(int n)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < n; i++)
            {
                list.Add(i);
            }

            var results = list
                .Select(x => x * x)
                .OrderByDescending(x => x)
                .Select(x => x.ToString()).ToList();

            return results;
        }

        // Gets tuple count of all even numbers. Tuple is for easy manipulating LINQ data.
        public static IEnumerable<Tuple<int, int>> GetEvenTuples(int[] nums)
        {
            var evenNumberHistogram = nums
                .Where(x => x % 2 == 0)
                .GroupBy(x => x)
                .Select(x => new Tuple<int, int>(x.Key, x.ToList().Count))
                .OrderByDescending(x => x.Item1);
            return evenNumberHistogram;
        }

        public static IEnumerable<int> GetPrimes(int[] nums)
        {
            var primeNumbers = nums
                .Where(isPrime);
            return primeNumbers;
        }

        public static Boolean isPrime(int n)
        {
            int divisCount = 1;
            for (int i = 2; i < n; i++)
            {
                if (n % i == 0)
                {
                    divisCount++;
                }
            }
            return divisCount == 1;
        }

        public static void Exit()
        {
            Console.WriteLine();
            Console.Write("Press <Enter> to exit.");
            Console.ReadKey();
        }
    }
}

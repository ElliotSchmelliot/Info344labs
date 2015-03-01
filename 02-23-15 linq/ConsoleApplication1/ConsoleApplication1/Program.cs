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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class eBayProduct
    {
        // properties
        public string Name { get; private set; }
        public double Price { get; set; }

        // constructor with no paramas
        public eBayProduct()
        {
            this.Name = "unknown";
            this.Price = 0.0;
        }

        // constructor with params
        public eBayProduct(string name, double price)
        {
            this.Name = name;
            this.Price = price;
        }

        // method
        public void SetName(string newName)
        {
            this.Name = newName;
        }

        // static function
        public static List<eBayProduct> GetDefaultProducts()
        {
            List<eBayProduct> products = new List<eBayProduct>();
            products.Add(new eBayProduct("iMac", 2000));
            products.Add(new eBayProduct("Win7", 1000));
            return products;
        }
    }
}

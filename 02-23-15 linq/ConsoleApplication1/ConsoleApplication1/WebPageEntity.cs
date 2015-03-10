using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ClassLibrary1
{
    /// <summary>
    /// An object representing a single webpage for storage in Azure Cloud Tables.
    /// </summary>
    public class WebPageEntity : TableEntity
    {
        public DateTime? pageDate { get; set; } // from html meta, nullable
        public string html { get; set; }
        public DateTime crawlDate { get; set; }

        // Blank constructor needed for initialization
        public WebPageEntity() { }

        public WebPageEntity(string titleKeyWord, string url, DateTime? pageDate, string html)
        {
            this.pageDate = pageDate;
            this.html = html;
            this.crawlDate = DateTime.Now;

            this.PartitionKey = titleKeyWord;
            this.RowKey = HttpUtility.UrlEncode(url); // must be encoded and decoded
        }
    }
}

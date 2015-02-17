using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;
using Microsoft.WindowsAzure.Storage;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WebRole1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public static NBAPlayerStats[] playerData;
        public static CloudTable table;

        public static CloudQueue theQueue;

        [WebMethod]
        private void InitializeCloud()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnString"]);
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference("nbaplayerstats");

            //table.CreateIfNotExists();
            // Recreate the table each time so data is not duplicated.
            table.DeleteIfExists();

            // After deletion, a table is not immediately ready for recreation. Sleep thread until
            // table is ready to be recreated. Takes a long time, would be faster to manually delete data
            // and not recreate table each time.
            Boolean error = true;
            while (error)
            {
                error = false;
                try
                {
                    table.Create();
                }
                catch
                {
                    error = true;
                    System.Threading.Thread.Sleep(5000);
                }
            }

        }

        // read csv data into memory
        [WebMethod]
        private void ReadCSV()
        {
            // local path to csv file in webrole
            string fileName = System.Web.HttpContext.Current.Server.MapPath(@"/2012-2013.nba.stats.csv");
            List<string> filedata = LoadCSVFile(fileName);
            var nbaplayers = filedata.Skip(9) // skip file header
                .Select(x => x.Split(','))
                .Select(x => new NBAPlayerStats(x[1], x[24])) // player name and ppg
                .Take(30)
                .ToArray();
            playerData = nbaplayers;
        }

        // save data to azure tables
        [WebMethod]
        private void SaveToTable()
        {
            foreach (NBAPlayerStats player in playerData)
            {
                TableOperation insertOperation = TableOperation.Insert(player);
                table.Execute(insertOperation);
            }
        }

        // search azure tables for data
        [WebMethod]
        public List<NBAPlayerStats> SearchTable(string string1, string string2)
        {
            InitializeCloud();
            ReadCSV();
            SaveToTable();
            TableQuery<NBAPlayerStats> rangeQuery = new TableQuery<NBAPlayerStats>()
                .Where(TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThan, string1),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.LessThan, string2))
                );


            List<NBAPlayerStats> stats = new List<NBAPlayerStats>();
            IEnumerable<NBAPlayerStats> results = table.ExecuteQuery(rangeQuery);
            foreach (NBAPlayerStats entity in results)
            {
                stats.Add(entity);
            }
            return stats;
        }

        private List<string> LoadCSVFile(string fileName)
        {
            List<string> data = new List<string>();
            StreamReader s = new StreamReader(new FileStream(fileName, FileMode.Open));
            while (!s.EndOfStream)
            {
                data.Add(s.ReadLine());
            }
            return data;
        }

        [WebMethod]
        private void InitializeQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                ConfigurationManager.AppSettings["StorageConnString"]);
            CloudQueueClient qClient = storageAccount.CreateCloudQueueClient();
            theQueue = qClient.GetQueueReference("myurls");
            theQueue.CreateIfNotExists();
        }

        [WebMethod]
        private void ReadMessage(string inputMessage)
        {
            CloudQueueMessage message = new CloudQueueMessage(inputMessage);
            theQueue.AddMessage(message);
        }

        // Retrieves messages from the queue, removes them, and returns their toString.
        [WebMethod]
        public string RemoveMessage()
        {
            InitializeQueue();
            ReadMessage("http://www.cnn.com/index.html");
            CloudQueueMessage message2 = theQueue.GetMessage(TimeSpan.FromMinutes(5));
            theQueue.DeleteMessage(message2);
            return message2.AsString;
        }


    }
}

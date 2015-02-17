using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using ClassLibrary1;
using System.Threading;

namespace WebRole1
{
    /// <summary>
    /// Summary description for admin
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        public static CloudStorageAccount storageAccount;

        // Pass 3 integers into CloudQueue for worker role to sum.
        [WebMethod]
        public string WorkerRoleCalculateSum(int one, int two, int three)
        {
            // Connect to CloudStorage
            InitializeCloudStorage();

            // Connect to CloudQueue
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("mynumbers"); // MUST be all lowercase or throws 400
            queue.CreateIfNotExists();

            // Add numbers to CloudQueue, use a single message
            string nums = one + "," + two + "," + three;
            CloudQueueMessage message = new CloudQueueMessage(nums);
            queue.AddMessage(message);

            return "Message added to queue: " + one + ", " + two + ", " + three;
        }

        [WebMethod]
        public int _TestQueue()
        {
            // Manually run the Worker Role Run() code.
            // Will only work if worker role is not running and removing from queue.
            InitializeCloudStorage();

            // Connect to CloudQueue
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue queue = queueClient.GetQueueReference("mynumbers");
            queue.CreateIfNotExists();

            // Read message from queue
            CloudQueueMessage message = queue.GetMessage(TimeSpan.FromMinutes(5));
            string nums = message.AsString;

            // calculate sum
            string[] numSplit = nums.Split(',');
            int sum = Convert.ToInt32(numSplit[0]) + Convert.ToInt32(numSplit[1]) + Convert.ToInt32(numSplit[2]);

            // sleep 10s
            Thread.Sleep(10000);

            // Connect to CloudTable
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("sums");
            table.CreateIfNotExists();

            // Put sum in Azure table storage
            NumberEntity ent = new NumberEntity(sum);
            TableOperation insertOperation = TableOperation.Insert(ent);
            table.Execute(insertOperation);

            return 0;
        }

        // Connects to Cloud Storage
        private void InitializeCloudStorage()
        {
            if (storageAccount == null)
            {
                storageAccount = CloudStorageAccount.Parse(
                    ConfigurationManager.AppSettings["StorageConnectionString"]);
            }
        }

        // Read sum from Azure Table storage
        [WebMethod]
        public List<NumberEntity> ReadSumFromTableStorage()
        {
            // Connect to CloudStorage
            InitializeCloudStorage();

            // Connect to CloudTable
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            CloudTable table = tableClient.GetTableReference("sums");
            table.CreateIfNotExists();

            // Access Azure table storage
            TableQuery<NumberEntity> query = new TableQuery<NumberEntity>()
                    .Take(20);
            IEnumerable<NumberEntity> ents = table.ExecuteQuery(query);

            List<NumberEntity> list = new List<NumberEntity>();
            foreach (NumberEntity entity in ents)
            {
                list.Add(entity);
            }
            return list;
        }
    }
}

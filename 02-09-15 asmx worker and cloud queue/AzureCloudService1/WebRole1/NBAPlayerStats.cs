using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage.Table;

namespace WebRole1
{
    public class NBAPlayerStats : TableEntity
    {
        public string Name { get; set; }
        public double PPG { get; set; }

        public NBAPlayerStats(string name, string ppg)
        {
            this.PartitionKey = name; // player name
            this.RowKey = Guid.NewGuid().ToString(); // something random

            this.Name = name;
            this.PPG = double.Parse(ppg);
        }

        public NBAPlayerStats() { }

    }
}

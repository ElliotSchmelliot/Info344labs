using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class NumberEntity : TableEntity
    {
        public int sum { get; set; }

        public NumberEntity(int sum)
        {
            this.sum = sum;

            this.PartitionKey = "sum";
            this.RowKey = Guid.NewGuid().ToString();
        }

        public NumberEntity() { }
    }

}

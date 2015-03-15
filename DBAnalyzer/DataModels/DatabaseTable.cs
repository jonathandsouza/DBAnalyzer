using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class DatabaseTable
    {
        public String TableName { get; set; }
        public int TableEntries { get; set; }
        public String PrimaryKey { get; set; }
        public int MaxPrimaryKeyValue { get; set; }

    }

}

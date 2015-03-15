using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataModels
{

    public class DatabaseComparison
    {
        public List<ComparisonEntry> Comparison{ get; set; }

        #region public properties
        public DatabaseComparison()
        {
            Comparison = new List<ComparisonEntry>();
        }
        #endregion

    }

    public class  ComparisonEntry
    {
        public DatabaseTable OriginalTalbe { get; set; }

        public DatabaseTable RefTable { get; set; }

        public int EntryDifferences { get; set; }

        public int MaxPrimaryKeyValueDiff { get; set; }

        public DataTable DifferenceTable { get; set; }


    }
}

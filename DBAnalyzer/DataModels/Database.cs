using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels
{
    public class Database
    {

        #region public properties
        public List<DatabaseTable> Tables{ get; set; }
        #endregion

        #region public methods
        public Database()
        {
            Tables = new List<DatabaseTable>();
        }

        public void ClearTableList()
        {
            Tables.Clear(); 
        }

        public void  GenerateTableList(List<string> input)
        {

            Tables = input.Select(n => new DatabaseTable { TableName = n }).ToList<DatabaseTable>();

        }

        #endregion

    }
}

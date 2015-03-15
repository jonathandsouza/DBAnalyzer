using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using System.Data;
using DBAccess;


namespace CoreLogic
{
    public class CoreDBAnalyzerLogic
    {
        #region properties
        public Database DBRefPoint { get; set; }

        #endregion

        #region private fields

        private Access dbAccess = new Access();

        #endregion

        #region public methods

        public CoreDBAnalyzerLogic()
        {
            DBRefPoint = new Database();
        }

        
        public void SetInitialDBRefrencePoint()
        {
            DBRefPoint = this.GenerateDBRefrencePoint();
        }
       
        public DatabaseComparison CompareToLastRefrencePoint()
        {
            Database currentyDbRefrencePoint = this.GenerateDBRefrencePoint();

            return CompareDbRefences(DBRefPoint, currentyDbRefrencePoint);

        }


        public DataSet getDifferenceData(DatabaseComparison dataComparison ,string tableName)
        {
            var table = dataComparison.Comparison.Where(n=>n.OriginalTalbe.TableName == tableName).Select(n=>n.OriginalTalbe).FirstOrDefault();

            return dbAccess.getEntriesAboveID(table.TableName, table.PrimaryKey, table.MaxPrimaryKeyValue.ToString());

        }

        #endregion

        #region private methods

        Database GenerateDBRefrencePoint()
        {
            Database tempdbRefPoint = new Database();

            var listTables = dbAccess.GetDBTableList();
            tempdbRefPoint.ClearTableList();
            tempdbRefPoint.Tables = GenerateDbTableData(listTables);

            return tempdbRefPoint;
        }

        List<DatabaseTable> GenerateDbTableData(List<string> listTable)
        {
            List<DatabaseTable> result = new List<DatabaseTable>();

            result = listTable.Select(n => new DatabaseTable
                {
                    MaxPrimaryKeyValue = dbAccess.GetMaxPrimaryKeyValue(n),
                    PrimaryKey = dbAccess.GetPrimaryKeyOfTable(n),
                    TableEntries =dbAccess.GetTotalTableEntries(n),
                    TableName = n
                }
            ).ToList<DatabaseTable>();

            return result;

        }

        DatabaseComparison CompareDbRefences(Database initial, Database current)
        {
            DatabaseComparison result = new DatabaseComparison();

            foreach (DatabaseTable iTable in initial.Tables)
            {
                var cTable = current.Tables.Where(n => n.TableName == iTable.TableName).FirstOrDefault();

                DataTable tempDT =null;


                if (cTable.TableEntries - iTable.TableEntries > 0 && !string.IsNullOrEmpty(cTable.PrimaryKey) && !string.IsNullOrEmpty(cTable.TableName) && !string.IsNullOrEmpty(iTable.MaxPrimaryKeyValue.ToString()))
                    tempDT = dbAccess.getEntriesAboveID(cTable.TableName, cTable.PrimaryKey, iTable.MaxPrimaryKeyValue.ToString()).Tables[0];
                else
                    tempDT = null;

                
                result.Comparison.Add(new ComparisonEntry
                             {
                                 EntryDifferences = cTable.TableEntries - iTable.TableEntries,
                                 MaxPrimaryKeyValueDiff = cTable.TableEntries - iTable.TableEntries,
                                 OriginalTalbe = iTable,
                                 RefTable = cTable,
                                 DifferenceTable = tempDT
                             });



            }

            result.Comparison = result.Comparison.Where(n=>n.EntryDifferences!=0).ToList<ComparisonEntry>();

            return result;
        }
        



        
        #endregion
        
    }
}

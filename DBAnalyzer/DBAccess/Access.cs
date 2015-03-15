using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DBAccess
{
    public class Access
    {
        
        SqlConnection sqlconn = new SqlConnection(CentralConfig.connectionString);

        public  List<string> GetDBTableList()
        {
            List<string> result = new List<string>();


            string sqlquery =@"SELECT t.name AS TABLE_NAME,
                                SCHEMA_NAME(schema_id) AS schema_name
                            FROM sys.tables AS t";

            SqlCommand sqlCmd = new SqlCommand(sqlquery,sqlconn);

            SqlDataAdapter sqlAdpt= new SqlDataAdapter(sqlCmd);

            try
            {
                DataSet dataSet = new DataSet();
                sqlAdpt.Fill(dataSet);
                if (dataSet.Tables != null && dataSet.Tables[0] != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in dataSet.Tables[0].Rows)
                        result.Add(row["TABLE_NAME"].ToString());
                }
            }
            catch
            {

            }

            return result;

        }

        public string GetPrimaryKeyOfTable(string tableName)
        {
            string result=string.Empty;


            string sqlquery = string.Format(@"SELECT column_name  
                                FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE
                                WHERE OBJECTPROPERTY(OBJECT_ID(constraint_name), 'IsPrimaryKey') = 1
                                AND table_name = '{0}'",tableName);

            SqlCommand sqlCmd = new SqlCommand(sqlquery, sqlconn);



            try
            {
                sqlconn.Open();
                result = sqlCmd.ExecuteScalar().ToString();
            }
            catch
            {

            }
            finally
            {
                sqlconn.Close();
            }

            return result;

        }  

        public  int  GetTotalTableEntries(string tableName)
        {
            int result=0;
            
            string sqlquery = string.Format(@"SELECT count(*) FROM {0}", tableName);

            SqlCommand sqlCmd = new SqlCommand(sqlquery, sqlconn);

            try
            {
                sqlconn.Open();
                result = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                sqlconn.Close();
            }

            return result;

        }  

        public  int GetMaxPrimaryKeyValue(string tableName,string primaryKey="")
        {
            int result=0;
            string sqlquery =string.Empty;

            if(string.IsNullOrEmpty(primaryKey))                        
                sqlquery = string.Format(@"SELECT max({0}) FROM {1}", GetPrimaryKeyOfTable(tableName) ,tableName);
            else
                sqlquery = string.Format(@"SELECT max({0}) FROM {1}",  primaryKey ,tableName);  

            SqlCommand sqlCmd = new SqlCommand(sqlquery, sqlconn);

            try
            {
                sqlconn.Open();
                result = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
            catch
            {

            }
            finally
            {
                sqlconn.Close();
            }

            return result;

        }  
    
        public DataSet getEntriesAboveID(string tableName,string primaryKey,string id)
        {
            DataSet result = new DataSet();


            string sqlquery = string.Format(@"SELECT * from {0} where {1} >{2}",tableName,primaryKey,id);

            SqlCommand sqlCmd = new SqlCommand(sqlquery, sqlconn);

            SqlDataAdapter sqlAdpt = new SqlDataAdapter(sqlCmd);

            try
            {
                sqlAdpt.Fill(result);
                
            }
            catch
            {

            }

            return result;

        }

    }
}

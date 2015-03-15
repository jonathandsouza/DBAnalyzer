using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBAccess;
using DataModels;


namespace ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {

            CoreLogic.CoreDBAnalyzerLogic cLogic = new CoreLogic.CoreDBAnalyzerLogic();

            string cmd = string.Empty;
            
           do
           {
               Console.Write("cmd>>");
               cmd = Console.ReadLine().Trim();

               switch (cmd.ToUpper())
               {
                   case "SET":
                       cLogic.SetInitialDBRefrencePoint();
                       Console.WriteLine("OPERATION COMPLETE");
                       break;
                   case "COMPARE":
                       DatabaseComparison dc=  cLogic.CompareToLastRefrencePoint();

                       foreach (ComparisonEntry ce in dc.Comparison)
                           Console.WriteLine("{0} vs {1} ===> DIFF:{2}", ce.OriginalTalbe.TableName, ce.RefTable.TableName, ce.EntryDifferences);
                       break;
                   default:
                       break;
               }
           
           }while(cmd.ToUpper()!="EXIT" || cmd.ToUpper() != "QUIT");

        }
    }
}

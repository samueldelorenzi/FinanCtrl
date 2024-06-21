using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using FinanCtrl.Data;
using FinanCtrl.Models;

namespace FinanCtrl.Utils
{
    public class ExportarDados
    {
        public static void ExportLucro()
        {
            StreamWriter Export = File.AppendText(@"lucrohistory.txt");

            foreach (Lucro lucro in DataSet.lucros)
            {
                Export.WriteLine(lucro.ToString());
            }

            Export.Close();
        }
        public static void ExportDespesa()
        {
            StreamWriter Export = File.AppendText(@"despesahistory.txt");

            foreach (Despesa despesa in DataSet.despesas)
            {
                Export.WriteLine(despesa.ToString());
            }
            
            Export.Close();
        }
    }
}

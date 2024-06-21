using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanCtrl.Models;

namespace FinanCtrl.Data
{
    public class DataSet
    {
        public static List<Lucro> lucros { get; set; } = new List<Lucro>();
        public static List<Despesa> despesas { get; set; } = new List<Despesa>();
    }
}
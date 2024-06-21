using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanCtrl.Models;
using FinanCtrl.Data;

namespace FinanCtrl.Repository
{
    public class DespesaRepository
    {
        public bool Save(Despesa despesa)
        {
            DataSet.despesas.Add(despesa);
            return true;
        }
        public List<Despesa> Retrieve()
        {
            return DataSet.despesas;
        }
        public bool DeleteById(int id)
        {
            try
            {
                DataSet.despesas.RemoveAt(id);
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
        public bool UpdateDespesa(Despesa despesa, int id)
        {
            try
            {
                DataSet.despesas[id] = despesa;
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
    }
}
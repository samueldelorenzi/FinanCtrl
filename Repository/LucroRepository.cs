using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanCtrl.Models;
using FinanCtrl.Data;

namespace FinanCtrl.Repository
{
    public class LucroRepository
    {
        public bool Save(Lucro lucro)
        {
            DataSet.lucros.Add(lucro);
            return true;
        }
        public List<Lucro> Retrieve()
        {
            return DataSet.lucros;
        }
        public bool DeleteById(int id)
        {
            try
            {
                DataSet.lucros.RemoveAt(id);
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
            
        }
        public bool UpdateLucro(Lucro lucro, int id)
        {
            try
            {
                DataSet.lucros[id] = lucro;
            }
            catch (System.Exception)
            {
                return false;
            }

            return true;
        }
    }
}
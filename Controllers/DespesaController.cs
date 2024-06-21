using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanCtrl.Models;
using FinanCtrl.Repository;

namespace FinanCtrl.Controllers
{
    public class DespesaController
    {
        private DespesaRepository despesaRepository;
        public DespesaController()
        {
            despesaRepository = new DespesaRepository();
        }
        public bool Insert(Despesa despesa)
        {
            if (despesaRepository.Save(despesa))
                return true;
            else
                return false;    
        }
        public List<Despesa> Get()
        {
            return despesaRepository.Retrieve();
        }

        public bool Delete(int id)
        {
            if (despesaRepository.DeleteById(id))
                return true;
            else
                return false;
        }
        public bool Update(Despesa despesa, int id)
        {
            if (despesaRepository.UpdateDespesa(despesa, id))
                return true;
            else
                return false;
        }
    }
}
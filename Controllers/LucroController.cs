using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using FinanCtrl.Models;
using FinanCtrl.Repository;

namespace FinanCtrl.Controllers
{

    public class LucroController
    {
        private LucroRepository lucroRepository;
        public LucroController()
        {
            lucroRepository = new LucroRepository();
        }
        public bool Insert(Lucro lucro)
        {
            if (lucroRepository.Save(lucro))
                return true;
            else
                return false;    
        }
        public List<Lucro> Get()
        {
            return lucroRepository.Retrieve();
        }

        public bool Delete(int id)
        {
            if (lucroRepository.DeleteById(id))
                return true;
            else
                return false;
        }
        public bool Update(Lucro lucro, int id)
        {
            if (lucroRepository.UpdateLucro(lucro, id))
                return true;
            else
                return false;
        }
    }
}
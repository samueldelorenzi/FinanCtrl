using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanCtrl.Models;
using FinanCtrl.Repository;

namespace FinanCtrl.Controllers
{
    public class RelatorioController
    {
        private RelatorioRepository relatorioRepository;
        private DespesaRepository despesaRepository;
        private LucroRepository lucroRepository;
        public RelatorioController()
        {
            lucroRepository = new LucroRepository();
            relatorioRepository = new RelatorioRepository();
            despesaRepository = new DespesaRepository();
        }       
        public float RetornarSaldo()
        {
            return relatorioRepository.CalcularSaldo();
        }
        public Despesa MaiorGasto()
        {
            List<Despesa> gastos = despesaRepository.Retrieve();

            return gastos.MaxBy(despesa => despesa.Valor);
        }
        public Lucro MaiorGanho()
        {
            List<Lucro> ganhos = lucroRepository.Retrieve();

            return ganhos.MaxBy(ganho => ganho.Valor);
        }
        public float GastoNoDia(string data)
        {
            float gastosdiarios = 0;
            List<Despesa> gastos = despesaRepository.Retrieve();
            IEnumerable<Despesa> gastosnodia = gastos.Where(despesas => despesas.Data == data);

            foreach (Despesa despesa in gastosnodia)
            {
                gastosdiarios += despesa.Valor;
            }

            return gastosdiarios;
        }
        public float GanhoNoDia(string data)
        {
            float ganhosdiarios = 0;
            List<Lucro> ganhos = lucroRepository.Retrieve();
            IEnumerable<Lucro> ganhosnodia = ganhos.Where(ganhos => ganhos.Data == data);

            foreach (Lucro lucro in ganhosnodia)
            {
                ganhosdiarios += lucro.Valor;
            }

            return ganhosdiarios;
        }
    }
}
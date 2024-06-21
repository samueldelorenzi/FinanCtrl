using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanCtrl.Controllers;
using FinanCtrl.Data;
using FinanCtrl.Models;

namespace FinanCtrl.Repository
{
    public class RelatorioRepository
    {
        LucroController lucroController = new LucroController();
        DespesaController despesaController = new DespesaController();
        public float CalcularSaldo()
        {
            List<Lucro> lucros = lucroController.Get();
            List<Despesa> despesas = despesaController.Get();

            float somalucros = 0;
            foreach (Lucro lucro in lucros)
            {
                somalucros += lucro.Valor;
            }

            float somadespesas = 0;
            foreach (Despesa despesa in despesas)
            {
                somadespesas += despesa.Valor;
            }

            return somalucros - somadespesas;
        }
    }
}
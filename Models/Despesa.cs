using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanCtrl.Models
{
    public class Despesa
    {
        public float Valor { get; set; }
        public string Tipo { get; set; }
        public string FormaDePagamento { get; set; }
        public string Descricao { get; set; }
        public string Data { get; set; }
        public Despesa(float valor, string tipo, string formadepagamento, string descricao)
        {
            Valor = valor;
            Tipo = tipo;
            FormaDePagamento = formadepagamento;
            Descricao = descricao;
            Data = DateTime.Now.ToString("yyyy-MM-dd");
        }
        public override string ToString()
        {
            return $"Valor: {Valor} | Categoria: {Tipo} | Forma de pagamento: {FormaDePagamento} | Descrição: {Descricao} | Data: {Data}";
        }
    }
}
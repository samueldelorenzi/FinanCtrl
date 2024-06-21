using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanCtrl.Controllers;
using FinanCtrl.Data;
using FinanCtrl.Models;
using FinanCtrl.Utils;

namespace FinanCtrl.Views
{
    enum MenuDespesa { Cadastrar = 1, Listar, Soma, Excluir, Editar, Sair = 0 }
    
    public class DespesaView
    {
        private DespesaController despesaController;
        public DespesaView()
        {
            despesaController = new DespesaController();
            this.Init();
        }
        public void Init()
        {
            bool rodar = true;
            do
            {
                Console.WriteLine("Menu despesa");
                Console.WriteLine("--------------");

                Console.WriteLine("1 - Cadastrar despesa");
                Console.WriteLine("2 - Listar despesas");
                Console.WriteLine("3 - Soma das despesas");
                Console.WriteLine("4 - Excluir despesa");
                Console.WriteLine("5 - Editar despesa");
                Console.WriteLine("0 - Retornar");

                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    MenuDespesa opcao = (MenuDespesa)escolha;
                    
                    switch(opcao)
                    {
                        case MenuDespesa.Cadastrar:
                            Console.Clear();
                            CadastrarDespesa();
                            Console.Clear();
                            break;

                        case MenuDespesa.Listar:
                            if (DataSet.despesas.Count != 0)
                                ListarDespesa();
                            else
                                ErroDespesaVazia();
                            Console.Clear();
                            break;
                        
                        case MenuDespesa.Soma:
                            if (DataSet.despesas.Count != 0)
                                SomarDespesa();
                            else
                                ErroDespesaVazia();
                            Console.Clear();
                            break;

                        case MenuDespesa.Excluir:
                            if (DataSet.despesas.Count != 0)
                            {
                                Console.Clear();
                                ExcluirDespesa();
                            }
                            else
                                ErroDespesaVazia();
                            Console.Clear();
                            break;

                        case MenuDespesa.Editar:
                            if (DataSet.despesas.Count != 0)
                            {
                                Console.Clear();
                                EditarDespesa();
                            }
                            else
                                ErroDespesaVazia();
                            Console.Clear();
                            break;

                        case MenuDespesa.Sair:
                            rodar = false;
                            break;
                        
                        default:
                            OpcaoInvalida();
                            break;
                    }
                }
                else
                {
                    OpcaoInvalida();
                }

            } while (rodar); 
        }
        private void CadastrarDespesa()
        {
            Console.WriteLine("Cadastrar despesa");
            Console.WriteLine("---------------");

            Console.Write("Valor: ");

            if(!float.TryParse(Console.ReadLine(), out float valor))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Insira valores válidos!");
                Console.ResetColor();
                Thread.Sleep(1000);
                return;
            }

            Console.Write("Categoria: ");
            string tipo = Console.ReadLine();

            Console.Write("Forma de pagamento: ");
            string formadepagamento = Console.ReadLine();

            Console.Write("Descrição: ");
            string descricao = Console.ReadLine();

            Despesa despesa = new Despesa(valor, tipo, formadepagamento, descricao);

            Console.WriteLine("");
            if (despesaController.Insert(despesa))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sucesso ao cadastrar despesa!");
                Console.ResetColor();

                ExportarDados.ExportDespesa();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao cadastrar despesa!");
                Console.ResetColor();
            }

            
            Thread.Sleep(1000);
        }
        private void ListarDespesa()
        {
            List<Despesa> despesas = despesaController.Get();

            int id = 0;
            Console.WriteLine("----------------------------");
            foreach (Despesa despesa in despesas)
            {
                Console.WriteLine($" Id: {id}");
                Console.WriteLine($" Valor: {despesa.Valor}");
                Console.WriteLine($" Forma de pagamento: {despesa.FormaDePagamento}");
                Console.WriteLine($" Categoria: {despesa.Tipo}");
                Console.WriteLine($" Descrição: {despesa.Descricao}");
                Console.WriteLine($" Data: {despesa.Data}");
                Console.WriteLine("----------------------------");
                id++;
            }
            Console.WriteLine("Aperte ENTER para retornar");
            Console.ReadLine();
        }
        private void SomarDespesa()
        {
            List<Despesa> despesas = despesaController.Get();

            float somadasdespesas = 0;
            foreach (Despesa despesa in despesas)
            {
                somadasdespesas += despesa.Valor;
            }

            Console.WriteLine($"A soma das suas despesas cadastradas é igual a R${somadasdespesas}");

            Console.WriteLine("");
            Console.WriteLine("Pressione ENTER para retornar");
            Console.ReadLine();
        }
        private void ExcluirDespesa()
        {
            Console.WriteLine("Excluir despesa");
            Console.WriteLine("-------------");
            Console.Write("Informe a Id da despesa que deseja excluir: ");


            if(!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ocorreu um erro ao deletar a despesa!");
                Console.ResetColor();
            }
            else
            {
                if (despesaController.Delete(id))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Sucesso ao deletar a despesa!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ocorreu um erro ao deletar a despesa!");
                    Console.ResetColor();
                }
            }
            Thread.Sleep(1000); 
        }
        private void ErroDespesaVazia()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Você não possui nenhuma despesa cadastrada!");
            Console.ResetColor();
            
            Thread.Sleep(1000);
        }
        static void OpcaoInvalida()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Opção inválida! Tente novamente.");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
        }
        private void EditarDespesa()
        {
            Console.WriteLine("Editar despesa");
            Console.WriteLine("------------");
            Console.Write("Informe a Id da despesa que deseja editar: ");

            if(!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Insira valores válidos!");
                Console.ResetColor();
                Thread.Sleep(1000);
                return;
            }

            if (DataSet.despesas.ElementAtOrDefault(id) != null)
            {
                Console.Write("Novo Valor: ");
                float valor = float.Parse(Console.ReadLine());

                Console.Write("Nova Categoria: ");
                string tipo = Console.ReadLine();

                Console.Write("Nova Forma de pagamento: ");
                string formadepagamento = Console.ReadLine();

                Console.Write("Nova Descrição: ");
                string descricao = Console.ReadLine();

                Despesa despesa = new Despesa(valor, tipo, formadepagamento, descricao);

                if (despesaController.Update(despesa, id))
                    Console.WriteLine("Sucesso ao editar despesa");
                else
                    Console.WriteLine("Erro ao editar despesa");
            }
            else
                Console.WriteLine("A Id informada não existe");

            Thread.Sleep(1000);
        }
    }
}
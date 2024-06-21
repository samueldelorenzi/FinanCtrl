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
    enum MenuLucro { Cadastrar = 1, Listar, Soma, Excluir, Editar, Sair = 0 }
    public class LucroView
    {
        private LucroController lucroController;
        public LucroView()
        {
            lucroController = new LucroController();
            this.Init();
        }
        public void Init()
        {
            bool rodar = true;
            do
            {
                Console.WriteLine("Menu lucro");
                Console.WriteLine("--------------");

                Console.WriteLine("1 - Cadastrar lucro");
                Console.WriteLine("2 - Listar lucros");
                Console.WriteLine("3 - Soma dos lucros");
                Console.WriteLine("4 - Excluir lucro");
                Console.WriteLine("5 - Editar lucro");
                Console.WriteLine("0 - Retornar");

                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    MenuLucro opcao = (MenuLucro)escolha;
                    
                    switch(opcao)
                    {
                        case MenuLucro.Cadastrar:
                            Console.Clear();
                            CadastrarLucro();
                            Console.Clear();
                            break;

                        case MenuLucro.Listar:
                            if (DataSet.lucros.Count != 0)
                                ListarLucro();
                            else
                                ErroLucrosVazio();
                            Console.Clear();
                            break;
                        
                        case MenuLucro.Soma:
                            if (DataSet.lucros.Count != 0)
                                SomarLucro();
                            else
                                ErroLucrosVazio();
                            Console.Clear();
                            break;

                        case MenuLucro.Excluir:
                            if (DataSet.lucros.Count != 0)
                            {
                                Console.Clear();
                                ExcluirLucro();
                            }
                            else
                                ErroLucrosVazio();
                            Console.Clear();
                            break;

                        case MenuLucro.Editar:
                            if (DataSet.lucros.Count != 0)
                            {
                                Console.Clear();
                                EditarLucro();
                            }
                            else
                                ErroLucrosVazio();
                            Console.Clear();
                            break;

                        case MenuLucro.Sair:
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
        private void CadastrarLucro()
        {
            Console.WriteLine("Cadastrar lucro");
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

            Lucro lucro = new Lucro(valor, tipo, formadepagamento, descricao);

            Console.WriteLine("");
            if (lucroController.Insert(lucro))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Sucesso ao cadastrar lucro!");
                Console.ResetColor();
                
                ExportarDados.ExportLucro();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Erro ao cadastrar lucro!");
                Console.ResetColor();
            }            
            Thread.Sleep(1000);
        }
        private void ListarLucro()
        {
            List<Lucro> lucros = lucroController.Get();

            int id = 0;
            Console.WriteLine("----------------------------");
            foreach (Lucro lucro in lucros)
            {
                Console.WriteLine($" Id: {id}");
                Console.WriteLine($" Valor: {lucro.Valor}");
                Console.WriteLine($" Forma de pagamento: {lucro.FormaDePagamento}");
                Console.WriteLine($" Categoria: {lucro.Tipo}");
                Console.WriteLine($" Descrição: {lucro.Descricao}");
                Console.WriteLine($" Data: {lucro.Data}");
                Console.WriteLine("----------------------------");
                id++;
            }
            Console.WriteLine("Aperte ENTER para retornar");
            Console.ReadLine();
        }
        private void SomarLucro()
        {
            List<Lucro> lucros = lucroController.Get();

            float somadoslucros = 0;
            foreach (Lucro lucro in lucros)
            {
                somadoslucros += lucro.Valor;
            }

            Console.WriteLine($"A soma dos seus lucros cadastrados é igual a R${somadoslucros}");

            Console.WriteLine("");
            Console.WriteLine("Pressione ENTER para retornar");
            Console.ReadLine();
        }
        private void ExcluirLucro()
        {
            Console.WriteLine("Excluir lucro");
            Console.WriteLine("-------------");
            Console.Write("Informe a Id do lucro que deseja excluir: ");

            if(!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ocorreu um erro ao deletar a despesa!");
                Console.ResetColor();
            }
            else
            {
                if (lucroController.Delete(id))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Lucro deletado com sucesso!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Erro ao deletar lucro!");
                    Console.ResetColor();
                }
            }
            Thread.Sleep(1000); 
        }
        private void ErroLucrosVazio()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Você não possui nenhum lucro cadastrado!");
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
        private void EditarLucro()
        {
            Console.WriteLine("Editar lucro");
            Console.WriteLine("------------");
            Console.Write("Informe a Id do lucro que deseja editar: ");

            if(!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Insira valores válidos!");
                Console.ResetColor();
                Thread.Sleep(1000);
                return;
            }

            if (DataSet.lucros.ElementAtOrDefault(id) != null)
            {
                Console.Write("Novo Valor: ");
                float valor = float.Parse(Console.ReadLine());

                Console.Write("Nova Categoria: ");
                string tipo = Console.ReadLine();

                Console.Write("Nova Forma de pagamento: ");
                string formadepagamento = Console.ReadLine();

                Console.Write("Nova Descrição: ");
                string descricao = Console.ReadLine();

                Lucro lucro = new Lucro(valor, tipo, formadepagamento, descricao);

                if (lucroController.Update(lucro, id))
                    Console.WriteLine("Sucesso ao editar lucro");
                else
                    Console.WriteLine("Erro ao editar lucro");
            }
            else
                Console.WriteLine("A Id informada não existe");

            Thread.Sleep(1000);
        }
    }
}
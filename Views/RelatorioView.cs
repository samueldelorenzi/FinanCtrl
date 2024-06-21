using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanCtrl.Controllers;
using FinanCtrl.Data;
using FinanCtrl.Models;

namespace FinanCtrl.Views
{
    enum MenuRelatorio { Saldo = 1, MaiorGasto, GastoNoDia, MaiorGanho, GanhoNoDia, Sair = 0 }
    public class RelatorioView
    {
        private RelatorioController relatorioController;
        public RelatorioView()
        {
            relatorioController = new RelatorioController();
            this.Init();
        }
        public void Init()
        {
            bool rodar = true;
            do
            {
                Console.WriteLine("Menu relatório");
                Console.WriteLine("--------------");

                Console.WriteLine("1 - Relatório saldo");
                Console.WriteLine("2 - Relatório maior gasto");
                Console.WriteLine("3 - Relatório gasto no dia");
                Console.WriteLine("4 - Relatório maior ganho");
                Console.WriteLine("5 - Relatório ganho no dia");
                Console.WriteLine("0 - Retornar");

                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    MenuRelatorio opcao = (MenuRelatorio)escolha;
                    
                    switch(opcao)
                    {
                        case MenuRelatorio.Saldo:
                            if (DataSet.despesas.Count != 0 && DataSet.lucros.Count != 0)
                                Saldo();
                            else
                                ErroFaltaDados();
                            Console.Clear();
                            break;

                        case MenuRelatorio.MaiorGasto:
                            if (DataSet.despesas.Count != 0)
                                MaiorGasto();
                            else
                                ErroFaltaDados();
                            Console.Clear();
                            break;

                        case MenuRelatorio.MaiorGanho:
                            if (DataSet.lucros.Count != 0)
                                MaiorGanho();
                            else
                                ErroFaltaDados();
                            Console.Clear();
                            break;
                        
                        case MenuRelatorio.GastoNoDia:
                            if (DataSet.despesas.Count != 0)
                            {
                                Console.Clear();
                                GastoNoDia();
                            }
                            else
                                ErroFaltaDados();
                            Console.Clear();
                            break;

                        case MenuRelatorio.GanhoNoDia:
                            if (DataSet.lucros.Count != 0)
                            {
                                Console.Clear();
                                GanhoNoDia();
                            }
                            else
                                ErroFaltaDados();
                            Console.Clear();
                            break;

                        case MenuRelatorio.Sair:
                            rodar = false;
                            break;
                        
                        default:
                            Console.WriteLine("Opção inválida");
                            Thread.Sleep(1000);
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida");
                    Thread.Sleep(1000);
                    Console.Clear();
                }

            } while (rodar); 
        }
        private void ErroFaltaDados()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Você não possui dados suficientes para gerar esse relatório");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
        private void Saldo()
        {
            Console.WriteLine("Após um cálculo detalhado analisando seus lucros e despesas cadastrados...");
            Console.Write($"Seu saldo é de ");

            float saldo = relatorioController.RetornarSaldo();

            if (saldo > 0)
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"R${saldo}");

            Console.ResetColor();

            Console.WriteLine("");
            Console.WriteLine("Pressione ENTER para retornar...");
            Console.ReadLine();
        }
        private void MaiorGasto()
        {
            Despesa maiorgasto = relatorioController.MaiorGasto();

            Console.WriteLine("O seu maior gasto registrado foi:");
            Console.WriteLine($"Valor: R${maiorgasto.Valor}");
            Console.WriteLine($"No dia: {maiorgasto.Data}");
            Console.WriteLine($"Categoria: {maiorgasto.Tipo}");
            Console.WriteLine($"E pagou usando: {maiorgasto.FormaDePagamento}");

            Console.WriteLine("");
            Console.WriteLine("Pressione ENTER para retornar...");
            Console.ReadLine();
        }
        private void GastoNoDia()
        {
            Console.WriteLine("Gasto no dia");
            Console.WriteLine("------------");
            Console.Write("Qual dia gostaria de consultar? (AAAA-MM-DD): ");
            string data = Console.ReadLine();

            float gastosnodia = relatorioController.GastoNoDia(data);

            if(gastosnodia == 0)
            {
                Console.WriteLine("Dados inexistentes");
                Thread.Sleep(1000);
                return;
            }

            Console.WriteLine($"No dia {data} você gastou R${gastosnodia}");
            
            Console.WriteLine("");
            Console.WriteLine("Pressione ENTER para retornar...");
            Console.ReadLine();
        }
        private void MaiorGanho()
        {
            Lucro maiorganho = relatorioController.MaiorGanho();

            Console.WriteLine("O seu maior ganho registrado foi:");
            Console.WriteLine($"Valor: R${maiorganho.Valor}");
            Console.WriteLine($"No dia: {maiorganho.Data}");
            Console.WriteLine($"Categoria: {maiorganho.Tipo}");
            Console.WriteLine($"E recebeu através de: {maiorganho.FormaDePagamento}");

            Console.WriteLine("");
            Console.WriteLine("Pressione ENTER para retornar...");
            Console.ReadLine();
        }
        private void GanhoNoDia()
        {
            Console.WriteLine("Ganho no dia");
            Console.WriteLine("------------");
            Console.Write("Qual dia gostaria de consultar? (AAAA-MM-DD): ");
            string data = Console.ReadLine();

            float ganhosnodia = relatorioController.GanhoNoDia(data);

            if(ganhosnodia == 0)
            {
                Console.WriteLine("Dados inexistentes");
                Thread.Sleep(1000);
                return;
            }

            Console.WriteLine($"No dia {data} você ganhou R${ganhosnodia}");

            Console.WriteLine("");
            Console.WriteLine("Pressione ENTER para retornar...");
            Console.ReadLine();
        }
    }
}
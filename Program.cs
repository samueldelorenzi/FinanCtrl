using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanCtrl.Views;
using FinanCtrl.Models;

namespace FinanCtrl
{
    enum Menu { MenuDespesa = 1, MenuLucro, MenuRelatorios, Sair = 0 }
    
    internal class Program
    {
        static void Main()
        {
            Console.Clear();
            Console.WriteLine("Bem vindo ao gerenciador de finanças pessoais FinanCtrl");
            bool rodar = true;
            do
            {
                Console.WriteLine("Menu principal");
                Console.WriteLine("--------------");

                Console.WriteLine("1 - Menu despesa");
                Console.WriteLine("2 - Menu lucro");
                Console.WriteLine("3 - Relatórios");
                Console.WriteLine("0 - Sair");

                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    Menu opcao = (Menu)escolha;
                    
                    switch(opcao)
                    {
                        case Menu.MenuDespesa:
                            Console.Clear();
                            DespesaView despesaView = new DespesaView();
                            Console.Clear();
                            break;

                        case Menu.MenuLucro:
                            Console.Clear();
                            LucroView lucroView = new LucroView();
                            Console.Clear();
                            break;
                        
                        case Menu.MenuRelatorios:
                            Console.Clear();
                            RelatorioView relatorioView = new RelatorioView();
                            Console.Clear();
                            break;

                        case Menu.Sair:
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
        static void OpcaoInvalida()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Opção inválida! Tente novamente.");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.Clear();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
namespace PL
{

    class Program
    {
        static void Main(string[] args)
        {
            BLimp MyBL = new BLimp();// what's the problem

            char choice;
            do
            {
                choice = PLConsole.Menu();
                switch (choice)
                {
                    case 'R':
                        PLConsole.AddGuestRequest();
                        break;
                    case 'H':
                        PLConsole.AddHostingUnit();
                        break;
                    case 'S':
                        PLConsole.ChangeStatusOfOrder();
                        break;
                    case 'O':
                        PLConsole.CreateOrder();
                        break;
                    case 'E': break;
                    default:
                        Console.WriteLine("The command is unknown. Please try again");
                        break;

                }
            } while (choice != 'e');
        }
    }
}

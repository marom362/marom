using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    public class PLConsole
    {
        public static void CreateOrder()
        {
            try
            {
                List<GuestRequest> AllRequests = FactorySingletonBL.GetInstance.GetListOfRequests();
                List<HostingUnit> AllUnits = FactorySingletonBL.GetInstance.GetListOfUnits();
                List<HostingUnit> MyUnits = new List<HostingUnit>();
                int ID = 0;
                bool flag = true;
                do
                {
                    try
                    {
                        Console.WriteLine("Enter Your ID");
                        ID = Int32.Parse(Console.ReadLine());
                        flag = true;
                    }
                    catch (FormatException f)
                    {
                        Console.WriteLine("Your ID is incorrect");
                        flag = false;
                    }
                } while (!flag);
                foreach (HostingUnit unit in AllUnits)
                    if (unit.Owner.HostKey == ID)
                        MyUnits.Add(unit);
                Dictionary<GuestRequest, List<HostingUnit>> possibleOrders = new Dictionary<GuestRequest, List<HostingUnit>>();

                foreach (GuestRequest request in AllRequests)
                    if ((FactorySingletonBL.GetInstance.CreateListOfFittingUnits(request, MyUnits)).Any<HostingUnit>())
                        possibleOrders.Add(request, FactorySingletonBL.GetInstance.CreateListOfFittingUnits(request, MyUnits));
                foreach (KeyValuePair<GuestRequest, List<HostingUnit>> valuePair in possibleOrders)
                    foreach (HostingUnit munit in valuePair.Value)
                        FactorySingletonBL.GetInstance.AddOrder(valuePair.Key, munit);
                List <Order> orders = FactorySingletonBL.GetInstance.GetListOfOrders();
                Console.WriteLine("All the requests for your Hosting Units: ");
                foreach (HostingUnit unit in MyUnits)
                        foreach (Order order in orders)
                        if (order.HostingUnitKey == unit.HostingUnitKey)
                            Console.WriteLine(order.ToString());
                Console.WriteLine("***The end***");




            }
            catch (InvalidOperationException h)
            {
                Console.WriteLine("There are no requests or there are no registered units");
            }
        }

        public static void ChangeStatusOfOrder()
        {
            /*string s = "\0"*/
            bool flag = false;
            int desiredStatus = 0;
            try
            {
                Order currentOrder = GetOrder();
                Console.WriteLine("To send an email enter 1, to close because the client was not interested enter 2, to close because the deal was made enter 3");
                do
                {
                    try
                    {
                        desiredStatus = Int32.Parse(Console.ReadLine());
                        flag = true;
                    }
                    catch(FormatException g)
                    {
                        flag = false;
                        Console.WriteLine("Invalid input. ");
                    }
                } while (!flag);
                GuestRequest guestRequest = new GuestRequest();
                //FactorySingletonBL.GetInstance.ChangeStatusOfOrder(currentOrder, (StatusO)desiredStatus);
                int comission = 0;
                List<GuestRequest> requests = FactorySingletonBL.GetInstance.GetListOfRequests();
                foreach (GuestRequest request in requests)
                    if (request.GuestRequestKey == currentOrder.GuestRequestKey)
                        guestRequest = request;

                if (desiredStatus == 1)
                    FactorySingletonBL.GetInstance.SendingMail(currentOrder, guestRequest);
                if (desiredStatus ==2||desiredStatus==3)
                {
                    comission = FactorySingletonBL.GetInstance.ClosingOrder(currentOrder, guestRequest, (StatusO)desiredStatus);
                    if (comission > 0)
                        Console.WriteLine("The deal was successfully closed. The comission is {0} shekels", comission);
                    else Console.WriteLine("Te order was successfully closed");
                }
            }
            catch (InvalidOperationException)
            {
                do
                {
                    Console.WriteLine("No Orders are registered. Do you want to add an order?");
                    string s = Console.ReadLine();
                    if (s == "yes")
                    {
                        CreateOrder();
                        flag = true;
                    }
                    else if (s == "no")
                        return;
                    else
                    {
                        Console.WriteLine("No match found. Please enter 'yes' or 'no'");
                        flag = false;
                    }
                } while (!flag);

            }       
        }
        private static Order GetOrder()
        {
            try
            {
                bool flag = false;
                bool flag2 = true;
                int numOfOrder = 0;
                do
                {
                    do
                    {
                        Console.WriteLine("Enter the number of order you want to change the status of ");
                        try
                        {
                            numOfOrder = Int32.Parse(Console.ReadLine());
                            flag = true;
                        }
                        catch (FormatException f)
                        {
                            Console.WriteLine("The format is incorrect. ");
                            flag = false;
                        }
                    } while (!flag);
                    List<Order> orders = FactorySingletonBL.GetInstance.GetListOfOrders();
                    foreach (Order order in orders)
                        if (order.OrderKey == numOfOrder)
                        {
                            flag2 = true;
                            return order;
                            
                        }
                    Console.WriteLine("The entered number does not exist.");
                    flag2 = false;
                } while (!flag2);
            }
            catch (InvalidOperationException j) { throw j; }
            return new Order();
            
        }
        public static void AddHostingUnit()
        {
            try
            {
                HostingUnit unit = new HostingUnit();
                bool flag = true;
                Console.WriteLine("Enter the name of your unit");
                unit.HostingUnitName = Console.ReadLine();
                do
                {
                    Console.WriteLine("Enter the  area of your unit (enter 0 for all, 1 for North, 2 for South, 3 for Center, 4 for Jerusalem");
                    try
                    {
                        int en = Int32.Parse(Console.ReadLine());
                        if (en >= 0 && en <= 4)
                        {
                            unit.Area = (Areas)en;
                            flag = true;
                        }
                        else
                        {
                            Console.WriteLine("The input is incorrect");
                            flag = false;
                        }
                    }
                    catch (FormatException f)
                    {
                        Console.WriteLine("The input contains invalid digits");
                        flag = false;
                    }
                } while (!flag);
                Console.WriteLine("Enter the SubArea of your unit");
                unit.SubArea = Console.ReadLine();
                do
                {
                    Console.WriteLine("Enter the desired type of unit: 0 for zimmer, 1 for hotel, 2 for camping");
                    try
                    {
                        int en = Int32.Parse(Console.ReadLine());
                        if (en >= 0 && en <= 2)
                        {
                            unit.Type = (Types)en;
                            flag = true;
                        }
                        else
                        {
                            Console.WriteLine("The input is incorrect");
                            flag = false;
                        }
                    }
                    catch (FormatException f)
                    {
                        Console.WriteLine("The input contains invalid digits");
                        flag = false;
                    }
                } while (!flag);

                do
                {
                    Console.WriteLine("Does your unit have a jacuzzi?");
                    string s = Console.ReadLine();
                    if (s == "yes")
                    {
                        unit.Jacuzz = true;
                        flag = true;
                    }
                    else if (s == "no")
                    {
                        unit.Jacuzz = false;
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine("Your answer is invalid");
                        flag = false;
                    }
                } while (!flag);
                do
                {
                    Console.WriteLine("Does your unit have a swimming pool?");
                    string s = Console.ReadLine();
                    if (s == "yes")
                    {
                        unit.Pool = true;
                        flag = true;
                    }
                    else if (s == "no")
                    {
                        unit.Pool = false;
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine("Your answer is invalid");
                        flag = false;
                    }
                } while (!flag);

                //unit.Owner = CreateOwner();
                Host host = new Host();
                do
                {
                    try
                    {
                        Console.WriteLine("Enter Your ID");
                        host.HostKey = Int32.Parse(Console.ReadLine());
                        flag = true;
                    }
                    catch (FormatException f)
                    {
                        Console.WriteLine("Your ID is incorrect");
                        flag = false;
                    }
                } while (!flag);
                try
                {
                    List<HostingUnit> units = FactorySingletonBL.GetInstance.GetListOfUnits();
                    foreach (HostingUnit kunit in units)
                        if (kunit.Owner.HostKey == host.HostKey)

                            unit.Owner = kunit.Owner;

                        else unit.Owner = creatingHost(host.HostKey);
                }
                catch (InvalidOperationException j)
                {
                    unit.Owner = creatingHost(host.HostKey);
                }
                FactorySingletonBL.GetInstance.AddHostingUnit(unit);
                Console.WriteLine("The unit was successfully added");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("This unit alredy exists in the system");
            }

        }

        public static Host creatingHost (int key)
        {
            bool flag = true;
            Host host = new Host();
            host.HostKey = key;
            Console.WriteLine("Enter Your Name");
            host.PrivateName = Console.ReadLine();
            Console.WriteLine("Enter Your Family Name");
            host.FamilyName = Console.ReadLine();
            do
            {
                Console.WriteLine("Enter Your Email");
                host.MailAddress = Console.ReadLine();
                foreach (char letter in host.MailAddress)
                {
                    if (letter == '@')
                        flag = true;
                }
                if (!flag)
                    Console.WriteLine("Email is incorrect.");
            } while (!flag);
            bool cor = true;
            do
            {

                Console.WriteLine("Enter your phone number");
                host.FhoneNumber = Console.ReadLine();
                foreach (char letter in host.FhoneNumber)
                    if (letter < '0' && letter > '9')
                        cor = false;
                if (!cor)
                    Console.WriteLine("Your number contains illegal characters");
            } while (!cor);
            host.bankBranch = CreateBankBranchDetails();
            cor = true;
            do
            {
                try
                {
                    Console.WriteLine("Enter the number of your account");
                    host.BankAccountNumber = Int32.Parse(Console.ReadLine());
                    cor = true;
                }
                catch (FormatException g)
                {
                    Console.WriteLine("The format is incorrect");
                    cor = false;
                }
            } while (!cor);
            host.CollectionClearance = false;
            return host;


        }

        public static void AddGuestRequest()
        {
            GuestRequest request = new GuestRequest();
            Guest guest = new Guest();
            bool flag = false;
            do
            {
                try
                {
                    Console.WriteLine("Enter Your ID");
                    guest.ID = Int32.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (FormatException f)
                {
                    Console.WriteLine("Your ID is incorrect");
                    flag = false;
                }
            } while (!flag);
            Console.WriteLine("Enter Your Name");
            guest.PrivateName = Console.ReadLine();
            Console.WriteLine("Enter Your Family Name");
            guest.FamilyName = Console.ReadLine();
            do
            {
                Console.WriteLine("Enter Your Email");
                guest.MailAddress = Console.ReadLine();
                foreach (char letter in guest.MailAddress)
                {
                    if (letter == '@')
                        flag = true;
                }
                if (!flag)
                    Console.WriteLine("Email is incorrect.");
            } while (!flag);
            bool cor = true;
            do
            {
                
                Console.WriteLine("Enter your phone number");
                guest.PhoneNumber = Console.ReadLine();
                foreach (char letter in guest.PhoneNumber)
                    if (letter < '0' && letter > '9')
                        cor = false;
                if (!cor)
                    Console.WriteLine("Your number contains illegal characters");
            } while (!cor);
            guest.BankBranchDetails = CreateBankBranchDetails();
            cor = true;
            do
            {
                try
                {
                    Console.WriteLine("Enter the number of your account");
                    guest.BankAccountNumber = Int32.Parse(Console.ReadLine());
                    cor = true;
                }
                catch (FormatException g)
                {
                    Console.WriteLine("The format is incorrect");
                    cor = false;
                }
            } while (!cor);
            try
            {
                FactorySingletonBL.GetInstance.AddGuest(guest);
            }
            catch (InvalidOperationException k) { }
            guest = FactorySingletonBL.GetInstance.GetGuest(guest.ID);
            request.Status = StatusGR.Open;
            do
            {
                Console.WriteLine("Enter the entry date");
                try
                {
                    request.EntryDate = DateTime.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (FormatException f)
                {
                    Console.WriteLine("The date is in incorrect format");
                    flag = false;
                }
            } while (!flag);
            do
            {
                Console.WriteLine("Enter the release date");
                try
                {
                    request.ReleaseDate = DateTime.Parse(Console.ReadLine());// Don't know what's the problem
                    flag = true;
                }
                catch (FormatException k)
                {
                    Console.WriteLine("The date is in incorrect format");
                    flag = false;
                }
            } while (!flag);
            do
            {
                Console.WriteLine("Enter the desired area (enter 0 for all, 1 for North, 2 for South, 3 for Center, 4 for Jerusalem");
                try
                {
                    int en = Int32.Parse(Console.ReadLine());
                    if (en >= 0 && en <= 4)
                    {
                        request.Area = (Areas)en;
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine("The input is incorrect");
                        flag = false;
                    }
                }
                catch (FormatException f)
                {
                    Console.WriteLine("The input contains invalid digits");
                    flag = false;
                }
            } while (!flag);
            Console.WriteLine("Enter the desired subarea");
            request.SubArea = Console.ReadLine();
            do
            {
                Console.WriteLine("Enter the desired type of unit: 0 for zimmer, 1 for hotel, 2 for camping");
                try
                {
                    int en = Int32.Parse(Console.ReadLine());
                    if (en >= 0 && en <= 2)
                    {
                        request.Type = (Types)en;
                        flag = true;
                    }
                    else
                    {
                        Console.WriteLine("The input is incorrect");
                        flag = false;
                    }
                }
                catch (FormatException f)
                {
                    Console.WriteLine("The input contains invalid digits");
                    flag = false;
                }
            } while (!flag);
            do
            {
                Console.WriteLine("Enter the num of adults");
                try
                {
                    request.Adults = Int32.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (FormatException f)
                {
                    Console.WriteLine("The input contains invalid digits");
                    flag = false;
                }
            } while (!flag);
            do { 
                Console.WriteLine("Enter the number of children");
                try
                {
                    request.Children = Int32.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (FormatException f)
                {
                    Console.WriteLine("The input contains invalid digits");
                    flag = false;
                }
            } while (!flag);
            do
            {
                Console.WriteLine("Do you want a jacuzzi? Enter 0 to 'surely', 1 to 'maybe', 2 for 'not at all'");
                try {
                    int en = Int32.Parse(Console.ReadLine());
                    if (en >= 0 && en <= 2)
                    {
                        request.Jacuzz = (Options)en;// change the Options enum
                        flag = true;
                    }
                    else {
                        flag = false;
                        Console.WriteLine("The input is incorrect");
                    }
                }
                catch (FormatException f)
                {
                    flag = false; 
                    Console.WriteLine("The input is invalid");
                }
            } while (!flag);
            do
            {
                Console.WriteLine("Do you want a swimming pool? Enter 0 to 'surely', 1 to 'maybe', 2 for 'not at all'");
                try
                {
                    int en = Int32.Parse(Console.ReadLine());
                    if (en >= 0 && en <= 2)
                    {
                        request.Pool = (Options)en;// change the Options enum
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                        Console.WriteLine("The input is incorrect");
                    }
                }
                catch (FormatException f)
                {
                    flag = false;
                    Console.WriteLine("The input is invalid");
                }
            } while (!flag);
            try
            {
                FactorySingletonBL.GetInstance.AddRequest(request);
                Console.WriteLine("Your request was successfully added. Please wait");
            }
            catch (InvalidOperationException j)
            {
                Console.WriteLine("Your request already exists. Please wait");
            }

        }

        private static BankBranch CreateBankBranchDetails()
        {
            BankBranch branch = new BankBranch();
            bool flag = true;
            do
            {
                Console.WriteLine("Enter the number of your bank");
                try
                {
                    branch.BankNumber = Int32.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (FormatException g)
                {
                    Console.WriteLine("Your bank number is incorrect because it contains illegal characters");
                    flag = false;
                }
            } while (!flag);
            Console.WriteLine("Enter the name of your bank");
            branch.BankName = Console.ReadLine();
            do
            {
                Console.WriteLine("Enter the number of branch");
                try
                {
                    branch.BranchNumber = Int32.Parse(Console.ReadLine());
                    flag = true;
                }
                catch (FormatException g)
                {
                    Console.WriteLine("Your branch number is incorrect because it contains illegal characters");
                    flag = false;
                }
            } while (!flag);
            Console.WriteLine("Enter the address of your branch");
            branch.BranchAddress = Console.ReadLine();
            Console.WriteLine("Enter the city of your branch");
            branch.BranchCity = Console.ReadLine();
            return branch;
            }

        public static char Menu()
        {
            Console.WriteLine("For adding a request for holiday enter r, \nfor adding a hosting unit enter h, \nto change the status of an order enter s, \n to create an order enter o, \n to exit enter e ");
            
            //Console.WriteLine((char)Console.ReadKey().Key);
            return (char)Console.ReadKey().Key;
        }
    }
}
    


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.IO;
using BE;
using System.Threading;
using System.Xml;


namespace DAL
{
    public class XMLDAL : Idal
    {

        //DataBase

        static readonly string ProjectPath = @"C:\Users\marom\Documents\מתמטיקה-סמסטר אלול\Project_3289_4525_doteNet5780\DAL";//path of xml files
        XElement configRoot;
        private readonly string configPath = ProjectPath + "/Data/config.xml";
        XElement Orders;
        private readonly string OrdersPath = ProjectPath + "/Data/Orders.xml";
        XElement GuestRequests;
        private readonly string GuestRequestsPath = ProjectPath + "/Data/GuestRequests.xml";
        XElement Hosts;
        private readonly string HostsPath = ProjectPath + "/Data/Hosts.xml";
        XElement HostingUnits;
        private readonly string HostingUnitsPath = ProjectPath + "/Data/HostingUnits.xml";
        XElement BankBranches;
        private readonly string BankBranchesPath = ProjectPath + "/Data/BankBranches.xml";
        XElement Guests;
        XElement aaas;
        private readonly string aaasPath = ProjectPath + "/Data/aaa.xml";
        private readonly string GuestsPath = ProjectPath + "/Data/Guests.xml";
        private static List<Order> ListOfOrders = new List<Order>();
        private static List<aaa> ListOfaaas = new List<aaa>();
        private static List<GuestRequest> ListOfRequests = new List<GuestRequest>();
        private List<Host> ListOfHosts = new List<Host>();
        private static List<HostingUnit> ListOfUnits = new List<HostingUnit>();
        private static List<BankBranch> ListOfBranches = new List<BankBranch>();
        private static List<Guest> ListOfGuests = new List<Guest>();
        int TotalComission = 0;
        int CurrentUnitNum;
        int CurrentRequestNum;
        int CurrentOrderNum;
        int ComissionPerDay;
        


        //methods
        //ctor
        public /*internal*/ XMLDAL()
        {
            if (!File.Exists(configPath))
            {
                //Console.WriteLine("KEK");

                SaveConfigToXml();
            }
            else
            {
                //SaveConfigToXml();
                GetCurrentNumber();
                configRoot = XElement.Load(configPath);
                Configuration.HostingUnitKey = Convert.ToInt32(configRoot.Element("HostingUnitKey").Value);
                Configuration.GuestRequestKey = Convert.ToInt32(configRoot.Element("GuestRequestKey").Value);
                Configuration.OrderKey = Convert.ToInt32(configRoot.Element("OrderKey").Value);
                try
                {
                    Configuration.NumCommission = Convert.ToInt32(configRoot.Element("NumComission"));
                }
                catch (InvalidCastException)
                {
                    //maybe I'll not write it at all?
                }
                try
                {
                    TotalComission = Convert.ToInt32(configRoot.Element("TotalComission"));
                }
                catch (InvalidCastException) { /*do we need smth here*/}


            }
            if (!File.Exists(OrdersPath))
            {
                Orders = new XElement("Orders");
                Orders.Save(OrdersPath);

            }
            else
            //ListOfOrders = 
            if (!File.Exists(GuestRequestsPath))
            {
                GuestRequests = new XElement("GuestRequests");
                GuestRequests.Save(GuestRequestsPath);
            }
            if (!File.Exists(HostsPath))
            {
                Hosts = new XElement("Hosts");
                Hosts.Save(HostsPath);
            }

            if (!File.Exists(HostingUnitsPath))
            {
                HostingUnits = new XElement("HostingUnits");
                HostingUnits.Save(HostingUnitsPath);
            }
            if (!File.Exists(aaasPath))
            {
                aaas = new XElement("aaas");
                aaas.Save(aaasPath);
            }
            if (!File.Exists(GuestsPath))
            {
                Guests = new XElement("Guests");
                Guests.Save(GuestsPath);
            }
            if (!File.Exists(BankBranchesPath))
            {
                BankBranches = new XElement("BankBranches");
                //foreach(BankBranch branch in )
               
                BankBranches.Save(BankBranchesPath);
            }
            try
            {
                ListOfGuests = LoadFromXML<List<Guest>>(GuestsPath);
            }
            catch (InvalidOperationException)
            {
                ListOfGuests = new List<Guest>();
            }
            try
            {
                ListOfBranches = LoadFromXML<List<BankBranch>>(BankBranchesPath);
            }
            catch (InvalidOperationException)
            {

            }
            try
            {
                ListOfHosts = LoadFromXML<List<Host>>(HostsPath);
            }
            catch (InvalidOperationException)
            {
                ListOfHosts = new List<Host>();
            }
            
            try
            {
                ListOfOrders = LoadFromXML<List<Order>>(OrdersPath);
            }
            catch (InvalidOperationException)
            {
                ListOfOrders = new List<Order>();
            }
            try
            {
                ListOfRequests = LoadFromXML<List<GuestRequest>>(GuestRequestsPath);
            }
            catch (InvalidOperationException)
            {
                ListOfRequests = new List<GuestRequest>();
            }
            try
            {
                ListOfaaas = LoadFromXML<List<aaa>>(aaasPath);
            }
            catch (InvalidOperationException)
            {
                ListOfaaas = new List<aaa>();
            }
            try
            {
                ListOfUnits = LoadFromXML<List<HostingUnit>>(HostingUnitsPath);
            }
            catch (InvalidOperationException)
            {
                ListOfUnits = new List<HostingUnit>();
            }

        }
        //XML needed methods
        private void SaveNewKeys()
        {
            configRoot.RemoveAll();
            //configRoot = new XElement("config");
            configRoot.Add(new XElement("GuestRequestKey", CurrentRequestNum),
                            new XElement("OrderKey", CurrentOrderNum),
                            new XElement("HostingUnitKey", CurrentUnitNum),
                            new XElement("NumComission", ComissionPerDay),
                            new XElement("TotalComission", TotalComission));
            configRoot.Save(configPath);
        }
        public static void SaveToXML<T>(T source, string path)// copied from the last year project
        {
            //if(!File.Exists(path))
            for (int i = 0; i <= 5; i++)
            {
                try
                {
                    FileStream file = new FileStream(path, FileMode.Create);
                    XmlSerializer xmlSerializer = new XmlSerializer(source.GetType());
                    xmlSerializer.Serialize(file, source); file.Close();
                    break; // When done we can break loop
                }
                catch (IOException) when (i <= 5)
                {

                    Thread.Sleep(1000);
                }
            }

        }
        public static T LoadFromXML<T>(string path)
        {
            int i = 0;
            for (; i <= 5; i++)
            {
                //try
                //{


                try
                {
                    FileStream file = new FileStream(path, FileMode.Open);
                    XmlSerializer xmlSer = new XmlSerializer(typeof(T));
                    T result = (T)xmlSer.Deserialize(file);
                    file.Close();
                    return result;
                }
                catch (InvalidOperationException f)
                {
                    throw f;
                }
                catch (IOException)
                {
                    throw new InvalidOperationException();
                }


                //var myFile = File.Open(path, FileMode.Open);
                //myFile.Close();

                //}
                //catch (IOException e) when (i <= 5)
                //{

                //    Thread.Sleep(1000);
                //}
                //LoadFromXML<T>(path);
            }
            throw new InvalidOperationException();


        }

        public void GetCurrentNumber()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);
            // XmlNode node = doc.DocumentElement.SelectSingleNode(configPath+"/config");//what it does?
            int i = 0;
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string text = node.InnerText; //or loop through its children as well
                if (i == 0)
                    CurrentRequestNum = Int32.Parse(text);
                else if (i == 1)
                    CurrentOrderNum = Int32.Parse(text);
                else if (i == 2)
                    CurrentUnitNum = Int32.Parse(text);
                else if (i == 3)
                    ComissionPerDay = Int32.Parse(text);
                else if (i == 4)
                    TotalComission = Int32.Parse(text);
                else break;
                i++;

            }
        }
        private void SaveConfigToXml()
        {
            configRoot = new XElement("config");
            configRoot.Add(new XElement("GuestRequestKey", Configuration.GuestRequestKey),
                            new XElement("OrderKey", Configuration.OrderKey),
                            new XElement("HostingUnitKey", Configuration.HostingUnitKey),
                            new XElement("NumComission", Configuration.NumCommission),
                            new XElement("TotalComission", 0));
            configRoot.Save(configPath);
        }
        private void SaveCurrentConfiguration()
        {
            //configRoot
        }

        //add smth 
        public void AddGuestRequest(GuestRequest gr)
        {
            //var myFile = File
            //myFile.Close();
            try
            {
                if (GetGuestRequest(gr.GuestRequestKey) == null)
                {
                    gr.RegistrationDate = DateTime.Today;
                    gr = Copy(gr);
                    ListOfRequests.Add(gr);//is it ok?
                    SaveToXML<List<GuestRequest>>(ListOfRequests, GuestRequestsPath);
                    /*System.Xml.Serialization.XmlSerializer req = new System.Xml.Serialization.XmlSerializer(typeof(GuestRequest));
                    System.IO.FileStream file = System.IO.File.OpenWrite(GuestRequestsPath);
                    req.Serialize(file, gr);
                    if (GuestRequests == null)
                        GuestRequests = new XElement("GuestRequests");
                    GuestRequests.Add(gr);
                    //GuestRequests.Save(GuestRequestsPath);

    */
                }
                else throw new InvalidOperationException("Request Already Exists");//when the program'll get here?
            }
            catch (IOException) { }
        }
        public void AddHostingUnit(HostingUnit hu)
        {
            try
            {
                if (GetHostingUnit(hu.HostingUnitKey) == null)
                {
                    //hu = Copy(hu);
                    List<HostingUnit> units = GetAllHostingUnits();
                    units.Add(hu);
                    SaveToXML<List<HostingUnit>>(ListOfUnits, HostingUnitsPath);
                    if (HostingUnits == null)
                    {
                        HostingUnits = new XElement("HostingUnits");
                        HostingUnits.Save(HostingUnitsPath);
                    }
                    if (HostingUnits.FirstNode == null)//ifthe file is empty
                    {
                        // SaveToXML<List<HostingUnit>>(ListOfUnits, HostingUnitsPath);
                        System.Xml.Serialization.XmlSerializer hostU = new System.Xml.Serialization.XmlSerializer(typeof(HostingUnit));
                        System.IO.FileStream file = System.IO.File.OpenWrite(HostingUnitsPath);
                        //StreamWriter myWriter = new StreamWriter(HostingUnitsPath);
                        hostU.Serialize(file, hu);
                        //hostU.Serialize(, hu);
                        //ListOfUnits.Add(hu);
                    }
                    HostingUnits.Add(hu);

                    SaveToXML<List<HostingUnit>>(ListOfUnits, HostingUnitsPath);
                    //foreach (HostingUnit un in units)
                    //{
                    //    System.Xml.Serialization.XmlSerializer hostU = new System.Xml.Serialization.XmlSerializer(typeof(HostingUnit));
                    //    System.IO.FileStream file = System.IO.File.OpenWrite(HostingUnitsPath);
                    //    hostU.Serialize(file, )

                    //}
                }

                else throw new InvalidOperationException("Unit already exists");
            }
            catch (IOException)
            { }
        }
        public void AddOrder(Order order)
        {
            if (GetOrder(order.OrderKey) == null)
            {
                order.OrderKey = ++Configuration.OrderKey;
                order = Copy(order);
                ListOfOrders.Add(order);
                SaveToXML<List<Order>>(ListOfOrders, OrdersPath);
                /*System.Xml.Serialization.XmlSerializer or = new System.Xml.Serialization.XmlSerializer(typeof(Order));
                System.IO.FileStream file = System.IO.File.OpenWrite(OrdersPath);
                or.Serialize(file, order);*/

            }
            else throw new InvalidOperationException("Order already exists");
        }

        public void AddHost(Host host)
        {
            if (GetHost(host.HostKey) == null)
            {
                ListOfHosts.Add(Copy(host));
                SaveToXML<List<Host>>(ListOfHosts, HostsPath);
            }
            else throw new InvalidOperationException("Host already exists");
        }
        //Get One object of type
        public GuestRequest GetGuestRequest(int key)
        {
            return ListOfRequests.FirstOrDefault(o => o.GuestRequestKey == key);
        }
        public Order GetOrder(int key)
        {
            return ListOfOrders.FirstOrDefault(o => o.OrderKey == key);
        }
        public Host GetHost(int key)
        {
            return ListOfHosts.FirstOrDefault(h => h.HostKey == key);
        }
        public HostingUnit GetHostingUnit(int key)
        {
            return ListOfUnits.FirstOrDefault(h => h.HostingUnitKey == key);
        }

        //get lists of objects
        public List<Host> GetAllHosts()
        {
            return ListOfHosts;//I've no idea whether it works
        }
        public List<Order> GetAllOrders()
        {
            return ListOfOrders;//I've no idea whether it works
        }
        public List<GuestRequest> GetAllGuestRequests()
        {
            return ListOfRequests; //I've no idea whether it works
        }
        public List<HostingUnit> GetAllHostingUnits()
        {
           // return ListOfUnits;//I've no idea whether it works
            LoadData();
            List<HostingUnit> ArrayOfHostingUnits;
            try
            {
                ArrayOfHostingUnits = (from p in HostingUnits.Elements()
                                       select new HostingUnit()
                                       {
                                           HostingUnitKey = Convert.ToInt32(p.Element("HostingUnitKey").Value),
                                           HostingUnitName =p.Element("HostingUnitName").Value,
                                           //Type = p.Element("Type", Typ).Value,
                            }).ToList();
            }
            catch
            {
                ArrayOfHostingUnits = null;
            }
            return ArrayOfHostingUnits;
        }
        private void LoadData()
        {
            try
            {
                HostingUnits = XElement.Load(HostingUnitsPath);
            }
            catch
            {
                Console.WriteLine("File upload problem");
            }
        }
        public List<BankBranch> GetAllBankBranches()
        {
            return ListOfBranches;//I've no idea whether it workss
        }
        public List<Guest> GetAllGuests()
        {
            return ListOfGuests;
        }
        //Copy methods
        private GuestRequest Copy(GuestRequest request)
        {
            GuestRequest temp = new GuestRequest();
            temp.Garden = request.Garden;
            temp.Adults = request.Adults;
            temp.Area = request.Area;
            temp.Children = request.Children;
            temp.ChildrensAttractions = request.ChildrensAttractions;
            temp.EntryDate = request.EntryDate;
            temp.guest = request.guest;
            temp.GuestRequestKey = CurrentRequestNum;
            CurrentRequestNum++; ;
            temp.Jacuzz = request.Jacuzz;
            temp.NumGuests = request.NumGuests;
            temp.Pool = request.Pool;
            temp.RegistrationDate = request.RegistrationDate;
            temp.ReleaseDate = request.ReleaseDate;
            temp.Status = request.Status;
            temp.SubArea = request.SubArea;
            temp.Type = request.Type;
            configRoot.Save(configPath);
            //SaveConfigToXml();
            SaveNewKeys();
            return temp;



        }

        private HostingUnit Copy(HostingUnit unit)
        {
            HostingUnit temp = new HostingUnit();
            temp.Garden = unit.Garden;
            temp.ChildrenAtraction = unit.ChildrenAtraction;
            temp.Area = unit.Area;
            temp.HostingUnitKey = CurrentUnitNum;
            CurrentUnitNum++;
            temp.HostingUnitName = unit.HostingUnitName;
            temp.Jacuzz = unit.Jacuzz;
            temp.numOfMaxGuests = unit.numOfMaxGuests;
            temp.Owner = unit.Owner;
            temp.Pool = unit.Pool;
            temp.SubArea = unit.SubArea;
            temp.Type = unit.Type;
            //bool[,] tempMatrix = new bool[12, 31];
            //for (int i = 0; i < 12; i++)
            //    for (int j = 0; j < 31; j++)
            //        tempMatrix[i, j] = unit.Diary[i, j];
            configRoot.Save(configPath);
            //SaveConfigToXml();
            SaveNewKeys();
            return temp;
        }
        private Order Copy(Order order)
        {
            Order temp = new Order();
            temp.CreateDate = order.CreateDate;
            temp.GuestRequestKey = order.GuestRequestKey;
            temp.HostingUnitKey = order.HostingUnitKey;
            temp.OrderDate = order.OrderDate;
            temp.OrderKey = CurrentOrderNum;
            CurrentOrderNum++;
            temp.Status = order.Status;
            configRoot.Save(configPath);
            //SaveConfigToXml();
            SaveNewKeys();
            return temp;
        }
        private Host Copy(Host host)
        {
            Host temp = new Host();
            temp.BankAccountNumber = host.BankAccountNumber;
            temp.bankBranch = host.bankBranch;
            temp.password = host.password;
            temp.CollectionClearance = host.CollectionClearance;
            temp.FamilyName = host.FamilyName;
            temp.FhoneNumber = host.FhoneNumber;
            temp.HostKey = host.HostKey;
            temp.MailAddress = host.MailAddress;
            temp.PrivateName = host.PrivateName;
            return temp;
        }
        //update
        public void UpdatingHostUnit(HostingUnit hu)
        {
            HostingUnit CurrentUnit = GetHostingUnit(hu.HostingUnitKey);
            if (CurrentUnit == null)
                throw new InvalidOperationException("The unit doesn't exist");

            ListOfUnits.Remove(CurrentUnit);
            //hu.HostingUnitKey = CurrentUnit.HostingUnitKey;
            ListOfUnits.Add(hu);
            SaveToXML<List<HostingUnit>>(ListOfUnits, HostingUnitsPath);
        }
        public void UpdatingGuestRequest(GuestRequest gr)
        {
            GuestRequest current = GetGuestRequest(gr.GuestRequestKey);
            if (current == null)
                throw new KeyNotFoundException("The request doesn't exist");
            else
            {
                ListOfRequests.Remove(current);
                //  gr.GuestRequestKey = current.GuestRequestKey;
                ListOfRequests.Add(gr);
                SaveToXML<List<GuestRequest>>(ListOfRequests, GuestRequestsPath);
            }
        }
        public void UpdatingOrder(Order order)
        {
            Order current = GetOrder(order.OrderKey);
            if (current == null)
                throw new KeyNotFoundException("The Order doesn't exist");
            ListOfOrders.Remove(current);
            // order.OrderKey = current.OrderKey
            ListOfOrders.Add(order);
            SaveToXML<List<Order>>(ListOfOrders, OrdersPath);
        }
        public void UpdatingHost(Host host)
        {
            // Host current = GetHost(host.HostKey);
            try
            {
                DelHost(host);
                ListOfHosts.Add(host);
                SaveToXML<List<Host>>(ListOfHosts, HostsPath);
            }
            catch (InvalidOperationException exc)
            {
                throw new KeyNotFoundException(exc.Message);
            }

        }
        // deleting
        public void DelHostingUnit(HostingUnit unit)
        {
            if (!ListOfUnits.Exists(u => u.HostingUnitKey == unit.HostingUnitKey))
                throw new InvalidOperationException("The unit does not exist");
            ListOfUnits.Remove(unit);
            SaveToXML<List<HostingUnit>>(ListOfUnits, HostingUnitsPath);
        }
        public void DelHost(Host host)
        {
            if (ListOfHosts.Exists(h => h.HostKey == host.HostKey))
            {
                //bool d = true;
                // bool deleted = ListOfHosts.Remove(new Host {HostKey = host.HostKey});;
                //foreach (Host hos in ListOfHosts)
                //    Console.WriteLine(hos);

                ListOfHosts.Remove(host);
                SaveToXML<List<Host>>(ListOfHosts, HostsPath);
                foreach (Host hos in ListOfHosts)
                    Console.WriteLine(hos);
            }
            else throw new InvalidOperationException("The host doesn't exist");
        }
        //dtor
        ~XMLDAL()
        {
            configRoot.Save(configPath);
            //SaveToXML<List<Tester>>(testers, testersPath);
            //SaveToXML<List<Test>>(tests, testsPath);
            SaveConfigToXml();
            //? is it enough?
        }

    }
}

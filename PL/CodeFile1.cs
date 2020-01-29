//using System;

//using DAL;
//using BE;
//using System.Collections.Generic;

//namespace TestConsoleApp
//{
//    class Program
//    {
//        public static void Main(string[] args)
//        {
//            //XMLDAL myDal = new XMLDAL();
//            //myDal.SaveConfigToXml();
//            // myDal.GetCurrentNumber();
//            //Guest guest = new Guest();
//            //guest.BankAccountNumber = 15;
//            //guest.FamilyName = "Levi";
//            //GuestRequest gr = new GuestRequest();
//            //gr.Adults = 5;
//            //gr.Type = Types.Hotel;
//            //gr.Area = Areas.Center;
//            //gr.Children = 5;
//            //gr.ChildrensAttractions = Options.NotIntresting;
//            //gr.EntryDate = new System.DateTime(12 / 11 / 2020);
//            //gr.guest = guest;
//            //gr.Status = StatusGR.Open;
//            //gr.Jacuzz = Options.Must;
//            //gr.NumGuests = gr.Adults + gr.Children;
//            //gr.Pool = Options.NotIntresting;
//            //gr.RegistrationDate = DateTime.Today;
//            //gr.SubArea = "Jerusalem";
//            //gr.Garden = Options.NotIntresting;
//            //gr.ReleaseDate = new System.DateTime(15 / 11 / 2020);
//            //FactorySingletonDal.GetInstance.AddGuestRequest(gr);
//            ////Console.WriteLine((FactorySingletonDal.GetInstance.GetGuestRequest(10000007)).ToString());
//            //guest.PhoneNumber = "0537190183";
//            //guest.FamilyName = "Ivanov";
//            //gr.Adults = 8;
//            //gr.Type = Types.Zimmer;
//            //gr.Area = Areas.North;
//            //gr.Children = 2;

//            //gr.ReleaseDate = new System.DateTime(2020, 08, 05);
//            //gr.EntryDate = new System.DateTime(2020, 08, 09);
//            //gr.guest = guest;
//            //FactorySingletonDal.GetInstance.AddGuestRequest(gr);
//            //FactorySingletonDal.GetInstance.AddGuestRequest(gr);
//            //    FactorySingletonDal.GetInstance.AddGuestRequest(gr);
//            //    FactorySingletonDal.GetInstance.AddGuestRequest(gr);
//            //List<GuestRequest> requests = myDal.GetAllGuestRequests();
//            //foreach(GuestRequest r in requests)
//            //{
//            //    Console.WriteLine(r.ToString());
//            //}

//            //Console.WriteLine(gr.RegistrationDate.ToString() + gr.ReleaseDate.ToString() + gr.EntryDate.ToString());
//            //return;
//            //Console.WriteLine((FactorySingletonDal.GetInstance.GetGuestRequest(10000005)).ToString());
//            //Console.WriteLine((FactorySingletonDal.GetInstance.GetGuestRequest(10000006)).ToString());
//            //Console.WriteLine((FactorySingletonDal.GetInstance.GetGuestRequest(10000007)).ToString());
//            //Console.WriteLine((FactorySingletonDal.GetInstance.GetGuestRequest(10000008)).ToString());

//            //Host host = new Host();
//            //host.FamilyName = "Petrov";
//            //host.FhoneNumber = "082550302";
//            //host.HostKey = 333222555;
//            //host.PrivateName = "Ivan";
//            //FactorySingletonDal.GetInstance.AddHost(host);
//            //Host host2 = new Host();
//            //host2.FamilyName = "Sidorov";
//            //host2.HostKey = 323232323;
//            //host2.MailAddress = "ddd@gmail.com";
//            //FactorySingletonDal.GetInstance.AddHost(host2);
//            ////Console.WriteLine(FactorySingletonDal.GetInstance.GetHost(host.HostKey));
//            ////Console.WriteLine(FactorySingletonDal.GetInstance.GetHost(host2.HostKey));
//            //GuestRequest gr = new GuestRequest();
//            //Guest guest = new Guest();
//            //gr.GuestRequestKey = 10000006;
//            //guest.PhoneNumber = "0537190183";
//            //guest.FamilyName = "Ivanov";
//            //gr.Adults = 8;
//            //gr.Type = Types.Hotel;
//            //gr.Area = Areas.South;
//            //gr.Children = 2;

//            //gr.ReleaseDate = new System.DateTime(2020, 08, 05);
//            //gr.EntryDate = new System.DateTime(2020, 08, 09);
//            //gr.guest = guest;
//            //myDal.UpdatingGuestRequest(gr);
//            //Console.WriteLine((FactorySingletonDal.GetInstance.GetGuestRequest(10000006)).ToString());
//            //HostingUnit unit = new HostingUnit();
//            //unit.Area = Areas.North;
//            //unit.ChildrenAtraction = true;
//            //unit.Garden = true;
//            //unit.HostingUnitName = "Leonardo";
//            // //unit.Jacuzz = false;
//            // //unit.numOfMaxGuests = 100;
//            // //unit.Owner = myDal.GetHost(323232323);
//            // //myDal.AddHostingUnit(unit);
//            // //myDal.AddHostingUnit(unit);
//            // //myDal.AddHostingUnit(unit);
//            // //myDal.AddHostingUnit(unit);
//            // Order order = new Order();
//            // order.CreateDate = DateTime.Today;
//            // order.GuestRequestKey = 10000005;
//            // order.HostingUnitKey = 10000038;
//            // order.Status = StatusO.MailSent;
//            // List<Order> myOrders = myDal.GetAllOrders();
//            // //foreach(Order or in myOrders)
//            // //{
//            // //    Console.WriteLine(or.ToString());
//            // //}
//            // //Console.WriteLine(myDal.GetOrder(10000006));
//            // order.OrderKey = 10000006;
//            //myDal.UpdatingOrder(order);
//            Host host = new Host();
//            host.FamilyName = "Alexandrov";
//            host.FhoneNumber = "082540322";
//            host.HostKey = 333222555;
//            host.PrivateName = "Ivan";
//            //FactorySingletonDal.GetInstance.AddHost(host);
//            myDal.DelHost(host);


//        }


//    }

//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public class BLimp : IBL
    {


        /// <summary>
        /// If the same Guest Request doesn't exist, sends it to DAL to add it
        /// </summary>
        /// <param name="request"></param>
        /*public void AddGuest(Guest guest)
        {

            try
            {
                FactorySingletonDal.GetInstance.AddGuest(guest);// if the function exists
            }
            catch (InvalidOperationException x)
            {
                throw x;
            }
        }*/
        /// <summary>
        /// If the same Guest Request doesn't exist, sends it to DAL to add it
        /// </summary>
        /// <param name="request"></param>
        public void AddRequest(GuestRequest request) //not checked
        {
            try
            {
                List<GuestRequest> requests = GetListOfRequests();
                /*if (requests.Contains(request))
                throw new InvalidOperationException();*/
                bool isExist = false;
                foreach (GuestRequest guestRequest in requests)
                    if (guestRequest.GuestRequestKey == request.GuestRequestKey)
                        isExist = true;
                if (isExist)
                    throw new InvalidOperationException();
                else FactorySingletonDal.GetInstance.AddGuestRequest(request);
                AddOrdersForNewRequest(request);
            }
            catch (InvalidOperationException x)
            {
                throw x;
            }
            
        }
        /// <summary>
        /// checks whether the dates are available
        /// </summary>
        /// <param name="GuestRequest"></param>
        /// <param name="Order"></param>
        /// <returns>bool</returns>
        public bool CheckDates(GuestRequest request, HostingUnit unit)
        {

            //HostingUnit unit = FactorySingletonDal.GetInstance.GetHostingUnit(order.HostingUnitKey);
            bool[,] diary = unit.Diary;
            for (int i = request.EntryDate.Month, j = request.EntryDate.Day + 1; i <= request.EntryDate.Month; i++)
                if (i != request.ReleaseDate.Month)
                {
                    for (; j < 31; j++)
                        if (diary[i, j])
                            return false;
                    j = 1;
                }
                else
                {
                    for (; j < request.EntryDate.Day - 1; j++)
                        if (diary[i, j])
                            return false;
                }
            return true;
        }//do not touch
        public void ChangeStatusOfRequest(int numOfRequest, StatusGR status)
        {
            try
            {
                GuestRequest gr = FactorySingletonDal.GetInstance.GetGuestRequest(numOfRequest);
                gr.Status = status;
                FactorySingletonDal.GetInstance.UpdatingGuestRequest(gr);
            }
            catch (InvalidOperationException x)
            {
                throw x;
            }
        }
        /// <summary>
        /// If this Hosting Unit doesn't exist yet, sends it to DAL to add it;
        /// </summary>
        /// <param name="unit"></param>
        public void AddHostingUnit(HostingUnit unit)
        {
            try
            {
                FactorySingletonDal.GetInstance.AddHostingUnit(unit);
                AddOrdersForNewUnit(unit);
            }
            catch (InvalidOperationException x)
            {
                throw x;
            }
        }
        private bool UnitExists(HostingUnit units)
        {
            /* if (FactorySingletonDal.GetInstance.GetAllHostingUnits().Exists(x=> x.HostingUnitKey==units.HostingUnitKey))
                 throw new KeyNotFoundException();
             else return true;*/
            return FactorySingletonDal.GetInstance.GetAllHostingUnits().Exists(x => x.HostingUnitKey == units.HostingUnitKey);
        }
        /// <summary>
        /// Checks whether there are orders for this unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        public bool AppearsInOrders(HostingUnit unit)
        {
            List<Order> orders = GetListOfOrders();
            foreach (Order order in orders)
            {
                if (order.HostingUnitKey == unit.HostingUnitKey)
                    return true;

            }
            return false;

        }
        /// <summary>
        /// checks whether this Hosting Unit exists; if it exisxt, the function sends it to DAL for delition
        /// </summary>
        /// <param name="unit"></param>
        public void RemoveHostingUnit(HostingUnit unit)
        {
            List<HostingUnit> units = GetListOfUnits();

            // checking whether there are open requests

            try
            {
                FactorySingletonDal.GetInstance.DelHostingUnit(unit);
            }
            catch (KeyNotFoundException p)
            {
                throw p;
            }
            catch (InvalidOperationException i)
            {
                throw i;
            }
        }
        /// <summary>
        /// <summary>
        /// If the order dates are correct, sends an order object to DAL
        /// </summary>
        /// <param name="order"></param>
        public void ChangeStatusOfOrder(Order order, StatusO status)
        {
            try
            {
                if (order.Status == StatusO.ClosedByClientsResponse || order.Status == StatusO.ClosedBecauseofClient)
                    throw new InvalidOperationException("This order is closed");
                order.Status = status;
                FactorySingletonDal.GetInstance.UpdatingOrder(order);
            }
            catch (KeyNotFoundException h)// where from???
            {
                throw h;
            }
        }
        //public List<Guest> GetListOfGuest()
        public List<Guest> GetListOfGuest()
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllGuests();
            }
            catch (InvalidOperationException j)
            {
                throw j;
            }
        }
        /*public Guest GetGuest(int ID)
        {
            try
            {
                List<Guest> guests = GetListOfGuest();
                foreach (Guest guest in guests)
                    if (guest.ID == ID)
                        return guest;
                throw new KeyNotFoundException("The guest doesn't exist in system");
            }
            catch (InvalidOperationException j)//the list is empty
            {
                throw j;
            }
        }*/
        /*private Guest GetGuest(Order order)
        {
            try
            {
                List<Guest> guests = GetListOfGuest();
                GuestRequest gr = new GuestRequest();
                List<GuestRequest> requests = GetListOfRequests();
                foreach (GuestRequest request in requests)
                    if (request.GuestRequestKey == order.GuestRequestKey)
                        gr = request;
                foreach (Guest guest in guests)
                    if (guest.ID == gr.guest.ID)
                        return guest;

                //Guest person = guests.Find( == order.id);
                //if (guests.Exists(person))
                //  return person;
                throw new KeyNotFoundException();
            }
            catch (InvalidOperationException j)
            {
                throw j;
            }
        }*/
        /// <summary>
        /// The function that acts to send a mail
        /// </summary>
        /// <param name="order"></param>
        /// <param name="guest"></param>
        public string SendingMail(Order order)
        {
            if (order.Status == StatusO.MailSent)
                throw new Exception("The mail is already sent");
            try
            {
                
                ChangeStatusOfOrder(order, StatusO.MailSent);
                FactorySingletonDal.GetInstance.UpdatingOrder(order);// as if this function of DAL receives the new Status and the num of order;
                return "The mail is sent";
            }
            catch (KeyNotFoundException k)
            {
                throw k;
            }
            catch (InvalidOperationException y)
            {
                throw y;
            }
        }
        /// <summary>
        /// The function that marks in the calendar that the dates are no more available
        /// </summary>
        /// <param name="order"></param>
        /// <param name="entry"></param>
        /// <param name="release"></param>
        private void BookDays(Order order)
        {
            try
            {
                GuestRequest request = FactorySingletonDal.GetInstance.GetGuestRequest(order.GuestRequestKey);
                HostingUnit unit = FactorySingletonDal.GetInstance.GetHostingUnit(order.HostingUnitKey);
                bool[,] diary = unit.Diary;
                for (int i = request.EntryDate.Month, j = request.EntryDate.Day + 1; i <= request.EntryDate.Month; i++)
                    if (i != request.ReleaseDate.Month)
                    {
                        for (; j < 31; j++)
                            diary[i, j] = true;

                        j = 1;
                    }
                    else
                    {
                        for (; j < request.EntryDate.Day - 1; j++)
                            diary[i, j] = true;
                    }
            }
            catch (InvalidOperationException f)
            {
                throw f;
            }
        }
        /// <summary>
        /// The function that closes other orders of the same client
        /// </summary>
        /// <param name="guest"></param>
        private void CloseOtherOrders(GuestRequest request)
        {
            try
            {
                List<Order> orders = GetListOfOrders();
                foreach (Order order in orders)
                {
                    if (order.GuestRequestKey == request.GuestRequestKey)// need to be changed to the correct names of variables
                        ClosingOrder(order, request, StatusO.ClosedBecauseofClient);
                }
            }
            catch (InvalidOperationException h) { throw h; }
            catch (KeyNotFoundException k) { throw k; }
        }
        /// <summary>
        /// Closes the order (because that the client was not interested/because the agreement was made
        /// </summary>
        /// <param name="order"></param>
        /// <param name="guest"></param>
        /// <param name="desiredStatus"></param>
        public List<Order> TheWaitingTimeExpired(int numberOfDays)
        {
            List<Order> orders = GetListOfOrders();
            List<Order> orders1 = new List<Order>();
            foreach (var item in orders)
            {
                double dates = (item.OrderDate - item.CreateDate).TotalDays;// check whether double is ok
                if (dates >= numberOfDays)

                    orders1.Add(item);

            }
            return orders1;
        }
        public int ClosingOrder(Order order, GuestRequest request, StatusO desiredStatus)
        {
            try
            {
                GuestRequest gr = FactorySingletonDal.GetInstance.GetGuestRequest(order.GuestRequestKey);
                if (desiredStatus == (StatusO)2)
                {
                    gr.Status = (StatusGR)2; // if enum is defined
                    order.Status = (StatusO)3;
                }
                else
                {
                    BookDays(order);
                    CloseOtherOrders(request);
                    return NumDaysBetween(gr.EntryDate, gr.ReleaseDate) * Configuration.NumCommission; //DayComission should be defined in Configurations
                }
                return 0; //default code
            }
            catch (KeyNotFoundException k)
            {
                throw k;
            }
            catch (InvalidOperationException g)// from DAL
            {
                throw g;
            }
        }
        /// <summary>
        /// sends an order to other functions that change order status
        /// </summary>
        /// <param name="numOfOrder"></param>
        /// <param name="desiredStatus"></param>

        public List<HostingUnit> GetListOfUnits()
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllHostingUnits();
            }
            catch (InvalidOperationException j)
            {
                throw j;
            }
        }
        /// <summary>
        /// Returns a list of requests
        /// </summary>
        /// <returns></returns>
        public List<GuestRequest> GetListOfRequests()
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllGuestRequests();
            }
            catch (InvalidOperationException b)
            {
                throw b;
            }
        }
        /// <summary>
        /// returns list of orders
        /// </summary>
        /// <returns></returns>
        public List<Order> GetListOfOrders()
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllOrders();
            }
            catch (InvalidOperationException k)
            {
                throw k;
            }
        }
        /// <summary>
        /// returns list of bank branches
        /// </summary>
        /// <returns></returns>
        public List<BankBranch> getListOfBankBranches()
        {
            return FactorySingletonDal.GetInstance.GetAllBankBranches();
        }
        public List<HostingUnit> CreateListOfFittingUnits(GuestRequest request)
        {
            try
            {
                if (request.ReleaseDate.Date <= request.EntryDate.Date)
                    throw new ArgumentException();// implement in UI catch etc for this type of exception that will be translated as "the dates are incorrect"
                else
                {
                    List<HostingUnit> allUnits = GetListOfUnits();
                    List<HostingUnit> units = new List<HostingUnit>();
                    foreach (HostingUnit unit in allUnits)
                        if (IsFitting(unit, request))
                            units.Add(unit);
                    return units;
                }
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }
        }
        public void AddOrdersForNewRequest(GuestRequest request)
        {
            try
            {
                if (request.ReleaseDate.Date <= request.EntryDate.Date)
                    throw new ArgumentException();// implement in UI catch etc for this type of exception that will be translated as "the dates are incorrect"
                else
                {
                    List<HostingUnit> allUnits = GetListOfUnits();
                    foreach (HostingUnit unit in allUnits)
                        if (IsFitting(unit, request))
                            AddOrder(request, unit);
                }
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }
        }
        public void AddOrdersForNewUnit(HostingUnit unit)
        {
            try
            {
                List<GuestRequest> allRequests = FactorySingletonDal.GetInstance.GetAllGuestRequests();
                foreach (GuestRequest request in allRequests)
                    if (IsFitting(unit, request))
                        AddOrder(request, unit);
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }
        }
        public List<HostingUnit> CreateListOfFittingUnits(GuestRequest request, List<HostingUnit> myUnits)
        {
            try
            {
                if (request.ReleaseDate.Date <= request.EntryDate.Date)
                    throw new ArgumentException();// implement in UI catch etc for this type of exception that will be translated as "the dates are incorrect"
                else
                {
                    List<HostingUnit> units = new List<HostingUnit>();
                    foreach (HostingUnit unit in myUnits)
                        if (IsFitting(unit, request))
                            units.Add(unit);
                    return units;
                }
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }
        }
        public List<GuestRequest> CreateListOfFittingRequests(HostingUnit unit, List<GuestRequest> myRequests)
        {
            try
            {
               
                    List<GuestRequest> requests = new List<GuestRequest>();
                    foreach (GuestRequest request in myRequests)
                        if (IsFitting(unit, request))
                            requests.Add(request);
                    return requests;
                
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }
        }
        private bool IsFitting(HostingUnit unit, GuestRequest request)
        {
            if (unit.Jacuzz == true && request.Jacuzz == Options.NotIntresting)
                return false;
            if (unit.Jacuzz == false && request.Jacuzz == Options.Must)
                return false;
            if (unit.Pool == true && request.Pool == Options.NotIntresting)
                return false;
            if (unit.Pool == false && request.Pool == Options.Must)
                return false;
            if (request.Area != unit.Area)
                return false;
            if (unit.SubArea != request.SubArea)
                return false;
            return CheckDates(request, unit);
        }
        public void AddOrder(GuestRequest request, HostingUnit unit)
        {
            try
            {
                Order order = new Order();
                order.HostingUnitKey = unit.HostingUnitKey;
                order.GuestRequestKey = request.GuestRequestKey;
                order.OrderKey = ++Configuration.OrderKey;
                FactorySingletonDal.GetInstance.AddOrder(order);
            }
            catch (InvalidOperationException x)
            {
                throw x;
            }
        }
        public List<HostingUnit> AllAvailableOnDates(DateTime date, int num)
        {
            try
            {
                List<HostingUnit> unit = FactorySingletonDal.GetInstance.GetAllHostingUnits().Where(x =>
                FreeIn(x, date, num)).ToList();
                return null;
            }
            catch (InvalidOperationException v)
            {
                throw v;
            }
        }
        private bool FreeIn(HostingUnit unit, DateTime date, int num)
        {
            for (DateTime i = date; i < date.AddDays(num - 1); i = i.AddDays(1))
            {
                if (unit.Diary[i.Month, i.Day] == true)
                    return false;
            }
            return true;
        }
        public int NumDaysBetween(DateTime date1, DateTime date2 = default(DateTime))
        {
            if (date2 == default(DateTime))
                date2 = DateTime.Today;
            int counter = 0;
            for (DateTime i = date1; i < date2; i = i.AddDays(1))
                counter++;
            return counter;
        }
        public List<Order> NumDaysEqualBigger(int num)
        {
            try
            {
                List<Order> order = FactorySingletonDal.GetInstance.GetAllOrders().Where(x =>
               NumDaysBetween(x.OrderDate) >= num).ToList();
                return order;
            }
            catch (InvalidOperationException j)
            {
                throw j;
            }

        }
        public List<GuestRequest> GuestRequestIf(CheckGuestRequest check)// MAKE IT
        {
            try
            {
                List<GuestRequest> requests = FactorySingletonDal.GetInstance.GetAllGuestRequests().Where(x =>
               check(x)).ToList();
                return requests;
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }
        }
        public int NumOfOrders(GuestRequest request)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllOrders().Count(x =>
                x.GuestRequestKey == request.GuestRequestKey);
            }
            catch (InvalidOperationException j)
            {
                throw j;
            }
        }
        public int NumOfOrders(HostingUnit unit)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllOrders().Count(x =>
               x.HostingUnitKey == unit.HostingUnitKey);
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }
        }
        public IEnumerable<IGrouping<Areas, GuestRequest>> GetRequestsGroupingByAreas()
        {

            return FactorySingletonDal.GetInstance.GetAllGuestRequests().GroupBy(x =>
             x.Area);

        }
        public IEnumerable<IGrouping<int, GuestRequest>> GetRequestsGroupingByNumGuests()
        {

            return FactorySingletonDal.GetInstance.GetAllGuestRequests().GroupBy(x =>
             x.NumGuests);

        }
        public IEnumerable<IGrouping<int, HostingUnit>> GetUnitsGroupingByOwner()
        {

            return FactorySingletonDal.GetInstance.GetAllHostingUnits().GroupBy(x =>
             x.Owner.HostKey);

        }
        public IEnumerable<IGrouping<Areas, HostingUnit>> GetUnitssGroupingByAreas()
        {

            return FactorySingletonDal.GetInstance.GetAllHostingUnits().GroupBy(x =>
             x.Area);

        }
        public void UpdateHostingUnit(HostingUnit unit, string change)
        {
            try
            {
                List<HostingUnit> units = GetListOfUnits();
                HostingUnit currentUnit;
                foreach (HostingUnit thisunit in units)
                    if (unit.HostingUnitKey == thisunit.HostingUnitKey)
                        currentUnit = thisunit;
                if (change == "jacuzzi")
                    unit.Jacuzz = !(unit.Jacuzz);
                if (change == "pool")
                    unit.Pool = !(unit.Pool);
                FactorySingletonDal.GetInstance.UpdatingHostUnit(unit);
            }
            catch (InvalidOperationException n)
            {
                throw n;
            }
        }
        public void UpdateHostingUnit(HostingUnit unit)
        {
            try
            {
                FactorySingletonDal.GetInstance.UpdatingHostUnit(unit);
            }
            catch(Exception x)
            {
                throw x;
            }
        }
        public List<HostingUnit> AllUnitsOfOneHostFittingTorequest(int hostKey, GuestRequest request)
        {
            try
            {
                List<HostingUnit> units = FactorySingletonDal.GetInstance.GetAllHostingUnits().Where(x =>
                x.Owner.HostKey == hostKey && IsFitting(x, request)).ToList();
                return units;
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }

        }
        public int NumOfClosedOrders(HostingUnit unit)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllOrders().Count(x =>
               x.HostingUnitKey == unit.HostingUnitKey && x.Status == StatusO.ClosedByClientsResponse);
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }
        }
        public List<HostingUnit> OrderUnitsByPopularity()
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllHostingUnits().OrderByDescending(x => NumOfClosedOrders(x)).ToList();
            }
            catch (InvalidCastException x)
            {
                throw x;
            }
        }
        public HostingUnit THeMostPopularUnit()
        {
            try
            {
                return OrderUnitsByPopularity().First();
            }
            catch (InvalidCastException x)
            {
                throw x;
            }

        }
        public List<HostingUnit> AllUnitsOfOneHost(int hostKey)
        {
            try
            {
                List<HostingUnit> units = FactorySingletonDal.GetInstance.GetAllHostingUnits().Where(x => x.Owner.HostKey == hostKey).ToList();
                return units;
            }
            catch (InvalidCastException x)
            {
                throw x;
            }

        }
        public List<GuestRequest> AllrequestFittingToUnit(int key, HostingUnit unit)
        {
            try
            {
                List<GuestRequest> requests = FactorySingletonDal.GetInstance.GetAllGuestRequests().Where(x =>
                x.GuestRequestKey == key).ToList();
                return requests;
            }
            catch (InvalidOperationException h)
            {
                throw h;
            }

        }
        public int numOfUnits(Host host)
        {
            int num = FactorySingletonDal.GetInstance.GetAllHostingUnits().Count(x => x.Owner == host);
            return num;
        }
        public IEnumerable<IGrouping<int, Host>> GethostsGroupingByNumOfUnits()
        {

            return FactorySingletonDal.GetInstance.GetAllHosts().GroupBy(x =>
             numOfUnits(x));

        }
        /*public bool GuestIsExist(int ID)
        {
            return FactorySingletonDal.GetInstance.GetAllGuests().Exists(x => x.ID == ID);
        }*/
        public bool mailGuestIsExist(string mail)
        {
            return FactorySingletonDal.GetInstance.GetAllGuests().Exists(x => x.MailAddress == mail);
        }

        public bool AddHost(Host host)
        {
            try
            {
                FactorySingletonDal.GetInstance.AddHost(host);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
        public GuestRequest GetRequest(int id)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetGuestRequest(id);
            }
            catch(Exception)
            {
                return null;
            }
        }
        public Host GetHost(int key)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetHost(key);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Order GetOrder(int key)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetOrder(key);
            }
            catch (Exception)
            {
                return null;
            }

        }
        public List<Order> GetAllOrdersOfUnit(HostingUnit unit)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllOrders().Where(x =>
                x.HostingUnitKey == unit.HostingUnitKey).ToList();
            }
            catch(Exception)
            {
                return null;
            }
        }
        public List<Order> GetAllOrdersOfHost(int key)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetAllOrders().Where(x =>
                GetUnit(x.HostingUnitKey).Owner.HostKey ==key).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool DelUnit(HostingUnit unit)
        {
            if (GetAllOrdersOfUnit(unit).Exists(x => x.Status == StatusO.MailSent))
                return false;
            try
            {
                FactorySingletonDal.GetInstance.DelHostingUnit(unit);
                
            }
            catch (Exception )
            {
                return false;
            }
            
            return true;

        }
        public HostingUnit GetUnit(int key)
        {
            try
            {
                return FactorySingletonDal.GetInstance.GetHostingUnit(key);

            }
            catch (Exception)
            {
                return null;
            }

        }



    }
}


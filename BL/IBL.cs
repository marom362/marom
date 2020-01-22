using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public delegate bool CheckGuestRequest(GuestRequest request);
    public delegate bool CheckHostingUnit(HostingUnit request);
    public interface IBL
    {

        /// <summary>
        /// If the same Guest Request doesn't exist, sends it to DAL to add it
        /// </summary>
        /// <param name="request"></param>
        void AddRequest(GuestRequest request); //not checked
        /// <summary>
        /// checks whether the dates are available
        /// </summary>
        /// <param name="GuestRequest"></param>
        /// <param name="Order"></param>
        /// <returns>bool</returns>
        bool CheckDates(GuestRequest request, HostingUnit unit);
        void ChangeStatusOfRequest(int numOfRequest, StatusGR status);
        /// <summary>
        /// If this Hosting Unit doesn't exist yet, sends it to DAL to add it;
        /// </summary>
        /// <param name="unit"></param>
        void AddHostingUnit(HostingUnit unit);
        /// <summary>
        /// checks whether this Hosting Unit exists; if it exisxt, the function sends it to DAL for delition
        /// </summary>
        /// <param name="unit"></param>
        void RemoveHostingUnit(HostingUnit unit);
        void UpdateHostingUnit(HostingUnit unit);
        /// <summary>
        /// Updates the information about jacuzzi/ swimming pool
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="change"></param>
        void UpdateHostingUnit(HostingUnit unit, string change);
        /// <summary>
        /// If the order dates are correct, sends an order object to DAL
        /// </summary>
        /// <param name="order"></param>
        void AddOrder(GuestRequest request, HostingUnit unit);
        void ChangeStatusOfOrder(Order order, StatusO status);
        /// <summary>
        /// Returns a list of Units
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> GetListOfUnits();
        /// <summary>
        /// Returns a list of requests
        /// </summary>
        /// <returns></returns>
        List<GuestRequest> GetListOfRequests();
        /// <summary>
        /// returns list of orders
        /// </summary>
        /// <returns></returns>
        string SendingMail(Order order);
        int ClosingOrder(Order order, GuestRequest request, StatusO desiredStatus);
        List<Order> GetListOfOrders();
        /// <summary>
        /// returns list of bank branches
        /// </summary>
        /// <returns></returns>
        List<HostingUnit> AllAvailableOnDates(DateTime date, int num);
        List<HostingUnit> CreateListOfFittingUnits(GuestRequest request);
        int NumDaysBetween(DateTime date1, DateTime date2 = default(DateTime));
        List<Order> NumDaysEqualBigger(int num);
        List<GuestRequest> GuestRequestIf(CheckGuestRequest check);
        int NumOfOrders(GuestRequest request);
        int NumOfOrders(HostingUnit unit);
        IEnumerable<IGrouping<Areas, GuestRequest>> GetRequestsGroupingByAreas();
        IEnumerable<IGrouping<int, GuestRequest>> GetRequestsGroupingByNumGuests();
        IEnumerable<IGrouping<int, HostingUnit>> GetUnitsGroupingByOwner();
        IEnumerable<IGrouping<Areas, HostingUnit>> GetUnitssGroupingByAreas();
        List<Guest> GetListOfGuest();
        // List<Host> GetListOfHosts();
        List<BankBranch> getListOfBankBranches();
        //void AddGuest(Guest guest);
        List<HostingUnit> CreateListOfFittingUnits(GuestRequest request, List<HostingUnit> myUnits);
        //Guest GetGuest(int ID);
        int NumOfClosedOrders(HostingUnit unit);
        List<HostingUnit> OrderUnitsByPopularity();
        HostingUnit THeMostPopularUnit();
        List<HostingUnit> AllUnitsOfOneHost(int hostKey);
        List<HostingUnit> AllUnitsOfOneHostFittingTorequest(int hostKey, GuestRequest request);
        int numOfUnits(Host host);
        //bool GuestIsExist(int ID);
        bool mailGuestIsExist(string mail);
        bool AddHost(Host host);
        GuestRequest GetRequest(int id);
        Host GetHost(int key);
        Order GetOrder(int key);
        bool DelUnit(HostingUnit unit);
        List<Order> GetAllOrdersOfUnit(HostingUnit unit);
        List<Order> GetAllOrdersOfHost(int key);

        }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DAL
{
    public interface Idal
    {
        void AddGuestRequest(GuestRequest gr);
        void UpdatingGuestRequest(GuestRequest gr);
        GuestRequest GetGuestRequest(int key);
        List<GuestRequest> GetAllGuestRequests();
        void AddHostingUnit(HostingUnit hu);
        void DelHostingUnit(HostingUnit hu);
        void UpdatingHostUnit(HostingUnit hu);
        HostingUnit GetHostingUnit(int key);
        List<HostingUnit> GetAllHostingUnits();
        void AddOrder(Order order);
        void UpdatingOrder(Order order);
        Order GetOrder(int key);
        List<Order> GetAllOrders();
        List<BankBranch> GetAllBankBranches();
        List<Guest> GetAllGuests();
        //List<Host> GetAllHosts();
        void AddGuest(Guest guest);
        void AddHost(Host host);
        void DelHost(Host host);
        void UpdatingHost(Host host);
        Host GetHost(int key);
        List<Host> GetAllHosts();
    }
}

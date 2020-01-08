using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;
using System.Reflection;

namespace DAL
{
    public class Dal_imp : Idal
    {
        public void AddGuestRequest(GuestRequest gr)
        {
            var g = DataSource.GuestRequests;
            if (g == null)
            {
                DataSource.GuestRequests = new List<GuestRequest>();

            }
            else if (g.Exists(x => x.GuestRequestKey == gr.GuestRequestKey))
            {
                throw new InvalidOperationException("The request alredy exists");
            }
            gr.GuestRequestKey = ++Configuration.GuestRequestKey;
            gr.NumGuests = gr.Adults + gr.Children;
            DataSource.GuestRequests.Add(gr);
        }
       public void UpdatingGuestRequest(GuestRequest gr)
        {
            GuestRequest request = DataSource.GuestRequests.Where(req =>
             req.GuestRequestKey == gr.GuestRequestKey).SingleOrDefault();
            if (request == null)
            {
                throw new KeyNotFoundException("This hosting unit is not exist");
            }
            else
            {
                gr.NumGuests = gr.Adults + gr.Children;
                request = gr;
            }
        }
        public GuestRequest GetGuestRequest(int key)
        {
            GuestRequest request = DataSource.GuestRequests.Where(req => req.GuestRequestKey == key).SingleOrDefault();
            if (request == null)
                throw new InvalidOperationException("Doesn't exist");
            return Copy(request);
       
        }
        public void AddHostingUnit(HostingUnit hu)
        {
            var h = DataSource.HostingUnits;
            if (h.Exists(x => x.HostingUnitKey == hu.HostingUnitKey))
            {
                throw new InvalidOperationException("The unit already exists");
            }
            else
            {
                hu.HostingUnitKey = ++Configuration.HostingUnitKey;
                DataSource.HostingUnits.Add(hu);
            }
        }
        public void DelHostingUnit(HostingUnit hu)
        {
            List<HostingUnit> h = DataSource.HostingUnits;
            if (h.Exists(x => x.HostingUnitKey == hu.HostingUnitKey))
            {
                hu.HostingUnitKey = ++Configuration.HostingUnitKey;
                DataSource.HostingUnits.Remove(hu);
            }
            else
            {
                throw new InvalidOperationException("The unit is not exists");
            }
        }
        public void UpdatingHostUnit(HostingUnit hu)
        {
            HostingUnit unit = DataSource.HostingUnits.Where(x =>
            x.HostingUnitKey == hu.HostingUnitKey).SingleOrDefault();
            if (unit == null)
                throw new InvalidOperationException("This hosting unit is not exist");
            else
                unit = hu;
        }
        public HostingUnit GetHostingUnit(int key)
        {
            HostingUnit unit = DataSource.HostingUnits.Where(x =>
            x.HostingUnitKey == key).SingleOrDefault();
            if (unit == null)
                throw new InvalidOperationException("no hosting unit has found");
            else
                return unit;
        }
        public void /*Idal.*/AddOrder(Order order)
        {
            List<Order> or = DataSource.Orders;
            if (or.Exists(x => x.OrderKey == order.OrderKey))
                throw new InvalidOperationException("The order alredy exists");
            else
            {
                order.GuestRequestKey = ++Configuration.GuestRequestKey;
                DataSource.Orders.Add(order);
            }

        }
        void Idal.UpdatingOrder(Order order)
        {
            Order or = DataSource.Orders.Where(x =>
            x.OrderKey == order.OrderKey).SingleOrDefault();
            if (or == null)
            {
                throw new KeyNotFoundException("This order is not exist");
            }
            else
            {
                order = or;
            }

        }
        Order Idal.GetOrder(int key)
        {
            Order order = DataSource.Orders.Where(x =>
           x.OrderKey == key).SingleOrDefault();
            if (order == null)
                throw new InvalidOperationException("no order has found");
            else
                return order;
        }
        List<HostingUnit> Idal.GetAllHostingUnits()
        {
            HostingUnit[] units = new HostingUnit[DataSource.HostingUnits.Count];
            DataSource.HostingUnits.CopyTo(units);
            if (units == null)
                throw new InvalidOperationException("thier is no units");
            return units.ToList<HostingUnit>();
        }
        List<GuestRequest> Idal.GetAllGuestRequests()
        {
            List<GuestRequest> requests = 
            (from item in DS.DataSource.GuestRequests
             select Copy(item)
            ).ToList();
            //GuestRequest[] requests = new GuestRequest[DataSource.GuestRequests.Count];
            //DataSource.GuestRequests.CopyTo(requests);
            if (requests == null)
                throw new InvalidOperationException("thier is no clients");
            return requests.ToList<GuestRequest>();
        }
        List<Order> Idal.GetAllOrders()
        {
            Order[] orders = new Order[DataSource.Orders.Count];
            DataSource.Orders.CopyTo(orders);
            if (orders == null)
                throw new InvalidOperationException("thier is no orders");
            return orders.ToList<Order>();
        }
        List<BankBranch> Idal.GetAllBankBranches()
        {
            BankBranch[] branch = new BankBranch[DS.DataSource.BankBranchs.Count];
            DS.DataSource.BankBranchs.CopyTo(branch);
            return branch.ToList();
        }
        public ObjectType Copy<ObjectType>(ObjectType src)
        {
            ObjectType target = (ObjectType)Activator.CreateInstance(src.GetType());
            foreach (PropertyInfo property in src.GetType().GetProperties())
            {
                property.SetValue(target, property.GetValue(src));
            }
            return target;
        }
        //public List<Guest> GetAllGuests()
        //{
        //    using (Guest guest = new Guest()) ;
        //    {
        //        var query = from pro in db.Projects
        //                    select new ProjectInfo() { Name = pro.ProjectName, Id = pro.ProjectId };

        //        return query.ToList();
        //    }
        //}
        public List<Guest> GetAllGuests()
        {
          if (DataSource.Guests == null)
                throw new InvalidOperationException("thier is no orders");
            List<Guest> guests =
                (from item in DS.DataSource.Guests
           select Copy(item)
          ).ToList();

            //Guest[] guests = new Guest[DataSource.Guests.Count];
            //DataSource.Guests.CopyTo(guests);
            //if (guests == null)
            //    throw new InvalidOperationException("thier is no orders");
            return guests;
        }
        //public List<Host> GetAllHosts()
        //{
        //    Host[] hosts = new Host[DataSource.Host.Count];
        //    DataSource.Guests.CopyTo(guests);
        //    if (guests == null)
        //        throw new InvalidOperationException("thier is no orders");
        //    return guests.ToList<Guest>();
        //}
        public void AddGuest(Guest guest)
        {
            List<Guest> g = DataSource.Guests;
            if (g == null)
            {
                //DataSource.Guests = new List<Guest>();
                DataSource.Guests.Add(guest);
                return;
            }
            if (g.Exists(x => x.ID == guest.ID))
                throw new InvalidOperationException("The order alredy exists");
            else
            {
                
                DataSource.Guests.Add(guest);
            }

        }
    }
}

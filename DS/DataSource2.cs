using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public static class DataSource2
    {
        public static List<HostingUnit> HostingUnits = new List<HostingUnit>()
        {
            new HostingUnit()
            {
                HostingUnitKey= 200000003,
                Owner= new Host(){HostKey=1, FamilyName="Meir" },
                HostingUnitName= "מול הים",
                Pool= true,
                ChildrenAtraction=false,
                Jacuzz= true,
                Area= Areas.South,
                Type= Types.Hotel,
                numOfMaxGuests=10
            },

            new HostingUnit()
            {
                HostingUnitKey= 200000004,
                Owner= new Host(){HostKey=1, FamilyName="Meir" },
                HostingUnitName= "בקתה בעמק",
                Pool= true,
                ChildrenAtraction= false,
                Jacuzz= true,
                Area= Areas.Center,
                Type= Types.Zimmer,
                numOfMaxGuests=5
            },

           new HostingUnit()
            {
                HostingUnitKey= 200000005,
                Owner= new Host(){HostKey=1, FamilyName="Meir" },
                HostingUnitName= "מול החוף",
                Pool= false,
                ChildrenAtraction= true,
                Jacuzz=  true,
                Area= Areas.North,
                Type= Types.Hotel,
                numOfMaxGuests=15
            },

            new HostingUnit()
            {
                HostingUnitKey= 200000006,
                Owner= new Host(){HostKey=3, FamilyName="Levi" },
                HostingUnitName= "מול החוף",
                Pool= false,
                ChildrenAtraction= true,
                Jacuzz=  true,
                Area= Areas.North,
                Type= Types.Hotel,
                numOfMaxGuests=9
            },

            new HostingUnit()
            {
                HostingUnitKey= 200000007,
                Owner= new Host(){HostKey=3, FamilyName="Levi" },
                HostingUnitName= "מול החוף",
                Pool= false,
                ChildrenAtraction= true,
                Jacuzz=  true,
                Area= Areas.North,
                Type= Types.Hotel,
                numOfMaxGuests=3
            }
        };
        public static List<GuestRequest> GuestRequests;
        public static List<Order> Orders = new List<Order>()
        {
            new Order()
            {
                HostingUnitKey=200000002,
                GuestRequestKey=300000002,
                OrderKey=10000001,
                CreateDate=new DateTime(2020,3,20),
                OrderDate=new DateTime(2020,3,28),
            },

            new Order()
            {
                HostingUnitKey=200000003,
                GuestRequestKey=300000003,
                OrderKey=10000002,
                CreateDate=new DateTime(2020,4,29),
                OrderDate=new DateTime(2020,5,1),
            },

            new Order()
            {
                HostingUnitKey=200000003,
                GuestRequestKey=300000004,
                OrderKey=10000003,
                CreateDate=new DateTime(2020,7,5),
                OrderDate=new DateTime(2020,7,15),
            }
        };
        //public static List<BankBranch> BankBranchs = new List<BankBranch>()
        //{
        //    new BankBranch()
        //    {
        //        BankNumber=122,
        //        BankName ="הפועלים",
        //        BranchNumber = 35,
        //        BranchAddress = "הרצל 7",
        //        BranchCity = "קרית אונו",

        //    },

        //    new BankBranch()
        //    {
        //         BankNumber=456,
        //        BankName ="לאומי",
        //        BranchNumber = 76,
        //        BranchAddress = "כנפי נשרים 10",
        //        BranchCity = "ירושלים",

        //    },

        //    new BankBranch()
        //    {
        //        BankNumber=390,
        //        BankName ="דיסקונט",
        //        BranchNumber = 24,
        //        BranchAddress = "יגאל אלון 5",
        //        BranchCity = "פתח תקווה",

        //    },

        //    new BankBranch()
        //    {
        //        BankNumber=650,
        //        BankName ="איגוד",
        //        BranchNumber = 59,
        //        BranchAddress = "ביאליק 20",
        //        BranchCity = "תל אביב",

        //    },

        //    new BankBranch()
        //    {
        //        BankNumber=720,
        //        BankName ="הבינלאומי",
        //        BranchNumber = 22,
        //        BranchAddress = "בן גוריון 18",
        //        BranchCity = "הרצליה",

        //    }
        //};
    }
}

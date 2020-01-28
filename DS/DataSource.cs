using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public static class DataSource
    {
        public static List<HostingUnit> HostingUnits = new List<HostingUnit>()
        {
            new HostingUnit()
            {
                HostingUnitKey= 200000003,
                Owner= new Host(){HostKey=1, FamilyName="Meir" },
                HostingUnitName= "מלון הדר",
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
                HostingUnitName= "בקתת רימונים",
                Pool= true,
                ChildrenAtraction= false,
                Jacuzz= true,
                Area= Areas.Center,
                Type= Types.Zimmer,
                numOfMaxGuests=9
                
            },

           new HostingUnit()
            {
                HostingUnitKey= 200000005,
                Owner= new Host(){HostKey=1, FamilyName="Meir" },
                HostingUnitName= "חופשה בטבע",
                Pool= false,
                ChildrenAtraction= true,
                Jacuzz=  true,
                Area= Areas.North,
                Type= Types.Camping,
                numOfMaxGuests=15
            },

            new HostingUnit()
            {
                HostingUnitKey= 200000006,
                Owner= new Host(){HostKey=3, FamilyName="Levi" },
                HostingUnitName= "מלון הוד",
                Pool= true,
                ChildrenAtraction= true,
                Jacuzz=  true,
                Garden=false,
                Area= Areas.Center,
                Type= Types.Hotel,
                numOfMaxGuests=3
            },

            new HostingUnit()
            {
                HostingUnitKey= 200000007,
                Owner= new Host(){HostKey=3, FamilyName="Levi" },
                HostingUnitName= "מלון פאר ",
                Pool= false,
                ChildrenAtraction= true,
                Jacuzz=  true,
                Area= Areas.North,
                Type= Types.Hotel,
                numOfMaxGuests=3
            }
        };
    

    public static List<Guest> Guests = new List<Guest>();
        public static List<Host> Hosts = new List<Host>()
        {
            new Host()
            {
                HostKey=1,
                FamilyName ="Meir",
                PrivateName="Moria",
                FhoneNumber="0527264891",
                MailAddress="Moria@gmail.com",
                bankBranch=new BankBranch(){BankNumber=122,BankName ="הפועלים",BranchNumber = 35,
                BranchAddress = "הרצל 7",BranchCity = "קרית אונו"},
                BankAccountNumber=3152667,
                CollectionClearance=false,
                password="11111"
            },

            new Host()
            {
                HostKey=3,
                FamilyName ="Levi",
                PrivateName="Mor",
                FhoneNumber="0527264891",
                MailAddress="Mor@gmail.com",
                bankBranch=new BankBranch(){BankNumber=122,BankName ="הפועלים",BranchNumber = 35,
                BranchAddress = "הרצל 7",BranchCity = "קרית אונו"},
                BankAccountNumber=3152667,
                CollectionClearance=false,
                password="33333"


            }

        };
        public static List<GuestRequest> GuestRequests = new List<GuestRequest>()
        {
            new GuestRequest()
            {
               GuestRequestKey=100000002,
               guest=new Guest(){PrivateName= "Shalom",
               FamilyName= "Tal",
               MailAddress="shalomT@gmail.com",
               BankAccountNumber=2245,
               passward="shalom"
               },
               Status= StatusGR.ClosedThroughSite,
               RegistrationDate=new DateTime(2020, 1,1),
               EntryDate=new DateTime(2020,5,4),
               ReleaseDate=new DateTime(2020,5,8),
               Area= Areas.Center,
              /* subArea= Enums.SubArea.TelAviv*/
               Type= Types.Zimmer,
               Adults=2,
               Children=6,
               Pool= Options.Possible,
               Garden= Options.Possible,
               ChildrensAttractions= Options.NotIntresting,
               Jacuzz= Options.Must,
               NumGuests=8
            },

            new GuestRequest()
            {
               GuestRequestKey=100000003,
               guest=new Guest(){PrivateName= "Shachar",
               FamilyName= "Biton",
               MailAddress="shacharB@gmail.com",
               BankAccountNumber=12366,
               passward="shachar"},
               Status= StatusGR.Open,
               RegistrationDate=new DateTime(2019,12,1),
               EntryDate=new DateTime(2020,2,1),
               ReleaseDate=new DateTime(2020,2,7),
               Area= Areas.All,
              /* subArea= Enums.SubArea.TelAviv*/
               Type= Types.Hotel,
               Adults=2,
               Children=0,
               Pool= Options.Possible,
               Garden= Options.Possible,
               ChildrensAttractions= Options.Possible,
               Jacuzz= Options.Must,
               NumGuests=2
            },

            new GuestRequest()
            {
               GuestRequestKey=100000004,
               guest=new Guest(){PrivateName= "Moria",
               FamilyName= "Ariel",
               MailAddress="maromsulami@gmail.com",
               BankAccountNumber =664435,
               passward="moriaA"},
               Status= StatusGR.Open,
               RegistrationDate=new DateTime(2020,1,20),
               EntryDate=new DateTime(2020,1,20),
               ReleaseDate=new DateTime(2020,5,4),
               Area= Areas.All,
              /* subArea= Enums.SubArea.TelAviv*/
               Type= Types.Camping,
               Adults=8,
               Children=0,
               Pool= Options.Possible,
               Garden= Options.Possible,
               ChildrensAttractions= Options.Must,
               Jacuzz= Options.Possible,
               NumGuests=8
            }
        };



        public static List<Order> Orders = new List<Order>()
        {
            new Order()
            {
                HostingUnitKey=200000004,
                GuestRequestKey=100000002,
                OrderKey=10000001,
                Status=StatusO.ClosedByClientsResponse,
                CreateDate=new DateTime(2020,1,1),
                OrderDate=new DateTime(2020,1,22),
            },
            new Order()
            {
                HostingUnitKey=200000006,
                GuestRequestKey=100000003,
                OrderKey=10000002,
                Status=StatusO.MailSent,
                CreateDate=new DateTime(2019,8,6),
                OrderDate=new DateTime(2020,1,22),
            },

            new Order()
            {
                HostingUnitKey=200000007,
                GuestRequestKey=100000003,
                OrderKey=10000003,
                Status=StatusO.NotDealed,
                OrderDate=new DateTime(2020,5,1),
            },

            new Order()
            {
                HostingUnitKey=200000003,
                GuestRequestKey=100000003,
                OrderKey=10000004,
                Status=StatusO.MailSent,
                CreateDate=new DateTime(2020,7,5),
                OrderDate=new DateTime(2020,7,15),
            },
            new Order()
            {
                HostingUnitKey=200000005,
                GuestRequestKey=100000004,
                OrderKey=10000005,
                Status=StatusO.MailSent,
                CreateDate=new DateTime(2020,7,5),
                OrderDate=new DateTime(2020,7,15),
            }
        };

        public static List<BankBranch> BankBranchs = new List<BankBranch>()
        {
            new BankBranch()
            {
                BankNumber=122,
                BankName ="הפועלים",
                BranchNumber = 35,
                BranchAddress = "הרצל 7",
                BranchCity = "קרית אונו",

            },

            new BankBranch()
            {
                 BankNumber=456,
                BankName ="לאומי",
                BranchNumber = 76,
                BranchAddress = "כנפי נשרים 10",
                BranchCity = "ירושלים",

            },

            new BankBranch()
            {
                BankNumber=390,
                BankName ="דיסקונט",
                BranchNumber = 24,
                BranchAddress = "יגאל אלון 5",
                BranchCity = "פתח תקווה",

            },

            new BankBranch()
            {
                BankNumber=650,
                BankName ="איגוד",
                BranchNumber = 59,
                BranchAddress = "ביאליק 20",
                BranchCity = "תל אביב",

            },

            new BankBranch()
            {
                BankNumber=720,
                BankName ="הבינלאומי",
                BranchNumber = 22,
                BranchAddress = "בן גוריון 18",
                BranchCity = "הרצליה",

            }
        };
        
    }
}




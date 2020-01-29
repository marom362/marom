using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest
    {
        public int GuestRequestKey { get; set; }
        /*public string password { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }*/
        public Guest guest { get; set; }
        public StatusGR Status { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public Areas Area { get; set; }
        public string SubArea { get; set; }
        public Types Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public Options Pool { get; set; }
        public Options Jacuzz { get; set; }
        public Options Garden { get; set; }
        public Options ChildrensAttractions { get; set; }
        public int NumGuests { get; set;}
        public override string ToString()
        {
            //string s;
                /*$@"status : {Status} 
                GuestRequestKey : {GuestRequestKey}
                MailAddress: {MailAddress}
                Status:*/
            string information;
            //information = " GuestRequestKey: " + GuestRequestKey.ToString() + ','+guest.ToString();
            // information += " PrivateName: " + PrivateName + ','+" FamilyName: " + FamilyName + ',';
            // information += " MailAddress: " + MailAddress + '\n';
            information = "";
            switch ((int)Status)
            {
                case 0:
                   information += " status: open,";
                    break;
                case 1:
                    information += " status: a deal was closed through the site,";
                    break;
                case 2:
                    information += " status: closed because it expired,";
                    break;
            }
            information += " Registration Date: " + RegistrationDate.Day.ToString() +'.'+ RegistrationDate.Month.ToString() +'.'+ RegistrationDate.Year.ToString()+',';
            information += " Entry Date: "+ EntryDate.Day.ToString() + '.'+ EntryDate.Month.ToString() + ','+ EntryDate.Year.ToString() + ',';
            information += " release date: " + ReleaseDate.Day.ToString() + ',' + ReleaseDate.Month.ToString() + ',' + ReleaseDate.Year.ToString() + '\n';
            information += " Area: " + Area.ToString()+','+ " SubArea: " + SubArea + ',';
            information += " Type: " + Type.ToString() + ',';
            information += " Adults: " + Adults.ToString() + ','+" Children:" + Children.ToString() + ',';
            information += " pool:" + Pool.ToString() + ',' + " Jacuzz:" + Jacuzz.ToString() + ',' + " Garden:" + Garden.ToString() + ',' + " ChildrensAttractions:" + ChildrensAttractions.ToString() + ',';
            return information;
        }
    }
}

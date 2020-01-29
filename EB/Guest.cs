using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Guest
    {
        public string passward { get; set; }
        //public int ID { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string MailAddress { get; set; }
        public string PhoneNumber { get; set; }
        //public BankBranch BankBranchDetails { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }
        public override string ToString()
        {
            string information;
            //information = " ID: " + ID.ToString() + ',';
            information = " PrivateName: " + PrivateName + ',' + " FamilyName: " + FamilyName + ',';
            information += " MailAddress: " + MailAddress + '\n';
            information += " PhoneNumber: " + PhoneNumber + '\n';
            //information += "BankBranchDetails:" + BankBranchDetails.ToString();
            information += "BankAccountNumber:" + BankAccountNumber + '\n';
            //information += "CollectionClearance: " + CollectionClearance + '\n';
            return information;
        }
    }
}


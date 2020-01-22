using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public int HostKey { get; set; }
        public string PrivateName { get; set; }
        public string FamilyName { get; set; }
        public string FhoneNumber { get; set; }
        public string MailAddress { get; set; }
        public BankBranch bankBranch { get; set; }
        public int BankAccountNumber { get; set; }
        public bool CollectionClearance { get; set; }
        public string password { get; set; }
        public override string ToString()
        {
            string s;
            s = "Host Key:" + HostKey.ToString();
            s += " Private Name: " + PrivateName + "Family Name: " + FamilyName;
            //s += " Fhone Number: " + FhoneNumber.ToString() + " MailAddress: " + MailAddress; 
            //s+= " bank Branch: "+bankBranch.ToString()+ " BankAccountNumber: "+BankAccountNumber.ToString()+ " CollectionClearance: "+CollectionClearance.ToString()+ '\n';
            return s;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch
    {
        public int BankNumber { get; set; }
        public string BankName { get; set; }
        public int BranchNumber { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCity { get; set; }
        public override string ToString()
        {
            string s;
            s = "bank number: " + BankNumber.ToString() + ", bank name: " + BankName + ", branch number: " + BranchNumber.ToString()
                + ",branch address: " + BranchAddress + ", branch city: " + BranchCity;
            return s;
        }
    }
}

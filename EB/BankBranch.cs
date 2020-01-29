using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch
    {
        public int Bank_Code { get; set; }
        public string Bank_Name { get; set; }
        public int Branch_Code { get; set; }
        public string Branch_Address { get; set; }
        public string City { get; set; }
        public override string ToString()
        {
            string s;
            s = ", bank name: " + Bank_Name + ", bank number: " + Bank_Code.ToString() + ", branch number: " + Branch_Code.ToString()
                + ", branch city: " + City + ",branch address: " + Branch_Address ;
            return s;
        }
    }
}

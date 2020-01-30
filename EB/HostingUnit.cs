using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit
    {
        public int HostingUnitKey { get; set; }
        public Areas Area { get; set; }
        public bool Pool { get; set; }
        public bool Jacuzz { get; set; }
        public bool Garden { get; set; }
        public bool ChildrenAtraction { get; set; }
        public string HostingUnitName { get; set; }
        public string SubArea { get; set; }
        public Types Type { get; set; }
        public int numOfMaxGuests { get; set; }
        public Host Owner { get; set; }
        [XmlIgnore]
        public bool[,] Diary = new bool[12, 31];
        // instead of DiaryDto'
        [XmlArray("Diary")]
        public bool[] DiaryDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(31); }
        }
        public override string ToString()
        {
            string s = "HostingUnitKey: " + HostingUnitKey.ToString() + '\n' + " Owner: " + Owner.ToString() + '\n' + " HostingUnitName: " + HostingUnitName + "HostingUnitType" + Type + "\nArea: " + "SubArea: " + SubArea + Area.ToString() + "\nJacuzzi: " + Jacuzz + "\nPool: " + Pool + '\n' + "\nGarden: " + Garden + "\nChildrenAtraction: " + ChildrenAtraction + '\n';
            return s;
        }
    }
}

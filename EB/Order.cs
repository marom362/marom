using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        public int HostingUnitKey { get; set; }
        public int GuestRequestKey { get; set; }
        public int OrderKey { get; }
        public StatusO Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime OrderDate { get; set; }
        public override string ToString()
        {
            string text = "Hosting Unit Key: " + HostingUnitKey.ToString() + "Guest Request Key: " + GuestRequestKey.ToString() + "\n Order Key: " + OrderKey.ToString() + "\n The request was created on " + CreateDate.ToString() + "\nStatus: ";
            switch ((int)Status)
            {
                case 0:
                    text += " Not answered";
                    break;
                case 1:
                    text += " The mail was sent";
                    break;
                case 2:
                    text += " Closed because the client was not interested";
                    break;
                case 3:
                    text += " Closed by answer to the client";
                    break;
            }
            if ((int)Status > 0)
                text += "\nOrderDate: " + OrderDate.ToString();
            return text;
        }
        /*public Order(GuestRequest gr, HostingUnit hs)
        {
            HostingUnitKey = hs.HostingUnitKey;
            GuestRequestKey = gr.GuestRequestKey;
            OrderKey = orderKey++;
            Status = 0;// is it correct??????
            CreateDate = DateTime.Today;
            //Nullable<DateTime> dt = null;
            //dt = new DateTime();
            OrderDate = new DateTime(01 / 01 / 1960);//default date; better to use nullable
        }*/
    }
}

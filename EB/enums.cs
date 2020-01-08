using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum Areas
    {
        All,
        North,
        South,
        Center,
        Jerusalem
    }
    public enum Options
    {
        Must,
        Possible,
        NotIntresting

    }
    public enum StatusGR
    {
        Open,
        ClosedThroughSite,
        ClosedBecauseExpired



    }
    public enum StatusO
    {
        NotDealed,
        MailSent,
        ClosedBecauseofClient,
        ClosedByClientsResponse
    }
    public enum Types
    {
        Zimmer,
        Hotel,
        Camping

    }
}

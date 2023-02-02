using GiftManagement.DAL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GiftManagement.DAL.Repositories
{
    public class GiftRequestorsRepository : IGiftRequestorsRepository
    {
        public IList<Requestor> Requestors { get; set; } = new List<Requestor>();
        public IDictionary<string, float> Catalog { get; set; } = new Dictionary<string, float>()
        {
            { "PSP", 50},
            { "Rocket", 45},
            { "RC Car", 25 },
            { "Lego", 15 },
            { "Barbie", 10 },
            { "Cryon’s", 10 },
            { "Candies", 5 },
            { "Mittens", 3 },
        };

    }
}

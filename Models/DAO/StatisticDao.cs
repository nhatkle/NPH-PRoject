using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class StatisticDao
    {
        NPHDbContext db = null;
        public StatisticDao()
        {
            db = new NPHDbContext();
        }
        public IEnumerable<GetRevenueStatistic_Result> GetRevenueStatistic(string fromDate,string toDate)
        {
            return db.GetRevenueStatistic(fromDate, toDate);
        }
    }
}

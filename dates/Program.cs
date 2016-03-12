using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeRange
{
    class Program
    {
        class Order
        {
            public DateTime Date { get; set;}
            public double Amount { get; set; }
        }

        static void Main(string[] args)
        {
            var begday = new DateTime(2016, 1, 1);
            var endday = new DateTime(2016, 1, 20);

            var daysWithoutSundays =
                Enumerable.Range(0, (endday - begday).Days)
                          .Select(i => begday.AddDays(i))
                          .Where(d => d.DayOfWeek != DayOfWeek.Sunday)
                          .ToList();

            var rand = new Random();
            var orders = Enumerable.Range(0, daysWithoutSundays.Count / 2).Select(i => new Order()
            {
                //Date = daysWithoutSundays[i * 2], // rand.Next(daysWithoutSundays.Count)],
                //Amount = i + 1 //rand.NextDouble() * 10
                Date = daysWithoutSundays[rand.Next(daysWithoutSundays.Count)],
                Amount = rand.NextDouble() * 10
            });

            var ordersByDate = orders.GroupBy(o => o.Date)
                  .Select(g => new Order()
                  {
                      Date = g.Key,
                      Amount = g.Sum(o => o.Amount)
                  })
                  .ToDictionary(o => o.Date);

            if (ordersByDate.Last().Value.Date != daysWithoutSundays.Last())
            {
                ordersByDate[daysWithoutSundays.Last()] = new Order()
                {
                    Date = daysWithoutSundays.Last()
                };
            }

            var daySums = new List<double>();
            var daysPassed = 0;
            foreach (var date in daysWithoutSundays)
            {
                daysPassed++;
                Order order;
                if (ordersByDate.TryGetValue(date, out order)) 
                {
                    var amountPerDay = order.Amount / daysPassed;
                    daySums.AddRange(Enumerable.Range(0, daysPassed).Select(_ => amountPerDay));
                    daysPassed = 0;
                }
            }


        }

    }
}

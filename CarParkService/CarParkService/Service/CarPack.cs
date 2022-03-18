using CarParkService.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarPark.Service
{
    public class CarPack
    {
        private readonly IConfiguration config;
        private readonly MainDb applicationdb;

        public CarPack(IConfiguration _config, MainDb _applicationdb)
        {
            config = _config;
            applicationdb = _applicationdb;
        }

        private List<decimal> GetRules()
        {
            try
            {
                List<decimal> PackingRules = applicationdb.PackingRules.Select(r => r.Amount).ToList();

                return PackingRules;
            }
            catch(Exception)
            {
                return null;
            }
            
        }

        private string GetDuration(string E, string L)
        {
            try
            {
                DateTime Entry = DateTime.Parse(E);
                DateTime Exit = DateTime.Parse(L);
                TimeSpan diff = Exit - Entry;

                return diff.ToString(); //hh:mm:ss
            }
            catch(Exception)
            {
                return null;
            }
        }

        public async Task<decimal> GetAmountToPay(string E, string L)
        {
            try
            {
                // Initialization
                int hr, min;
                string duration = GetDuration(E, L);
                (hr, min) = (int.Parse(duration.Split(':')[0]), int.Parse(duration.Split(':')[1]));
                decimal totalAmount = 0;

                List<decimal> rules = GetRules();
                int entrancefee = rules != null ? (int)rules[0] : 2;
                int firsthour = rules != null ? (int)rules[1] : 3;
                int otherhours = rules != null ? (int)rules[2] : 4;

                // Algorithm
                if (hr <= 1)
                {
                    totalAmount = entrancefee + firsthour;
                }
                else
                {
                    totalAmount = entrancefee + firsthour + ((hr - 1) * otherhours);
                    if (min > 0)
                    {
                        totalAmount += otherhours;
                    }
                }
                

                // Create Ticket
                PackingTicket ticket = new PackingTicket()
                {
                    AmountToPay = totalAmount,
                    Entrytime = E,
                    Exittime = L,
                    Date = DateTime.Now,
                    HoursSpent = hr,
                    Name = "Test Ticket"
                };

                // Save to table
                await applicationdb.PackingTickets.AddAsync(ticket);
                await applicationdb.SaveChangesAsync();            

                return totalAmount;
            }
            catch(Exception)
            {
                return -1;
            }
        }

        public List<PackingTicket> GetAlltickets()
        {
            return applicationdb.PackingTickets.ToList();
        }
    }
}

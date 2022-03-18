using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarParkService.Model
{
    public class PackingTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int ID { get; set; }

        public string Name { get; set; }

        public string Entrytime { get; set; }

        public string Exittime { get; set; }

        public int HoursSpent { get; set; }

        public decimal AmountToPay { get; set; }

        public DateTime Date { get; set; }
    }
}

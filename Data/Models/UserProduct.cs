using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E5MakersMarkt.Data.Models
{
    internal class UserProduct
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime Datum { get; set; }
        public float Price { get; set; }
    }
}

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

        public User User { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime Datum { get; set; }
        public float Price { get; set; }

        public Boolean Reported { get; set; }
    }
}

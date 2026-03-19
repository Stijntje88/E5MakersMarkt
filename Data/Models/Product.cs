using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E5MakersMarkt.Data.Models
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Img { get; set; }
        public string Material { get; set; }
        public string ProductionTime { get; set; }

    }
}

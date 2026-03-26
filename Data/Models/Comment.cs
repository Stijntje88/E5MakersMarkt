using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E5MakersMarkt.Data.Models
{
    internal class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public string CommentTitle { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string Description { get; set; }
        private int _rating;

        public int Rating
        {
            get => _rating;
            set => _rating = Math.Clamp(value, 1, 5);
        }
    }
}

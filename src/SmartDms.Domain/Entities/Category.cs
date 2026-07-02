using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuJuBis.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string CategoryCode { get; set; } = string.Empty;

        public string CategoryName { get; set; } = string.Empty;

        public string? KhmerCategoryName { get; set; }

        public bool Active { get; set; }

        public string? Remark { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

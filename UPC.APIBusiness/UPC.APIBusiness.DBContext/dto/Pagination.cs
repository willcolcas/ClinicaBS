using System.Collections.Generic;

namespace DBContext
{
    public class Pagination<T>
    {
        public List<T> content { get; set; }
        public int currentPage { get; set; }
        public int totalPages { get; set; }
    }
}



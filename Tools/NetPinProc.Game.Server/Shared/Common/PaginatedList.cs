namespace NetPinProc.Game.Manager.Shared.Common
{
    public class PaginatedList<T>
    {
        /// <summary>page index</summary>
        public int Index { get; set; }

        /// <summary>total pages in search</summary>
        public int TotalPages { get; set; }

        /// <summary>total results in search</summary>
        public int Results { get; set; }

        public List<T>? Items { get; set; }

        public bool HasPrevious => Index > 1;

        public bool HasNext => Index < TotalPages;
    
        public static PaginatedList<T> Create(
            IQueryable<T> source,
            int pageIndex,
            int pageSize) 
        {
            var count = source.Count();
            int totalPages = GetTotalPages(pageSize, count);

            var s = source
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();

            return new PaginatedList<T>(s, count, pageIndex, pageSize);
        }

        public PaginatedList(
            List<T> items,
            int count,
            int index,
            int pageSize)
        {
            Results = count;      
            Index = index;            
            Items = new List<T>();
            Items.AddRange(items);

            TotalPages = GetTotalPages(pageSize, Results);
        }

        private static int GetTotalPages(int pageSize, int count) => 
            (int)Math.Ceiling((double)count / pageSize);
    }
}

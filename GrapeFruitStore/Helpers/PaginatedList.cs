using System;
using System.Collections.Generic;
using System.Linq;

namespace GrapeFruitStore.Helpers
{

    /*
     * IQueryable<T> is a very powerful feature that enables a variety of interesting deferred
     * execution scenarios (like paging and composition based queries).  As with all powerful
     * features, you want to be careful with how you use it and make sure it is not abused.
     * 
     * It is important to recognize that returning an IQueryable<T> result from your repository
     * enables calling code to append on chained operator methods to it, and so participate in
     * the ultimate query execution. If you do not want to provide calling code this ability,
     * then you should return back IList<T>, List<T> or IEnumerable<T> results - which contain
     * the results of a query that has already executed.
     * 
     * For pagination scenarios this would require you to push the actual data pagination logic
     * into the repository method being called.  In this scenario we might update our
     * FindUpcomingDinners() finder method to have a signature that either returned a
     * PaginatedList: 
     * 
     * PaginatedList< Dinner> FindUpcomingDinners(int pageIndex, int pageSize) { }
     * 
     * Or return back an IList<Dinner>, and use a “totalCount” out param to return the total
     * count of Dinners:
     * 
     * IList<Dinner> FindUpcomingDinners(int pageIndex, int pageSize, out int totalCount) { }
     *
     */

    public class PaginatedList<T> : List<T> {

        public int PageIndex  { get; private set; }
        public int PageSize   { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(IQueryable<T> source, int pageIndex, int pageSize) {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = source.Count();
            TotalPages = (int) Math.Ceiling(TotalCount / (double)PageSize);

            this.AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
        }

        public bool HasPreviousPage {
            get {
                return (PageIndex > 0);
            }
        }

        public bool HasNextPage {
            get {
                return (PageIndex+1 < TotalPages);
            }
        }
    }
}

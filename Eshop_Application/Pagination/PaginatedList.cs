using Eshop_Domain.Entities.ProductEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Pagination
{
    public class PaginatedList<TClass> where TClass : class
    {
        // private set => to not edit the result 
        private PaginatedList(List<TClass> results, int limit, int offset, int totalCount) // to not make an instance of PaginatedList
        {
            this.Results = results;
            this.Limit = limit;
            this.Offset = offset;
            this.TotalCount = totalCount;
        }

        public int Limit { get; private set; } // How many elements is going to show

        public int Offset { get; private set; } // Where to start

        public int TotalCount { get; private set; }

        public bool HasNextPage => Limit * Offset < TotalCount;

        public bool HasPreviousPage => Limit > 1;

        public List<TClass> Results { get; private set; }

        public static async Task<PaginatedList<TClass>> Create(IQueryable<TClass> query, int limit, int offset) //Need to use this method to create an instance
        {
            int totalCount = await query.CountAsync(); // return how many elements the db has
            List<TClass> items = await query.Skip((offset - 1) * limit).Take(limit).ToListAsync();
            return new(items,limit,offset,totalCount);
        }

    }
}

using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDBDriver =  MongoDB.Driver;
using MongoDB.Driver.Linq;

// using MongoDBLinq = MongoDB.Driver.Linq;

namespace MBS_QUERY.Contract.Abstractions.Shared
{
    public class PagedResult<T>
    {
        public const int UpperPageSize = 100;
        public const int DefaultPageSize = 10;
        public const int DefaultPageIndex = 1;
    
        [JsonConstructor]
        public PagedResult(List<T> items, int pageIndex, int pageSize, int totalCount)
        { 
            Items = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
        
        public List<T> Items { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public bool HasNextPage => PageIndex * PageSize < TotalCount;
        public bool HasPreviousPage => PageIndex > 1;
        
        public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> query, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex <= 0 ? DefaultPageIndex : pageIndex;
            pageSize = pageSize <= 0
                ? DefaultPageSize
                : pageSize > UpperPageSize
                    ? UpperPageSize : pageSize;

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new (items, pageIndex, pageSize, totalCount);
        }
        
        public static async Task<PagedResult<T>> CreateAsyncMongoLinq(IMongoQueryable<T> query, int pageIndex, int pageSize)
        {
            pageIndex = pageIndex <= 0 ? DefaultPageIndex : pageIndex;
            pageSize = pageSize <= 0
                ? DefaultPageSize
                : pageSize > UpperPageSize
                    ? UpperPageSize : pageSize;

            var totalCount = await query.CountAsync();

            // Convert the IMongoQueryable to an IAsyncCursor and retrieve the results
            var cursor = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToCursorAsync();

            // Use cursor to asynchronously retrieve the list of items
            var items = await cursor.ToListAsync();

            return new PagedResult<T>(items, pageIndex, pageSize, totalCount);
        }

        public static PagedResult<T> Create(List<T> items, int pageIndex, int pageSize, int totalCount)
            => new (items, pageIndex, pageSize, totalCount);
    }
}
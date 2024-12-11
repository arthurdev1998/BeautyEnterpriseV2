using Microsoft.EntityFrameworkCore;

namespace BuildingBlocks.Core.Data;
public class PagedResults<T> where T : class, IPagedResult<T>
{
    public IEnumerable<T>? Items { get; set; }
    public int TotalResults { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; private set; }
    public bool HasNext { get; set; }

    public PagedResults()
    {
    }

    public PagedResults(IEnumerable<T> list, int totalResults, int currentPage, int pageSize)
    {
        ChangeResult(list, totalResults, currentPage, pageSize);
    }

    public static async Task<PagedResults<T>> Create(IQueryable<T> source, int currentPage, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PagedResults<T>(items, count, currentPage, pageSize);
    }

    private void ChangeResult(IEnumerable<T> list, int totalResults, int currentPage, int pageSize)
    {
        Items = list;
        TotalResults = totalResults;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(TotalResults / (double)pageSize);
        HasNext = CurrentPage < TotalPages;
    }

    public PagedResults<T> Apply()
    {
        Items = Items?.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
        return this;
    }
}
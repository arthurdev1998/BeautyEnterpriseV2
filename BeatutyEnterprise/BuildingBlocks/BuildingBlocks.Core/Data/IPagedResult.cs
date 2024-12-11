namespace BuildingBlocks.Core.Data;
public interface IPagedResult<T> where T : class
{
    int CurrentPage { get; set; }
    bool HasNext { get; set; }
    IEnumerable<T>? Items { get; set; }
    int PageSize { get; set; }
    int TotalPages { get; }
    int TotalResults { get; set; }
}
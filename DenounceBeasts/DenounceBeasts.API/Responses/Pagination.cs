using Microsoft.EntityFrameworkCore;

namespace DenounceBeasts.API.Models.Responses
{
    public sealed record PageRequest(int Page = 1, int PageSize = 2, string? Sort = null, bool Desc = false, string? Q = null);

    public sealed record PageResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount, int TotalPages);

    public static class Paging
    {
        public static async Task<PageResult<T>> ToPageAsync<T>(
            this IQueryable<T> query, PageRequest req, CancellationToken ct = default)
        {
            if (req.Page < 1) req = req with { Page = 1 };
            if (req.PageSize is < 1 or > 200) req = req with { PageSize = 20 };

            var total = await EntityFrameworkQueryableExtensions.CountAsync(query, ct);
            var totalPages = (int)Math.Ceiling(total / (double)req.PageSize);

            var skip = (req.Page - 1) * req.PageSize;
            var items = await EntityFrameworkQueryableExtensions
                .ToListAsync(query.Skip(skip).Take(req.PageSize), ct);

            return new PageResult<T>(items, req.Page, req.PageSize, total, totalPages);
        }
    }

}

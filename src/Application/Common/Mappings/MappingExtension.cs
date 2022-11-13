using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public static class MappingExtension
    {
        public static Task<PaginatedItems<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize,CancellationToken cancellationToken)
    => PaginatedItems<TDestination>.GetItemsAsync(queryable, pageNumber, pageSize, cancellationToken);

    }
}

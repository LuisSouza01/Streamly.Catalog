using Streamly.Catalog.Domain.SeedWork.SearchableRepository;

namespace Streamly.Catalog.Application.Common;

public class PaginatedListInput(
    int page,
    int perPage,
    string search,
    string sort,
    SearchOrder dir)
{
    public int Page { get; set; } = page;
    public int PerPage { get; set; } = perPage;
    public string Search { get; set; } = search;
    public string Sort { get; set; } = sort;
    public SearchOrder Dir { get; set; } = dir;
}
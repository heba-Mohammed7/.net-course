namespace cleanArchitecture.Domain.Filters;

public record ProductFilter(int PageSize = 10, int PageNumber = 1, decimal? MinPrice = null, decimal? MaxPrice = null) : BaseFilter(PageSize, PageNumber);

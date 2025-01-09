using MediatR;

namespace Streamly.Catalog.Application.UseCases.Category.ListCategories;

public interface IListCategories 
    : IRequestHandler<ListCategoriesInput, ListCategoriesOutput> { }
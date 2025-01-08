using MediatR;

namespace Streamly.Catalog.Application.UseCases.Category.DeleteCategory;

public interface IDeleteCategory 
    : IRequestHandler<DeleteCategoryInput> { }
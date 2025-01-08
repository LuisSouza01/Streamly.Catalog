using MediatR;

namespace Streamly.Catalog.Application.UseCases.Category.UpdateCategory;

public interface IUpdateCategory 
    : IRequestHandler<UpdateCategoryInput, UpdateCategoryOutput> { }
using MediatR;

namespace Streamly.Catalog.Application.UseCases.Category.CreateCategory;

public interface ICreateCategory 
    : IRequestHandler<CreateCategoryInput, CreateCategoryOutput> { }
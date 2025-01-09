using MediatR;
using Streamly.Catalog.Application.UseCases.Category.Common;

namespace Streamly.Catalog.Application.UseCases.Category.CreateCategory;

public interface ICreateCategory 
    : IRequestHandler<CreateCategoryInput, CategoryModelOutput> { }
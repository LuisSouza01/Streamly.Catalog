using MediatR;
using Streamly.Catalog.Application.UseCases.Category.Common;

namespace Streamly.Catalog.Application.UseCases.Category.UpdateCategory;

public interface IUpdateCategory 
    : IRequestHandler<UpdateCategoryInput, CategoryModelOutput> { }
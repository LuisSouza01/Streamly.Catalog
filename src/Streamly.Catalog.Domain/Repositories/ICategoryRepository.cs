using Streamly.Catalog.Domain.Entities;
using Streamly.Catalog.Domain.SeedWork;
using Streamly.Catalog.Domain.SeedWork.SearchableRepository;

namespace Streamly.Catalog.Domain.Repositories;

public interface ICategoryRepository 
    : IGenericRepository<Category>, ISearchableRepository<Category> { }
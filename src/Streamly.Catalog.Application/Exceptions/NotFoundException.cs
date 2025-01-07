namespace Streamly.Catalog.Application.Exceptions;

public class NotFoundException(string? message) 
    : ApplicationException(message) { }
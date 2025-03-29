public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public class DuplicateEntityException : Exception
{
    public DuplicateEntityException(string message) : base(message) { }
}

public class ValidationException : Exception
{
    public ValidationException(string message) : base(message) { }
}
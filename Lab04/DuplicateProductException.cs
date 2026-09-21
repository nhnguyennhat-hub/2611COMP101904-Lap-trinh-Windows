using System;

public class DuplicateProductException : Exception
{
    public DuplicateProductException(string message) : base(message) { }
}

namespace Application.Exceptions;

public record ValidationError(string propertyName, string ErrorMessage);
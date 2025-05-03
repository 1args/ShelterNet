namespace ShelterNet.Application.Exceptions;

public class TokenExpiredException(string message) : Exception(message);
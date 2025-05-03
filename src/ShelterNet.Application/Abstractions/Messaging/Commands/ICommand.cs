namespace ShelterNet.Application.Abstractions.Messaging.Commands;

public interface ICommand : IBaseCommand
{
    
}

public interface ICommand<TResponse> : IBaseCommand
{
    
}

public interface IBaseCommand
{
    
}
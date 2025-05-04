using System.Reflection;
using ShelterNet.Api.Authorization;
using ShelterNet.Application.Abstractions.Auth;
using ShelterNet.Infrastructure.Auth;

namespace ShelterNet.Api.Extensions;

public static class AuthorizationOptionsExtensions
{
    public static IServiceCollection AddAuthorizationPermissionRequirements(this IServiceCollection services)
    {
        var members = Assembly.GetExecutingAssembly()
            .GetTypes();

        var controllerAttributes = members.GetAttributes<HasPermissionAttribute>();

        var authorizationAttributes = controllerAttributes
            .GroupBy(a => a.Policy) 
            .Select(g => g.First()); 

        var builder = services.AddAuthorizationBuilder();

        foreach (var attribute in authorizationAttributes)
        {
            builder.AddPolicy(attribute.Policy!, policy =>
            {
                policy.Requirements.Add(new PermissionRequirement(attribute!.Permissions));
            });
        }
        
        return services;
    }

    private static IEnumerable<IHasPermissionAttribute> GetAttributes<TAttribute>(this IEnumerable<Type> members)
        where TAttribute : Attribute, IHasPermissionAttribute
    {
        var enumerable = members as Type[] ?? members.ToArray();
        return enumerable.GetAttributesFromMethods<TAttribute>()
            .Concat(enumerable.GetAttributesFromClasses<TAttribute>());
    }

    private static IEnumerable<IHasPermissionAttribute> GetAttributesFromMethods<TAttribute>(
        this IEnumerable<Type> members)
        where TAttribute : Attribute, IHasPermissionAttribute
    {
        return members
            .SelectMany(c => c.GetMethods())
            .FilterAttributes<TAttribute>();
    }

    private static IEnumerable<TAttribute> FilterAttributes<TAttribute>(this IEnumerable<MemberInfo> memberInfos)
        where TAttribute : Attribute
    {
        return memberInfos
            .Where(m => m.GetCustomAttribute<TAttribute>() != null)
            .Select(m => m.GetCustomAttribute<TAttribute>()!);
    }
    
    private static IEnumerable<IHasPermissionAttribute> GetAttributesFromClasses<TAttribute>(
        this IEnumerable<Type> controllerTypes)
        where TAttribute : Attribute, IHasPermissionAttribute
    {
        return controllerTypes.FilterAttributes<TAttribute>();
    }
}
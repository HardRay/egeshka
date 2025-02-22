using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Egeshka.Core.Hosting.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddValidatorsFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        // Получаем все типы из указанной сборки
        var types = assembly.GetTypes();

        // Фильтруем типы, которые наследуются от AbstractValidator и не являются абстрактными
        var validatorTypes = types
            .Where(t => t.BaseType != null &&
                        t.BaseType.IsGenericType &&
                        t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>) &&
                        !t.IsAbstract);

        foreach (var validatorType in validatorTypes)
        {
            // Получаем базовый тип AbstractValidator<T>
            var baseType = validatorType.BaseType;
            if (baseType != null && baseType.IsGenericType)
            {
                // Получаем тип T из AbstractValidator<T>
                var genericArgument = baseType.GetGenericArguments()[0];

                // Создаем закрытый тип AbstractValidator<T>
                var abstractValidatorType = typeof(AbstractValidator<>).MakeGenericType(genericArgument);

                // Регистрируем валидатор в DI контейнере
                services.AddScoped(abstractValidatorType, validatorType);
            }
        }

        return services;
    }
}

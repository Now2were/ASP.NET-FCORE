using ClothingStore.Api.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace ClothingStore.Api.Controllers;

/// <summary>Swagger получает конкретные типы ответов наследуемых generic CRUD-действий.</summary>
public sealed class CrudDocumentationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var type = context.ApiDescription.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor action
            ? action.ControllerTypeInfo.AsType() : null;
        Type? readType = type == typeof(UserRolesController) ? typeof(UserRoleDto)
            : type == typeof(StockReservationsController) ? typeof(StockReservationDto) : null;
        for (var current = type?.BaseType; current is not null; current = current.BaseType)
            if (current.IsGenericType && current.GetGenericTypeDefinition() == typeof(CrudController<,,,>))
                readType = current.GetGenericArguments()[0];
        if (readType is null) return;
        var method = context.ApiDescription.HttpMethod;
        bool list = method == "GET" && !context.ApiDescription.RelativePath!.Contains('{');
        string code = method == "POST" && !context.ApiDescription.RelativePath!.EndsWith("/cancel") ? "201"
            : method is "PUT" or "PATCH" or "DELETE" ? "204" : "200";
        operation.Responses.Clear();
        var response = new OpenApiResponse { Description = code switch { "201" => "Создано", "204" => "Выполнено", _ => "Успешно" } };
        if (code != "204")
            response.Content["application/json"] = new OpenApiMediaType
            {
                Schema = context.SchemaGenerator.GenerateSchema(list ? typeof(List<>).MakeGenericType(readType) : readType, context.SchemaRepository)
            };
        if (list)
            response.Headers["X-Total-Count"] = new OpenApiHeader
            {
                Description = "Общее количество записей до пагинации",
                Schema = new OpenApiSchema { Type = "integer", Format = "int32" }
            };
        operation.Responses[code] = response;
        foreach (var (status, description) in new[] { ("400", "Неверные данные"), ("404", "Запись не найдена"), ("409", "Конфликт или нехватка ресурса") })
            operation.Responses[status] = new OpenApiResponse
            {
                Description = description,
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["application/problem+json"] = new() { Schema = context.SchemaGenerator.GenerateSchema(typeof(ProblemDetails), context.SchemaRepository) }
                }
            };
    }
}

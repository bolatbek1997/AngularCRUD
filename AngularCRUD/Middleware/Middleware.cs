using AngularCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCRUD.Middleware
{

    public static class MvcCoreExtension
    {
        public static IMvcCoreBuilder AddMvcCoreCustom(this IServiceCollection builder)
        {
            return builder.AddMvcCore()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressUseValidationProblemDetailsForInvalidModelStateResponses = true;
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => new
                        {
                            Name = e.Key,
                            Message = e.Value.Errors.First().ErrorMessage
                        }).ToArray();
                        return new OkObjectResult(new BaseResponse<dynamic> { Code = -3108, Message = "Неправильная модель для проверки", Data = new { Errors = errors } });
                    };
                })
                 .AddJsonOptions(options =>
                 {
                     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                     options.SerializerSettings.Formatting = Formatting.Indented;
                     options.SerializerSettings.NullValueHandling = NullValueHandling.Include;
                     options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                     options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                 })
                .AddJsonFormatters()
                .AddFormatterMappings()
                .AddDataAnnotations()
                .AddApiExplorer();
        }

    }
}
